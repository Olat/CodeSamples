using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DinoClaw_LevelTwoManager : DinoClaw_LevelManager
{


    int numDinoEggs;
	// Use this for initializationz
	void Start () 
    {
        numDinoEggs = 1;
        BaseLevelManager.UpdateScore();
        totalDinosInLevel = dinosInLevel.Count + numDinoEggs;
        if (totalDinosInLevel <= 0)
            Debug.LogError("Dino count is not valid.");
        totalDinosText.text = totalDinosInLevel.ToString();
        savedDinosText.text = totalSavedDinosInLevel.ToString();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void DinoSaved()
    {
        DinoClaw_LevelManager.IncreaseScore();
        totalSavedDinosInLevel++;
        savedDinosText.text = totalSavedDinosInLevel.ToString();
        GameOverCheck();
    }
    public void DinoDied()
    {
        totalDeadDinosInLevel++;
        ResetMultiplier();
        GameOverCheck();

    }
    public void GameOverCheck()
    {
        if(totalSavedDinosInLevel + totalDeadDinosInLevel == totalDinosInLevel)
            DisplayGameOverScreen();
    }

    public void RestartLevel()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("GameSelection");

    }

    public void DisplayGameOverScreen()
    {
        gameoverScore.text = playerScore.ToString();
        float percent = (float)totalSavedDinosInLevel / (float)totalDinosInLevel * 100;
        gameoverPercent.text = percent.ToString();
        gameOverScreen.SetActive(true);
    }
   
}
