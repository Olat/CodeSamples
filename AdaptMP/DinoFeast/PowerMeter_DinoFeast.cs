using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerMeter_DinoFeast : MonoBehaviour 
{


    private Slider powerMeter;
   
	// Use this for initialization
	void Start () 
    {
        powerMeter = this.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void OnPowerChange(float newPowerLevel)
    {
        powerMeter.value = newPowerLevel;
    }
    public void OnFinalPower(float finalPowerForce)
    {
        powerMeter.value = 0.0f;
    }

    
}
