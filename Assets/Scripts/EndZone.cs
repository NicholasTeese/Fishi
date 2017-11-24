using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : GameManager
{
    protected bool m_bTerminate = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.m_bEndGame = true;
    }
}