using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_GameManager;
    public GameObject m_PlayButton;
    public GameObject m_PauseButton;
    public GameObject m_HomeButton;
    public GameObject m_ResumeButton;

    public bool m_bEndGame;
    public bool m_bPauseGame;

	void Awake()
    {
        m_bEndGame = false;
        m_bPauseGame = true;

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
    }
	
	void Update()
    {
        if (m_bPauseGame == false && m_bEndGame == false)
        {
            // Game is running
        }

        if (m_bPauseGame)
        {
            // Game is paused
        }

        if (m_bEndGame)
        {
            // Game over
            Debug.Log("Game Over");
            
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
        Application.LoadLevel("Menu");
    }
}