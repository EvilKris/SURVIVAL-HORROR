using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject prefabPauseMenu; 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        //pauseMenuUI.SetActive(false);
        
        //copy.GetComponent<objectInspector>().obToUse = itemToExamine;
        Destroy(prefabPauseMenu);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    void Pause()
    {
        //pauseMenuUI.SetActive(true);
        if (pauseMenuUI == null)
        {
            Debug.Log("Choose a prefab");
            return;
        }

        print("HI");
        prefabPauseMenu = (GameObject)Instantiate(pauseMenuUI);

        Time.timeScale = 0;
        gameIsPaused = true;
    }

}
