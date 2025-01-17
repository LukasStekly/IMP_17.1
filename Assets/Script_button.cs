using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_button : MonoBehaviour
{
    public TV tvScript; // Reference na TV skript
    public GameObject number; // Objekt ��sla, kter� se m� aktivovat

    private void OnCollisionEnter(Collision collision)
    {
        // Zaji�t�n�, �e hr�� m��e aktivovat ��slo
        if (tvScript.Jump_Player && collision.gameObject.CompareTag("Player"))
        {
            number.SetActive(true); // Aktivace ��sla
        }
    }
}
