using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalScoreIncrement : MonoBehaviour
{

    ScoreBoard scoreBoard;

    [SerializeField] int scoreIncrement = 1;
    [Tooltip("In Seconds")] [SerializeField] float interval = 5f;

    // Use this for initialization
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();

        InvokeRepeating("IncrementScore", 1f, interval);
    }

    void IncrementScore()
    {
        scoreBoard.ScoreHit(scoreIncrement);
    }
}