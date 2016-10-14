using UnityEngine;
using System.Collections;

public class LavaFlow_DinoClaw : MonoBehaviour 
{
    [SerializeField]
    private float lavaSpeed = 0.5f;

    bool isFlowing = true;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (isFlowing)
        {
            AnimateLava();
        }
	}
    private void AnimateLava()
    {
        Vector3 updatedPosition = gameObject.transform.position;
        updatedPosition.z = updatedPosition.z - (lavaSpeed * Time.deltaTime);
        gameObject.transform.position = updatedPosition;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("DinoClaw_River") || col.gameObject.tag.Equals("DinoClaw_Boulder"))
        {
            isFlowing = false;
        }
    }
    public void OnCollisionExit(Collision col)
    {
            isFlowing = true;
    } 
    
}
