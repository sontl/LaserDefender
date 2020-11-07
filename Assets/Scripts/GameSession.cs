using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        CreateSingletonSession();
    }

    private void CreateSingletonSession()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int score)
    {
        this.score += score;
        Debug.Log("score: " + this.score);
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
