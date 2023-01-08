using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;

    public static bool paused;
    // Start is called before the first frame update
    void Start()
    {
        GameResume();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            if(paused)
            {
                GameResume();                
            }
            else
            {
                GamePause();
            }
                    
        }
    }

    public void GamePause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void GameResume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
