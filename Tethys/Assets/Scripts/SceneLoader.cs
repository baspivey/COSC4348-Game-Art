using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject options;

    public int ScenceToLoad;
    int MainMenu = 0;
    int StartLevel = 1;
 
    int CurrentScene;


    public  void LoadStartScene()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void RestartLevel()
    {
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }

    public void LoadNext()
    {
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(CurrentScene + 1);
        StartCoroutine(LoadLevel(CurrentScene + 1));

    }
    public void LoadStart()
    {
        SceneManager.LoadScene(StartLevel);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Clicked");
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
