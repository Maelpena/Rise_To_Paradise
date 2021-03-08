using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreRegister : MonoBehaviour
{
    public Player player;
    public TMP_Text scoreText;
    public TMP_Text endScoreText;
    public float HeightScore;
    public float TotalScore;
    public float killScore;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }
    private void UpdateScore()
    {
        if (player != null)
        {
            if (Math.Round(player.transform.position.y) > 0)
            {
                HeightScore = Mathf.Max((float)Math.Round(player.transform.position.y) * 2, HeightScore);
                TotalScore = HeightScore + killScore;
                scoreText.text = TotalScore.ToString();
                endScoreText.text = TotalScore.ToString();
            }
        }

    }
    private void OnEnable()
    {
        Enemy.eventEnemyDeath.AddListener(EnemyDied);
    }

    private void EnemyDied(float score, Vector2 position, GameObject Levelpart)
    {
        killScore += score;
    }

}
