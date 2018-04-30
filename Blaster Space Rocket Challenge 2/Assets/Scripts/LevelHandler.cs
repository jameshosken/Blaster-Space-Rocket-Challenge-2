using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler: MonoBehaviour {

    // Use this class to load next level, revert to menu/win/lose screens
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void LoadNextLevel()
    {
        print("Loading level in scene manager");
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int maxLevel = SceneManager.sceneCountInBuildSettings;
        int nextLevel = (currentLevel + 1) % maxLevel;

        SceneManager.LoadScene(nextLevel);

    }

     void ReloadLevel()
    {
        print("Loading level in scene manager");
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentLevel);

    }
}

