using UnityEngine;
using System.Collections;

public class DinoClaw_Boulder : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;

    private bool hasTouchedLava;
    private int health = 3;

    private bool hasTimerStarted = false;
    private float elapsedTimerTime;
    private float timerLength = 1.0f;

    // Update is called once per frame
    void Update()
    {



        if (hasTimerStarted)
        {
            elapsedTimerTime += Time.deltaTime;
        }


        if (elapsedTimerTime >= timerLength)
        {
            LoseHealth(1);
        }


    }
    private void LoseHealth(int amntHealthToLose)
    {
        health -= amntHealthToLose;
        CheckHealth();
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        elapsedTimerTime = 0;
    }

    void OnMouseDown()
    {
        if (hasTouchedLava == false)
        {
            //translate the cubes position from the world to Screen Point
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 5, screenPoint.z));
        }
    }
    void OnMouseUp()
    {
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Lava_DinoClaw"))
        {
            hasTouchedLava = true;
            hasTimerStarted = true;
        }

    }

    void OnMouseDrag()
    {
        if (hasTouchedLava == false)
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

    private void CheckHealth()
    {
        switch (health)
        {
            case 3:
                {
                    this.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                    break;
                }
            case 2:
                {
                    this.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);

                    break;
                }
            case 1:
                {
                    this.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
