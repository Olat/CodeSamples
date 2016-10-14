using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelThreeManager_DinoFeast : LevelOneManager_DinoFeast 
{
	// Use this for initialization
	void Start () 
    {
        this.totalTrials = 10;
        this.numTrials = totalTrials;
        SetNumTrials(totalTrials);
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.CheckGameCompletion();
	}

    public override void EatHam(GameObject theCurrentHam)
    {
        base.EatHam(theCurrentHam);
        zoneSize = Random.Range(1, 100) % 3;
        zoneYPosition = Random.Range(-40, 90);
        SetActivationZone(zoneSize, zoneYPosition);
    }
   
}
