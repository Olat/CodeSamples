using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelOneManager_DinoFeast: LevelManager_DinoFeast 
{

	// Use this for initialization
	void Start () 
    {

        this.totalTrials = 10;
        this.numTrials = totalTrials;
        SetNumTrials(totalTrials);
	}
	
    public override void EatHam(GameObject theCurrentHam)
    {
        base.EatHam(theCurrentHam);
    }

   

   
}
