using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform playerHand; // Pozice, kde bude item dr�en
    private GameObject currentItem; // Aktu�ln� item, kter� hr�� zved�
    private bool isHoldingItem = false; // Kontrola, zda hr�� dr�� item

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isHoldingItem)
            {
                DropItem();
            }
            else
            {
                TryPickupItem();
            }
        }
    }

    void TryPickupItem()
    {
        // Kontrola koliz� pomoc� SphereCast
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f); // Polom�r detekce
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("PickupItem"))
            {
                PickupItem(collider.gameObject);
                break;
            }
        }
    }

    void PickupItem(GameObject item)
    {
        currentItem = item;
        isHoldingItem = true;

        // Deaktivace fyziky a p�ipojen� k hr��i
        Rigidbody itemRb = currentItem.GetComponent<Rigidbody>();
        if (itemRb != null)
        {
            itemRb.isKinematic = true;
        }
        currentItem.GetComponent<Collider>().enabled = false;

        // P�esunut� itemu do ruky
        currentItem.transform.SetParent(playerHand);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
    }

    void DropItem()
    {
        if (currentItem == null) return;

        // Reaktivace fyziky a odpojen� od hr��e
        Rigidbody itemRb = currentItem.GetComponent<Rigidbody>();
        if (itemRb != null)
        {
            itemRb.isKinematic = false;
        }
        currentItem.GetComponent<Collider>().enabled = true;

        // Odstran�n� parentingu a odhozen� itemu
        currentItem.transform.SetParent(null);
        currentItem = null;
        isHoldingItem = false;
    }
}
