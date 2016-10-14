using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public GameObject uiPanel;
    void Start()
    {
    }
    public void OnReplayClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnExitClicked()
    {
        SceneManager.LoadScene("GameSelection");
    }

    public void ShowGameOver()
    {
        uiPanel.SetActive(true);
    }
}
