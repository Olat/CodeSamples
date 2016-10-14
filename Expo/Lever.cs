using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lever : MonoBehaviour 
{

    float leverValue;
    private bool isCompressorActive;
    bool compressorCurrState;
    bool isWeatherOn;
    public Light circuitBoardLight;
    List<string> joynames;
    string axesName;
    public GameObject weather;

	// Use this for initialization
	void Start () 
    {
        joynames = new List<string>(Input.GetJoystickNames());
        if (joynames[0].Contains("Saitek"))
            axesName = "Joystick" + 0;
        else
            axesName = "Joystick" + 1;

        

        isWeatherOn = false;
        leverValue = 0.0f;
        isCompressorActive = compressorCurrState = true;
	}
	
	// Update is called once per frame
	void Update () 
    {

        //Lever Values are a bit odd as they are mapped to the X Axis. ,
        // Values:
        // -1 = Throttle at 100%
        //  0 = Throttle at 50%
        //  1 = Throttle at 0%
        leverValue = Input.GetAxis(axesName);
        Debug.Log("Lever Value = " + leverValue);

        if(leverValue<= -0.9f)
        {
            isCompressorActive = true;
            circuitBoardLight.intensity = 0.6f;

        }
        else if(leverValue >= 0.9f)
        {
            isCompressorActive = false;
            circuitBoardLight.intensity = 0f;
        }

        if (compressorCurrState != isCompressorActive)
        {
            compressorCurrState = isCompressorActive;

            if(isCompressorActive == false && isWeatherOn == false)
            {
                weather.GetComponent<Weather>().ActivateWeather(3.0f);
                isWeatherOn = true;
            }

            if (isCompressorActive)
                Debug.Log("Compressor Activated");
            else
                Debug.Log("Compressor Deacitivated");

        }

	
	}
    public bool IsCompressorActive() { return isCompressorActive; }
}
