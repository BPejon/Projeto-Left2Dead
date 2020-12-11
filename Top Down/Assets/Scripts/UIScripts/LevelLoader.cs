using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))    //Se clicar com o botão esquerdo do mouse
        {

        }
        
    }

    public void LoadNextLevel()
    {
        StartCoroutine( LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    //Criar uma corotina. Atrasar o código

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start"); //Parametro que criamos no animator

        //Wait animation
        yield return new WaitForSeconds(transitionTime);

        //Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
