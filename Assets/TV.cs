using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public GameObject battery1, battery2; // Reference na baterie
    private bool B1 = false, B2 = false; // Stav bateri�
    public bool Jump_Player = false; // Stav, zda hr�� m��e aktivovat ��slo

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == battery1)
        {
            Destroy(collision.gameObject);
            B1 = true;
        }

        if (collision.gameObject == battery2)
        {
            Destroy(collision.gameObject);
            B2 = true;
        }
    }

    void Update()
    {
        if (B1 && B2)
        {
            Jump_Player = true; // Aktivace mo�nosti zobrazen� ��sla
        }
    }
}
