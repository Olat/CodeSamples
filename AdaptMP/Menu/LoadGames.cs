using UnityEngine;
using System.Collections;

public class LoadGames : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void LoadVolcano()
    {
        Application.LoadLevel("VolcanoCrush");
    }
    public void LoadSprint()
    {
        Application.LoadLevel("Sprint");
    }
    public void LoadFeast()
    {
        Application.LoadLevel("VolcanoCrush");
    }
    public void LoadPickup()
    {
        Application.LoadLevel("PterodactylPickup");
    }

}
