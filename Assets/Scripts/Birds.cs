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

    void Awake()
    {
        m_fSpeed = 0.1f;
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameManager.m_GameManager.m_bPauseGame)
        {
            return;
        }

        switch (m_eBehaviour)
        {
            case Behaviour.Spawn:
                {
                    int spawnPos = Random.Range(0, 2);
                    m_fSpeed = Random.Range(0.07f, 0.12f);

                    if (spawnPos == 0)
                    {
                        transform.position = new Vector2(-11, Random.Range(0.0f, 2.0f));
                        m_eBehaviour = Behaviour.Fly_Right;
                    }
                    else if (spawnPos == 1)
                    {
                        transform.position = new Vector2(11, Random.Range(0.0f, 2.0f));
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

                    //if (transform.position.y > 6)
                    //{
                    //    Time.timeScale = 0; // temp result
                    //    Debug.Log("Game Over, please restart.");
                    //}
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
                m_eBehaviour = Behaviour.Spawn;
            }
        }
    }
}