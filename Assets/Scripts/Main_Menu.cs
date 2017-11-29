using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{

	void Awake()
    {
		
	}
	
	void Update()
    {
		
	}

    public void GameStart()
    {
        Application.LoadLevel("Main");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}