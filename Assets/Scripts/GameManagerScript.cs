using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject endPanel;
    public GameObject player;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<CharacterData>().eventDeath.AddListener(DisplayEndMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf) 
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }

        }

    }

    public void DisplayEndMenu()
    {
        Time.timeScale = 0;
        endPanel.SetActive(true);
    }

    public void ResumeBtn()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartBtn()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        pausePanel.SetActive(false);

    }
    public void QuitBtn()
    {
        Application.Quit();
    }

}
