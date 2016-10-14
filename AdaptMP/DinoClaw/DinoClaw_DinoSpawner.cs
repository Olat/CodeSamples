using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DinoClaw_DinoSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject dinoPrefab;
    [SerializeField]
    Transform northSpawn;
    [SerializeField]
    Transform eastSpawn;
    [SerializeField]
    Transform southSpawn;
    [SerializeField]
    Transform westSpawn;
    [SerializeField]
    private Text dinoCountDisplay;


    private int dinoCount = 4;

    float elapsedTime;
    float spawnTimer = 4;

    // Use this for initialization
    void Start()
    {
        dinoCountDisplay.text = dinoCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        elapsedTime += Time.deltaTime;

        if (CheckDinoSpawnTimer() == true)
        {
            SpawnDino();
        }

    }
    /// <summary>
    /// Checks the elapsed time with the SpawnTimer.
    /// If the elapsed time has passed the SpawnTimer it returns true.
    /// </summary>
    /// <returns>
    /// True - Elapsed Time has passed the Spawn Time
    /// False - Elapsed Time has not passed the Spawn time.
    /// </returns>
    private bool CheckDinoSpawnTimer()
    {
        if (elapsedTime >= spawnTimer)
        {
            elapsedTime = 0.0f;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Spawns a Dino at the hatchery with a scale of .1 that grows to full size as it leaves the hatchery.
    /// Dino will move into position and go idle.
    /// </summary>
    private void SpawnDino()
    {
        if (dinoCount <= 0)
        { 
            return;
        }
        int spawn = Random.Range(0,4);

        switch(spawn)
        {
            case 0:
                {
                    //North Spawn
                    GameObject newDino = Instantiate(dinoPrefab, northSpawn.position, northSpawn.rotation) as GameObject;
                    dinoCount--;
                    dinoCountDisplay.text = dinoCount.ToString();

                    break;
                }
            case 1:
                {
                    //South Spawn
                    GameObject newDino = Instantiate(dinoPrefab, southSpawn.position, southSpawn.rotation) as GameObject;
                    dinoCount--;
                    dinoCountDisplay.text = dinoCount.ToString();
                    break;
                }
            case 2:
                {
                    //East Spawn
                    GameObject newDino = Instantiate(dinoPrefab, eastSpawn.position, eastSpawn.rotation) as GameObject;
                    dinoCount--;
                    dinoCountDisplay.text = dinoCount.ToString();
                    break;
                }
            case 3:
                {
                    //West Spawn
                    GameObject newDino = Instantiate(dinoPrefab, westSpawn.position, westSpawn.rotation) as GameObject;
                    dinoCount--;
                    dinoCountDisplay.text = dinoCount.ToString();
                    break;
                }
            default:
                {

                    break;
                }
        }


        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Lava_DinoClaw"))
        {
            Destroy(this.gameObject);
            
        }
       
    }
}
