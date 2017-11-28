using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float m_fSpeed = 0;

    public GameObject m_RightBounds;

    void Awake()
    {
        m_fSpeed = Random.Range(0.01f, 0.08f);
	}
	
	void Update()
    {
        if (GameManager.m_GameManager.m_bPauseGame)
        {
            return;
        }

        transform.Translate(m_fSpeed, 0, 0);

        if (transform.position.x > m_RightBounds.transform.position.x)
        {
            Reset();
        }
    }

    private void Reset()
    {
        m_fSpeed = Random.Range(0.01f, 0.08f);
        transform.position = new Vector2(-11, Random.Range(2.5f, 4.5f));
    }
}