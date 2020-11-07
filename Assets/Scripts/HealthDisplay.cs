using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
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
        string health = gameSession.GetHealth().ToString();
        GetComponent<Text>().text = health;
    }

    // Update is called once per frame
    void Update()
    {
        Display(gameSession);
    }
}
