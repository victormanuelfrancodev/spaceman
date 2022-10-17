using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text scoreText,coinsText,maxScoreText; 
    private PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.shareInstance.currentGameState == GameState.inGame)
        {
            int coins = GameManager.shareInstance.collectableObject;
            float score = player.GetTraveledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore",0f);

            coinsText.text = "Coins: " + coins.ToString();
            scoreText.text = "Score: " + score.ToString("f1");
            maxScoreText.text = "Max Score: " + maxScore.ToString("f1");
        }
    }
}
