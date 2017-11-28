using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : MonoBehaviour
{
    public enum Behaviour
    {
        Spawn,
        Travel_Left,
        Travel_Right,
        Perish
    }

    private Behaviour m_eBehaviour = Behaviour.Spawn;

    public GameObject m_LeftBounds; public GameObject m_RightBounds;

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
                        m_eBehaviour = Behaviour.Travel_Right;
                    }
                    else if (spawnPos == 1)
                    {
                        transform.position = new Vector2(11, Random.Range(0.0f, 2.0f));
                        m_eBehaviour = Behaviour.Travel_Left;
                    }
                    else
                    {
                        Debug.Log("Error randomising spawn location, spawnPos = " + spawnPos);
                    }
                    break;
                }
            case Behaviour.Travel_Left:
                {
                    m_Sprite.flipX = true;
                    transform.Translate(-m_fSpeed, 0, 0);

                    break;
                }
            case Behaviour.Travel_Right:
                {
                    m_Sprite.flipX = false;
                    transform.Translate(m_fSpeed, 0, 0);

                    break;
                }
            case Behaviour.Perish:
                {
                    Player.m_Player.m_iScore += 50;
                    Debug.Log(Player.m_Player.m_iScore);
                    m_eBehaviour = Behaviour.Spawn;

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
            m_eBehaviour = Behaviour.Perish;
            // Debug.Log("Nom");
        }
        if (collision.gameObject.CompareTag("Boundary"))
        {
            m_eBehaviour = Behaviour.Spawn;
            // Debug.Log("Nom");
        }
    }
}