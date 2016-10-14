using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SmokeAnimation : MonoBehaviour 
{
    [SerializeField]
    List<Sprite> smokeImages;
    float elapsedTime;
    int currImgIdx = 0;


    Image theSmokeImage;


	// Use this for initialization
	void Start () 
    {
        theSmokeImage = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        elapsedTime += Time.deltaTime;
            
        if(elapsedTime > 0.3f)
        {
            currImgIdx++;
            if (currImgIdx > 2)
                currImgIdx = 0;

            theSmokeImage.overrideSprite = smokeImages[currImgIdx];
            elapsedTime = 0;
        }
	}
}
