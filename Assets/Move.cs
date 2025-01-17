using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, olw_speed, rn_speed, ro_speed, jumpForce;
    public bool walking;
    public bool jumping; // Indikace, zda hr�� sk��e
    bool light;
    public Transform playerTrans;
    public GameObject switchoff, switchon, lightoff, lighton, sedm;

    static public bool dialogue = false;

    void Start()
    {
        // Ujisti se, �e Rigidbody je spr�vn� nastaveno
        if (!playerRigid) playerRigid = GetComponent<Rigidbody>();
        playerRigid.freezeRotation = true; // Zabr�n� nekontrolovan�mu ot��en�
    }

    void FixedUpdate()
    {
        // Pohyb postavy pouze horizont�ln�
        if (walking && !jumping)
        {
            Vector3 movement = transform.forward * w_speed;
            playerRigid.velocity = new Vector3(movement.x, playerRigid.velocity.y, movement.z);
        }
        else if (!jumping)
        {
            playerRigid.velocity = new Vector3(0, playerRigid.velocity.y, 0); // Zastav horizont�ln� pohyb
        }
        else if (!dialogue)
        {

        }
    }

    void Update()
    {
        // Ch�ze
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }
        
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;
            w_speed = olw_speed; // Reset na p�vodn� rychlost
        }

        // Skok
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            jumping = true;
            playerAnim.SetTrigger("jump");
            playerAnim.ResetTrigger("idle");

            // Aplikace s�ly skoku
            playerRigid.velocity = new Vector3(playerRigid.velocity.x, jumpForce, playerRigid.velocity.z);
        }

        // Oto�en�
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        }

        // B�h
        if (walking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                w_speed += rn_speed; // Zrychlen�
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                w_speed = olw_speed; // N�vrat k rychlosti ch�ze
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            playerAnim.SetTrigger("dance1");
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
        {
            playerAnim.SetTrigger("dance2");
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            playerAnim.SetTrigger("dance3");
        }
    }

    // Detekce p�ist�n�
    void OnCollisionEnter(Collision collision)
    {
        // Zkontroluj, �e objekt m� Collider a nen� Trigger
        if (collision.collider != null && !collision.collider.isTrigger)
        {
            jumping = false;
            playerAnim.SetTrigger("idle"); // N�vrat k animaci klidu
        }

        if (collision.gameObject.tag == "Push")
        {
            if (!jumping && Input.GetKey(KeyCode.W))
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("dance2");
                w_speed = w_speed / 2;
            }

        }
        

    }
    private void OnTriggerStay(Collider collinder)
    {
        if (collinder.gameObject.tag == "Switch")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                light = !light;

                if (light)
                {
                    lightoff.SetActive(false);
                    lighton.SetActive(true);
                    switchon.SetActive(true);
                    switchoff.SetActive(false);
                    sedm.SetActive(false);   
                }
                else
                {
                    lightoff.SetActive(true);
                    lighton.SetActive(false);
                    switchon.SetActive(false);
                    switchoff.SetActive(true);
                    sedm.SetActive(true);
                }
            }
        }
    }


}