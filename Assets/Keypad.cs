using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class Keypad : MonoBehaviour
{
    [SerializeField] private TMP_Text Ans;
    [SerializeField] private Animator Door;
    

    private string Answer = "123456";

    public void Number(int number)
    {
        Ans.text += number.ToString();
    }

    public void Execute()
    {
        Door.SetBool("Open", false);
        if (Ans.text == Answer)
        {
            Ans.text = "Correct";
            Door.SetBool("Open", true);
            StartCoroutine("StopDoor");
        }
        else
        {
            Ans.text = "Invalid";
            Door.SetBool("Open", false);
        }
    }

    public void Cancel()
    {
        Ans.text = "";
    }

    IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(2f);
        Door.SetBool("Open", false);
        Door.enabled = false;
    }
}
