using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : MonoBehaviour
{
    public enum Behaviour
    {
        Travel_Left,
        Travel_Right,
        Perish
    }

    private Behaviour m_eBehaviour = Behaviour.Travel_Left;
    private Behaviour m_eTravelDir = Behaviour.Travel_Left;

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
        switch (m_eBehaviour)
        {
            case Behaviour.Travel_Left:
                {
                    m_Sprite.flipX = true;
                    transform.Translate(-m_fSpeed, 0, 0);

                    if (transform.position.x < m_LeftBounds.transform.position.x)
                    {
                        m_eBehaviour = Behaviour.Travel_Right;
                    }

                    break;
                }
            case Behaviour.Travel_Right:
                {
                    m_Sprite.flipX = false;
                    transform.Translate(m_fSpeed, 0, 0);

                    if (transform.position.x > m_RightBounds.transform.position.x)
                    {
                        m_eBehaviour = Behaviour.Travel_Left;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Nom");

        if (collision.transform.tag == "Player")
        {

        }
    }
}