using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject fireFX;
    [SerializeField] float levelLoadDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        
    }


    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        Invoke("SendLoadMessage", levelLoadDelay);
        fireFX.SetActive(true);
        deathFX.SetActive(true);

    }

    void SendLoadMessage()
    {
        SendMessage("ReloadLevel");
    }




}
