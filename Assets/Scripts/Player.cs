using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player m_Player;

    public enum Behaviour
    {
        Spawn,
        Travel_Left,
        Travel_Right,
        Ascend,
        Descend
    }

    private Behaviour m_eBehaviour;
    private Behaviour m_eTravelDir;

    public GameObject m_LeftBounds; public GameObject m_RightBounds;
    
    private SpriteRenderer m_Sprite;

    private float m_fSpeed;
    private float m_fJumpHeight;
    private float m_fSwimDepth;

    public int m_iScore;

    private bool m_bJumping;

    void Awake()
    {
        if (m_Player == null)
        {
            m_Player = this;
        }
        else if (m_Player != this)
        {
            Destroy(gameObject);
        }

        m_fSpeed = 0.1f;
        m_fJumpHeight = 2.3f;
        m_fSwimDepth = -1.8f;
        m_iScore = 0;
        m_bJumping = false;

        m_eBehaviour = Behaviour.Spawn;
        
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameManager.m_GameManager.m_bPauseGame)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !m_bJumping)
        {
            if (m_eTravelDir != m_eBehaviour)
            {
                m_eTravelDir = m_eBehaviour;
            }

            m_bJumping = true;
            m_eBehaviour = Behaviour.Ascend;
        }

        switch (m_eBehaviour)
        {
            case Behaviour.Spawn:
                {
                    transform.position = new Vector2(0.0f, -1.85f);

                    int spawnPos = Random.Range(0, 2);

                    if (spawnPos == 0)
                    {
                        m_eBehaviour = Behaviour.Travel_Right;
                    }
                    else if (spawnPos == 1)
                    {
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
                    transform.rotation = new Quaternion(0, 0, 0, 0);

                    m_eTravelDir = m_eBehaviour;
                    m_Sprite.flipX = false;
                    transform.Translate(-m_fSpeed, 0, 0);

                    if (transform.position.x < m_LeftBounds.transform.position.x)
                    {
                        m_eBehaviour = Behaviour.Travel_Right;
                    }

                    break;
                }
            case Behaviour.Travel_Right:
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);

                    m_eTravelDir = m_eBehaviour;
                    m_Sprite.flipX = true;
                    transform.Translate(m_fSpeed, 0, 0);

                    if (transform.position.x > m_RightBounds.transform.position.x)
                    {
                        m_eBehaviour = Behaviour.Travel_Left;
                    }

                    break;
                }
            case Behaviour.Ascend:
                {
                    Quaternion ascentRotation = new Quaternion(0, 0, 0, 0);

                    if (m_eTravelDir == Behaviour.Travel_Left)
                    {
                        ascentRotation = new Quaternion(0.0f, 0.0f, -0.7f, 0.7f);
                        transform.rotation = ascentRotation;
                        transform.Translate(-m_fSpeed, 0, 0);
                    }
                    else if(m_eTravelDir == Behaviour.Travel_Right)
                    {
                        ascentRotation = new Quaternion(0.0f, 0.0f, 0.7f, 0.7f);
                        transform.rotation = ascentRotation;
                        transform.Translate(m_fSpeed, 0, 0);
                    }
                    else
                    {
                        Debug.Log("Travel Direction Invalid");
                    }

                    if (transform.position.y > m_fJumpHeight)
                    {
                        m_eBehaviour = Behaviour.Descend;
                    }

                    break;
                }
            case Behaviour.Descend:
                {
                    Quaternion descentRotation = new Quaternion(0, 0, 0, 0);

                    if (m_eTravelDir == Behaviour.Travel_Left)
                    {
                        descentRotation = new Quaternion(0.0f, 0.0f, 0.7f, 0.7f);
                        transform.rotation = descentRotation;
                        transform.Translate(-m_fSpeed, 0, 0);
                    }
                    else if (m_eTravelDir == Behaviour.Travel_Right)
                    {
                        descentRotation = new Quaternion(0.0f, 0.0f, -0.7f, 0.7f);
                        transform.rotation = descentRotation;
                        transform.Translate(m_fSpeed, 0, 0);
                    }
                    else
                    {
                        Debug.Log("Travel Direction Invalid");
                    }                 

                    if (transform.position.y < m_fSwimDepth)
                    {
                        m_bJumping = false;
                        m_eBehaviour = m_eTravelDir;
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
}