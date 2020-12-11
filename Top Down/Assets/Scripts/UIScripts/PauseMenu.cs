using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Variavel universal
    public static bool gameIsPaused = false;

    //Abrir o menu com um som
    public AudioSource AudioSource;
    public AudioClip PauseSound;
    public AudioClip DispauseSound;

    public GameObject PauseMenuUI;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }


    public void Resume()
    {
        AudioSource.PlayOneShot(DispauseSound);
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        AudioSource.PlayOneShot(PauseSound);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitGame(){

        Debug.Log("Quiting...");
        Application.Quit();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("LoadingMenu...");
        SceneManager.LoadScene("Menu");

    }
}
