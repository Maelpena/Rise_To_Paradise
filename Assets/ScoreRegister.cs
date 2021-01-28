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
    public double HeightScore;
    public double TotalScore;

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
                HeightScore = (int)Math.Round(player.transform.position.y) * 2;
                TotalScore = HeightScore + charData.score;
                scoreText.text = TotalScore.ToString();
            }
        }
        
    }
}
