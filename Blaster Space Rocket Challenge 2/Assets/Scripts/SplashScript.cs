using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

public class SplashScript : MonoBehaviour
{

    LevelHandler levelHandler;

    // Use this for initialization
    void Start()
    {
        levelHandler = GetComponent<LevelHandler>();
        //Invoke("ExitSplash", delayTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            levelHandler.LoadNextLevel();
        }
    }
}


