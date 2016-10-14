using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelFourManager_DinoFeast : LevelManager_DinoFeast 
{
    private int numLives;
    public Text lifeCounter;
    private int scoreNeededForOneUP;
   
	// Use this for initialization
	void Start () 
    {
        base.Start();
        this.totalTrials = 99;
        numLives = 3;
        this.numTrials = totalTrials;
        scoreNeededForOneUP = 3000;
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.CheckGameCompletion();
	}

    public override void EatHam(GameObject theCurrentHam)
    {
        base.EatHam(theCurrentHam);
        numTrials++;
        zoneSize = Random.Range(1, 100) % 3;
        zoneYPosition = Random.Range(-40, 90);
        SetActivationZone(zoneSize, zoneYPosition);
    }
    public void LoseLife()
    {
        numLives--;
        if (numLives <= 0)
        {
            base.DisplayGameOverScreen();
            numLives = 0;
        }
        UpdateLives();
    }

    private void UpdateLives()
    {
        lifeCounter.text = numLives.ToString();
    }

    private void ScoreOneUPCheck()
    {
        if (playerScore >= scoreNeededForOneUP)
        {
            numLives++;
            if (numLives > 5)
                numLives = 5;
            scoreNeededForOneUP *= 3;
        }
    }

  
   
}
