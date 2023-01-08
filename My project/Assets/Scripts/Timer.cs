using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    
    public TextMeshProUGUI timerText;

    public float currentTime;

    public static bool timerOn;
    // Start is called before the first frame update
    void Start()
    {
        timerOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            timerStart();
        }
        else
        {
            timerStop();
        }
    }

    private void timerStart()
    {
        timerOn = true;
        currentTime = currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("0.00");
    }

    private void timerStop()
    {
        timerOn = false;        
    }
}
