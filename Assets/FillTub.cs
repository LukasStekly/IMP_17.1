using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillTub : MonoBehaviour
{
    public Transform waterSurface;
    public float fillSpeed = 0.1f;
    public float maxHeight = 1.0f;
    public bool isFilling = false;
    public ParticleSystem steam;
    public GameObject steam_mirror, mirror, tap, water;

    private bool tapRotated = false; // Kontrola, zda byl kohoutek oto�en
    private bool steamStarted = false; // Kontrola, zda ji� byl efekt p�ry spu�t�n

    void Start()
    {
        // Inicializace stavu
        steam.Stop(); // Zastav� efekt p�ry na za��tku
        mirror.SetActive(true);
        steam_mirror.SetActive(false);
        water.SetActive(false);
    }

    void Update()
    {
        if (isFilling && waterSurface.position.y < maxHeight)
        {
            // Pln�n� vany
            waterSurface.position += new Vector3(0, fillSpeed * Time.deltaTime, 0);

            if (steam.isPlaying)
            {
                steam.Stop(); // Zastav� p�ru b�hem pln�n�
            }
        }
        else if (isFilling && waterSurface.position.y >= maxHeight)
        {
            // Konec pln�n�
            isFilling = false;

            if (!steamStarted)
            {
                steam.Play(); // Spust� efekt p�ry
                steamStarted = true; // Zajist�, �e p�ra se spust� pouze jednou
            }

            mirror.SetActive(false);
            water.SetActive(false);
            steam_mirror.SetActive(true); // Uk�e zrcadlo s p�rou
        }
    }

    private void RotateTap()
    {
        if (!tapRotated)
        {
            tap.transform.Rotate(0, 0, 90); // Oto�� kohoutek o 90 stup��
            tap.transform.Translate(4, 0, 4, 0);
            tapRotated = true;
        }
    }

    public void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                RotateTap();
                StartFilling();

                // Aktivace p��slu�n�ch objekt�
                mirror.SetActive(true);
                steam_mirror.SetActive(false);
                water.SetActive(true);

                steamStarted = false; // Reset efektu p�ry
            }
        }
    }

    public void StartFilling()
    {
        isFilling = true;
    }
}

