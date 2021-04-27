using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Script : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject wonDialogBox;
    float timerDisplay;
    
    void Start()
    {
        dialogBox.SetActive(false);
        wonDialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }
    
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                wonDialogBox.SetActive(false);
            }
        }
    }
    
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        wonDialogBox.SetActive(false);
        
    }
}
