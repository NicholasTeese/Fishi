using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_GameManager;

    protected bool m_bEndGame;
    protected bool m_bPauseGame;

	void Awake()
    {
        m_bEndGame = false;
        m_bPauseGame = false;

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
            Time.timeScale = 0; // Temp result
        }
	}
}