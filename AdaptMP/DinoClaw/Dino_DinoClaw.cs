using UnityEngine;
using System.Collections;

public class Dino_DinoClaw : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    public bool isDinoSafe;
    public DinoClaw_LevelManager levelmanager;
    bool isDed;

    void Start()
    {
        isDinoSafe = false;
        isDed = false;
        levelmanager = GameObject.Find("ScriptObject").GetComponent<DinoClaw_LevelManager>();
    }
    void FixedUpdate()
    {

    }

    void OnMouseDown()
    {
        if (!isDinoSafe)
        {
            //translate the cubes position from the world to Screen Point
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 5, screenPoint.z));
        }

    }

    void OnMouseUp()
    {
        if (!isDinoSafe)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
        }
    }

    void OnMouseDrag()
    {
        if (!isDinoSafe)
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
        if (isDed == false)
        {
            if (col.gameObject.tag.Equals("Lava_DinoClaw"))
            {
                isDed = true;
                this.gameObject.SetActive(false);
                levelmanager.DinoDied();
            }
            else if (col.gameObject.tag.Equals("DinoClaw_River"))
            {
                isDed = true;
                this.gameObject.SetActive(false);
                levelmanager.DinoDied();

            }
            else if (col.gameObject.tag.Equals("DinoClaw_SafeZone"))
            {
                isDinoSafe = true;
                levelmanager.DinoSaved();
            }
        }
            
    }

}
