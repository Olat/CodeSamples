using UnityEngine;
using System.Collections;

public class LevelSelection : MonoBehaviour
{
 

    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
}
