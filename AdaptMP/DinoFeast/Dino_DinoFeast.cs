using UnityEngine;
using System.Collections;

public class Dino_DinoFeast : MonoBehaviour 
{

    public delegate void SetDinoGrounded();
    public SetDinoGrounded dinoGroundedDelegate;


    private LevelManager_DinoFeast levelManager;

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager_DinoFeast>();
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            if (dinoGroundedDelegate != null)
            {
                Debug.Log("Dino Collided with Ground");
                dinoGroundedDelegate();
                this.gameObject.GetComponent<Animator>().SetBool("jumping", false);
            }
        }
        if (col.gameObject.tag.Equals("DinoFeast_Ham") || col.gameObject.tag.Equals("DinoFeast_GoldHam"))
        {
            Debug.Log("Dino Collided with Ham");
            levelManager.EatHam(levelManager.GetCurrentHam());
        }
    }

    public void DinoJump(float jumpforce)
    {
        this.gameObject.GetComponent<Animator>().SetBool("jumping", true);
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, jumpforce, 0);
    }

    
}
