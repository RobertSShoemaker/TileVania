using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    float levelLoadDelay = 1f;
    int currentSceneIndex;

    void Start()
    {
        //build index of scenes based on current scene number
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    //If the player collides with the exit, then load the next scene
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    

    //load the next level after a short delay
    IEnumerator LoadNextLevel()
    {
        //delay before loading next level
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int nextSceneIndex = currentSceneIndex + 1;

        //if we are on the last level, then load the first level
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        //load the next level
        SceneManager.LoadScene(nextSceneIndex);
    }
}
