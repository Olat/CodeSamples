using UnityEngine;
using System.Collections;

public class ExpoInputHandler : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel("oilrig");
        }
 
	}
}
