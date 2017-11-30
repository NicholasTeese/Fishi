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

    public enum InsectType
    {
        Butterfly,
        Dragonfly,
        Cicada
    }

    private Behaviour m_eBehaviour = Behaviour.Spawn;
    private InsectType m_eInsectType;

    public GameObject m_LeftBounds; public GameObject m_RightBounds;

    private SpriteRenderer m_Sprite;

    private float m_fSpeed;
    private int m_iScore;

    void Awake()
    {
        m_fSpeed = 0;
        m_Sprite = GetComponent<SpriteRenderer>();

        if (gameObject.name == "Butterfly") { m_eInsectType = InsectType.Butterfly; }
        else if (gameObject.name == "Dragonfly") { m_eInsectType = InsectType.Dragonfly; }
        else if (gameObject.name == "Cicada") { m_eInsectType = InsectType.Cicada; }
    }

    private void Start()
    {
        switch(m_eInsectType)
        {
            case InsectType.Butterfly:
                {
                    m_iScore = 50;
                    break;
                }
            case InsectType.Dragonfly:
                {
                    m_iScore = 100;
                    break;
                }
            case InsectType.Cicada:
                {
                    m_iScore = 150;
                    break;
                }
            default:
                {
                    Debug.Log("Insect Type not found");
                    break;
                }
        }
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
                    if (m_eInsectType == InsectType.Butterfly)
                    {
                        m_fSpeed = Random.Range(0.07f, 0.12f);
                    }
                    if (m_eInsectType == InsectType.Dragonfly)
                    {
                        m_fSpeed = Random.Range(0.1f, 0.15f);
                    }
                    if (m_eInsectType == InsectType.Cicada)
                    {
                        m_fSpeed = Random.Range(0.13f, 0.18f);
                    }

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
                    Player.m_Player.m_iScore += m_iScore;
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
        }
        if (collision.gameObject.CompareTag("Boundary"))
        {
            if (GameManager.m_GameManager.m_bEndGame)
            {
                this.gameObject.SetActive(false);
            }

            m_eBehaviour = Behaviour.Spawn;
        }
    }
}