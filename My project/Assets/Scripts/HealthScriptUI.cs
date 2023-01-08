using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScriptUI : MonoBehaviour
{
    public Image heart1,heart2, heart3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.playerHealth < 1)
        {
            heart1.enabled = false;
            heart2.enabled = false;
            heart3.enabled = false;
        }
        else if(PlayerController.playerHealth < 2)
        {
            heart1.enabled = true;
            heart2.enabled = false;
            heart3.enabled = false;
        }
        else if(PlayerController.playerHealth < 3)
        {
            heart1.enabled = true;
            heart2.enabled = true;
            heart3.enabled = false;
        }
        else if(PlayerController.playerHealth < 4)
        {
            heart1.enabled = true;
            heart2.enabled = true;
            heart3.enabled = true;
        }
    }
}
