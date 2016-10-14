using UnityEngine;
using System.Collections;

public class LoadMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void LoadMainMenuScene()
    {
        Application.LoadLevel("MainMenu");
    }
}
