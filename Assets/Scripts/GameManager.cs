using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager m_GameManager;

    public GameObject m_EndScreen;

    public GameObject m_PlayButton;
    public GameObject m_PauseButton;
    public GameObject m_HomeButton;
    public GameObject m_ResumeButton;

    public bool m_bEndGame;
    public bool m_bPauseGame;

    public List<GameObject> InsectList = new List<GameObject>();
    public List<GameObject> BirdList = new List<GameObject>();


    void Awake()
    {
        m_bEndGame = false;
        m_bPauseGame = true;

        m_EndScreen.SetActive(false);

        m_PauseButton.SetActive(false);
        m_HomeButton.SetActive(false);
        m_ResumeButton.SetActive(false);

        if (m_GameManager == null)
        {
            m_GameManager = this;
        }
        else if (m_GameManager != this)
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
    }

    private void Start()
    {
        InsectList[1].SetActive(false);
        InsectList[2].SetActive(false);
        BirdList[1].SetActive(false);
        BirdList[2].SetActive(false);
    }

    void FixedUpdate()
    {
        if (m_bPauseGame == false && m_bEndGame == false)
        {
            // Game is running
            if(m_EndScreen.activeSelf)
            {
                m_EndScreen.SetActive(false);
            }
        }

        if (m_bEndGame)
        {
            // Game over
            SaveScore();
            m_EndScreen.SetActive(true);
        }

        if(Player.m_Player.m_iScore == 200)
        {
            InsectList[1].SetActive(true);
        }

        if(Player.m_Player.m_iScore > 699)
        {
            InsectList[2].SetActive(true);
        }

        if (Player.m_Player.m_iScore > 299)
        {
            BirdList[1].SetActive(true);
        }

        if(Player.m_Player.m_iScore > 599)
        {
            BirdList[2].SetActive(true);
        }
    }

    public void GameStart()
    {
        m_PlayButton.SetActive(false);
        m_PauseButton.SetActive(true);
        m_HomeButton.SetActive(false);
        m_ResumeButton.SetActive(false);

        m_bPauseGame = false;
    }

    public void GamePause()
    {
        m_bPauseGame = true;
        m_HomeButton.SetActive(true);
        m_ResumeButton.SetActive(true);
        m_PauseButton.SetActive(false);
    }

     public void ReturnToHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Reset()
    {
        SceneManager.LoadScene("Main");
    }

    void SaveScore()
    {
        int oldScore = PlayerPrefs.GetInt("Highscore");

        if (oldScore < Player.m_Player.m_iScore)
        {
            PlayerPrefs.SetInt("Highscore", Player.m_Player.m_iScore);
        }
    }
}