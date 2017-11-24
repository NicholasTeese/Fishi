using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int m_iScore;

	void Awake()
    {
        m_iScore = 0;
	}
	
	void Update()
    {
        m_iScore = Player.m_Player.m_iScore;
	}
}