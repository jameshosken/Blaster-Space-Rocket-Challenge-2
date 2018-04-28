using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour {

    [SerializeField] float delayTimer = 5f;
    LevelHandler levelHandler;

	// Use this for initialization
	void Start () {
        levelHandler = GetComponent<LevelHandler>();
        Invoke("ExitSplash", delayTimer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ExitSplash()
    {
        levelHandler.LoadNectLevel();
    }
}
