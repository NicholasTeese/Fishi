using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Behaviour
    {
        Travel_Left,
        Travel_Right,
        Ascend,
        Descend
    }

    private Behaviour m_eBehaviour = Behaviour.Travel_Left;
    private Behaviour m_eTravelDir = Behaviour.Travel_Left;

    public GameObject m_LeftBounds; public GameObject m_RightBounds;

    private SpriteRenderer m_Sprite;

    private float m_fSpeed;
    private float m_fJumpHeight;
    private float m_fSwimDepth;

    private bool m_bJumping;

    void Awake ()
    {
        m_fSpeed = 0.1f;
        m_fJumpHeight = 2.3f;
        m_fSwimDepth = -1.8f;
        m_bJumping = false;
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !m_bJumping)
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
            case Behaviour.Travel_Left:
                {
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
                    transform.Translate(0, m_fSpeed, 0);

                    if (transform.position.y > m_fJumpHeight)
                    {
                        m_eBehaviour = Behaviour.Descend;
                    }

                    break;
                }
            case Behaviour.Descend:
                {
                    transform.Translate(0, -m_fSpeed, 0);                    

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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.name == "Bounds_Left")
    //    {
    //        // Turn around
    //        m_Behaviour = Behaviour.Travel_Right;
    //    }
    //
    //    if (collision.collider.name == "Bounds_Right")
    //    {
    //        // Turn around
    //        m_Behaviour = Behaviour.Travel_Left;
    //    }
    //}
}