using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DinoClaw_LevelManager : BaseLevelManager
{
   
    public List<GameObject> dinosInLevel;


    [SerializeField]
    protected Text gameoverScore;
    [SerializeField]
    protected Text gameoverPercent;

    [SerializeField]
    protected Text totalDinosText;
    [SerializeField]
    protected Text savedDinosText;

    [SerializeField]
    protected GameObject gameOverScreen;

    protected int totalDinosInLevel;
    protected int totalSavedDinosInLevel = 0;
    protected int totalDeadDinosInLevel = 0;
	// Use this for initializationz
	void Start () 
    {
      
        BaseLevelManager.UpdateScore();
        totalDinosInLevel = dinosInLevel.Count;
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
    public void EggSaved()
    {
        DinoClaw_LevelManager.IncreaseMultiplier();
        DinoClaw_LevelManager.IncreaseScore();
        totalSavedDinosInLevel++;
        savedDinosText.text = totalSavedDinosInLevel.ToString();
        GameOverCheck();
    }
    public void DinoDied()
    {
        totalDeadDinosInLevel++;
        GameOverCheck();

    }
    public void EggDied()
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
