using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;

    //Singleton to make sure that we only have one GameSession per level
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        //if a GameSession already exists, then destroy this new GameSession
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            //allows GameSession to be kept from one level to the next
            DontDestroyOnLoad(gameObject);
        }
    }

    //determine whether we need to restart the level or the entire game based on the number of remaining player lives
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    //Subtract from the player's current lives and reload the level
    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //restart the game from level 1 when the player dies
    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        //make sure we destroy this GameSession when we restart the game
        Destroy(gameObject);
    }
}
