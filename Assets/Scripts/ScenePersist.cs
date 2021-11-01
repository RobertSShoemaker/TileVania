using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    //Singleton to make sure that we only have one GameSession per level
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        //if a ScenePersist already exists, then destroy this new ScenePersist
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            //allows ScenePersist to be kept when player dies
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
