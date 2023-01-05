using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcText : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dinoText;
    void Start()
    {
        dinoText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            dinoText.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collide)
    {
        if (collide.gameObject.tag =="Player")
        {
            dinoText.SetActive(false);
        }
    }
}
