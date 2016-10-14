using UnityEngine;
using System.Collections;

public class DinoClaw_Egg : MonoBehaviour 
{

    float elapsedTime;
    float twitchTimer = 1;
    int twitchDirection;
    int twitchSpeed = 1;
    private bool isEggSafe;
    private Vector3 screenPoint;
    private Vector3 offset;
    public DinoClaw_LevelManager levelmanager;
    public Animator eggAnim;
    bool isDed;

    // Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        Twitch();
	}

    private void Twitch()
    {
        twitchDirection = (int)Random.Range(0, 2);
        eggAnim.SetInteger("TwitchDirection",twitchDirection);
        

    }

    void OnMouseDown()
    {
        if (!isEggSafe)
        {
            //translate the cubes position from the world to Screen Point
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 5, screenPoint.z));
        }

    }

    void OnMouseUp()
    {
        if (!isEggSafe)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
        }
    }

    void OnMouseDrag()
    {
        if (!isEggSafe)
        {
            Plane plane = new Plane(Vector3.up, new Vector3(0, 5, 0));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                transform.position = ray.GetPoint(distance);
            }
        }

    }
    void OnCollisionEnter(Collision col)
    {
        if (!isDed)
        {

            if (col.gameObject.tag.Equals("Lava_DinoClaw"))
            {
                this.gameObject.SetActive(false);
                levelmanager.EggDied();
            }
            if (col.gameObject.tag.Equals("DinoClaw_River"))
            {
                this.gameObject.SetActive(false);
                levelmanager.EggDied();
            }
            if (col.gameObject.tag.Equals("DinoClaw_SafeZone"))
            {
                isEggSafe = true;
                levelmanager.EggSaved();
            }
        }
    }


}
