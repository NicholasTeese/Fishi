using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int m_iScore;

    private Text m_textDisplay;

    void Awake()
    {
        m_iScore = 0;

        m_textDisplay = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        m_iScore = Player.m_Player.m_iScore;

        if (m_textDisplay != null)
        {
            if (m_textDisplay.CompareTag("PlayerScore"))
            {
                m_textDisplay.text = "Score: " + m_iScore;
            }
            else if (m_textDisplay.CompareTag("Highscore"))
            {
                m_textDisplay.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
            }
            else
            {
                Debug.Log("Invalid Scoring Tag");
            }
        }
        else
        {
            Debug.Log("Text display not found");
        }
	}
}