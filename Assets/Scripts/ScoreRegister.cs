using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreRegister : MonoBehaviour
{
    public GameObject player;
    public CharacterData charData;
    public TMP_Text scoreText;
    public TMP_Text endScoreText;
    public double HeightScore;
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
            if ((int)Math.Round(player.transform.position.y) > 0)
            {
                HeightScore = Mathf.Max((float)Math.Round(player.transform.position.y) * 2, (float)HeightScore);
                TotalScore = (float)HeightScore + charData.score + killScore;
                scoreText.text = TotalScore.ToString();
                endScoreText.text = TotalScore.ToString();
            }
        }

    }
    private void OnEnable()
    {
        CharacterData.eventEnemyDeath.AddListener(EnemyDied);
    }

    private void EnemyDied(float score, Vector2 position, GameObject Levelpart)
    {
        killScore += score;
    }

}
