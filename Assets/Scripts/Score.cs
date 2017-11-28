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

    void Update()
    {
        m_iScore = Player.m_Player.m_iScore;

        if (m_textDisplay != null)
        {
            m_textDisplay.text = "Score: " + m_iScore;
        }
        else
        {
            Debug.Log("Text display not found");
        }
	}
}