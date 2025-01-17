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
    public bool jumping; // Indikace, zda hráè skáèe
    bool light;
    public Transform playerTrans;
    public GameObject switchoff, switchon, lightoff, lighton, sedm;

    static public bool dialogue = false;

    void Start()
    {
        // Ujisti se, že Rigidbody je správnì nastaveno
        if (!playerRigid) playerRigid = GetComponent<Rigidbody>();
        playerRigid.freezeRotation = true; // Zabrání nekontrolovanému otáèení
    }

    void FixedUpdate()
    {
        // Pohyb postavy pouze horizontálnì
        if (walking && !jumping)
        {
            Vector3 movement = transform.forward * w_speed;
            playerRigid.velocity = new Vector3(movement.x, playerRigid.velocity.y, movement.z);
        }
        else if (!jumping)
        {
            playerRigid.velocity = new Vector3(0, playerRigid.velocity.y, 0); // Zastav horizontální pohyb
        }
        else if (!dialogue)
        {

        }
    }

    void Update()
    {
        // Chùze
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
            w_speed = olw_speed; // Reset na pùvodní rychlost
        }

        // Skok
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            jumping = true;
            playerAnim.SetTrigger("jump");
            playerAnim.ResetTrigger("idle");

            // Aplikace síly skoku
            playerRigid.velocity = new Vector3(playerRigid.velocity.x, jumpForce, playerRigid.velocity.z);
        }

        // Otoèení
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        }

        // Bìh
        if (walking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                w_speed += rn_speed; // Zrychlení
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                w_speed = olw_speed; // Návrat k rychlosti chùze
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

    // Detekce pøistání
    void OnCollisionEnter(Collision collision)
    {
        // Zkontroluj, že objekt má Collider a není Trigger
        if (collision.collider != null && !collision.collider.isTrigger)
        {
            jumping = false;
            playerAnim.SetTrigger("idle"); // Návrat k animaci klidu
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