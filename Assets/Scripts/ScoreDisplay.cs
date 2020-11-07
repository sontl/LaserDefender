using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        Display(gameSession);
    }

    private void Display(GameSession gameSession)
    {
        string score = gameSession.GetScore().ToString();
        Debug.Log("score: " + score);
        GetComponent<Text>().text = score;
    }

    // Update is called once per frame
    void Update()
    {
        Display(gameSession);
    }
}
