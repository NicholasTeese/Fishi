using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{
    public enum Behaviour
    {
        Spawn,
        Fly_Left,
        Fly_Right,
        Capture
    }

    private Behaviour m_eBehaviour = Behaviour.Spawn;
    private Behaviour m_eTravelDir = Behaviour.Fly_Left;

    public GameObject m_LeftBounds; public GameObject m_RightBounds;
    public GameObject m_Fish;

    private SpriteRenderer m_Sprite;

    private float m_fSpeed;
    private float m_fRangeUpper; private float m_fRangeLower;

    private bool m_bCleared;

    void Awake()
    {
        m_bCleared = false;
        m_fSpeed = 0.1f;
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        m_fRangeLower = transform.position.x - 0.3f;
        m_fRangeUpper = transform.position.x + 0.3f;

        if (GameManager.m_GameManager.m_bPauseGame)
        {
            return;
        }

        if (Player.m_Player.m_bJumping)
        {
            if (Player.m_Player.transform.position.y > transform.position.y + 0.5f)
            {
                if (Player.m_Player.transform.position.x > m_fRangeLower && Player.m_Player.transform.position.x < m_fRangeUpper)
                {
                    if(!m_bCleared)
                    {
                        Player.m_Player.m_iScore += 150;
                        m_bCleared = true;
                    }
                }
            }
        }
        else
        {
            m_bCleared = false;
        }

        switch (m_eBehaviour)
        {
            case Behaviour.Spawn:
                {
                    int spawnPos = Random.Range(0, 2);
                    m_fSpeed = Random.Range(0.07f, 0.12f);

                    if (spawnPos == 0)
                    {
                        transform.position = new Vector2(-10.5f, Random.Range(0.0f, 2.0f));
                        m_eBehaviour = Behaviour.Fly_Right;
                    }
                    else if (spawnPos == 1)
                    {
                        transform.position = new Vector2(10.5f, Random.Range(0.0f, 2.0f));
                        m_eBehaviour = Behaviour.Fly_Left;
                    }
                    else
                    {
                        Debug.Log("Error randomising spawn location, spawnPos = " + spawnPos);
                    }

                    break;
                }
            case Behaviour.Fly_Left:
                {
                    m_eTravelDir = m_eBehaviour;
                    m_Sprite.flipX = false;
                    transform.Translate(-m_fSpeed, 0, 0);

                    break;
                }
            case Behaviour.Fly_Right:
                {
                    m_eTravelDir = m_eBehaviour;
                    m_Sprite.flipX = true;
                    transform.Translate(m_fSpeed, 0, 0);

                    break;
                }
            case Behaviour.Capture:
                {
                    Player.m_Player.gameObject.SetActive(false);
                    m_Fish.SetActive(true);

                    if (m_eTravelDir == Behaviour.Fly_Left)
                    {
                        transform.Translate(-m_fSpeed, m_fSpeed, 0);
                    }
                    else if (m_eTravelDir == Behaviour.Fly_Right)
                    {
                        transform.Translate(m_fSpeed, m_fSpeed, 0);
                    }
                    else
                    {
                        Debug.Log("Travel direction not found, previous behaviour == " + m_eBehaviour);
                    }
                    break;
                }
            default:
                {
                    Debug.Log("Behaviour not recognised");
                    break;
                }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_eBehaviour = Behaviour.Capture;
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            if (m_eBehaviour == Behaviour.Capture)
            {
                // Ignore boundary collision
            }
            else
            {
                if (GameManager.m_GameManager.m_bEndGame)
                {
                    this.gameObject.SetActive(false);
                }
                else
                {
                    m_eBehaviour = Behaviour.Spawn;
                }
            }
        }
    }
}