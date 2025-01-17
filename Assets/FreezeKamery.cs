using UnityEngine;
using Cinemachine;
using System.Diagnostics;

public class FreezeCameraOnCtrl : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera; // Reference na Cinemachine kameru

    private bool isCameraFrozen = false; // Stav, zda je kamera zamrzl�

    void Update()
    {
        // Detekce stisknut� a uvoln�n� kl�vesy Ctrl
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            FreezeCamera(true); // Zamrzne kameru a zobraz� kurzor
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            FreezeCamera(false); // Uvoln� kameru a skryje kurzor
        }
    }

    void FreezeCamera(bool freeze)
    {
        if (thirdPersonCamera != null)
        {
            isCameraFrozen = freeze;

            if (freeze)
            {
                // Zamrzne kameru
                thirdPersonCamera.m_XAxis.m_InputAxisName = ""; // Zastav� pohyb na ose X
                thirdPersonCamera.m_YAxis.m_InputAxisName = ""; // Zastav� pohyb na ose Y

                // Zobraz� kurzor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // Uvoln� kameru
                thirdPersonCamera.m_XAxis.m_InputAxisName = "Mouse X"; // Obnov� pohyb na ose X
                thirdPersonCamera.m_YAxis.m_InputAxisName = "Mouse Y"; // Obnov� pohyb na ose Y

                // Skryje kurzor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            
        }
    }
}
