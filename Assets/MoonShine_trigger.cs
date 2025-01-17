using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoonShine_trigger : MonoBehaviour
{
    public GameObject MOONSHINECANVAS;

    public bool read = false;

    void Start()
    {
        MOONSHINECANVAS.SetActive(false);
        read = false;
    }

    private void OnTriggerStay(Collider collinder)
    {
        
        if (collinder.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!read)
                {
                    read = true;
                    MOONSHINECANVAS.SetActive(true);


                }

                else
                {
                    MOONSHINECANVAS.SetActive(false);
                    read = false;
                }
               
            }
        }
    }
    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            MOONSHINECANVAS.SetActive(false);
            read = false;
        
        }
    }
}
