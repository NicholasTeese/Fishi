using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

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
}