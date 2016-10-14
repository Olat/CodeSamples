using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wheel : MonoBehaviour
{

    float wheelValue;
    public bool IsWheelValveClosed { get; private set; }
    bool valveCurrState;
    int wheelIndex;
    int newConstantForce = 0;
    int currentConstantForce = 0;
    LogitechGSDK.LogiControllerPropertiesData properties;
    List<string> joynames;
    string axesName;
    // Use this for initialization
    void Start()
    {
        LogitechGSDK.LogiSteeringInitialize(true);

        properties = new LogitechGSDK.LogiControllerPropertiesData();
        properties.wheelRange = 900;

        LogitechGSDK.LogiSetPreferredControllerProperties(properties);

        wheelValue = 0.0f;
        valveCurrState = IsWheelValveClosed = true;
        for (int i = 0; i < 10; i++)
        {
            if (LogitechGSDK.LogiIsConnected(i))
                wheelIndex = i;
        }

        joynames = new List<string>(Input.GetJoystickNames());
        if (joynames[0].Contains("G27"))
            axesName = "Joystick" + 0;
        else
            axesName = "Joystick" + 1;
    }

    // Update is called once per frame
    void Update()
    {
		//Lever Values are a bit odd as they are mapped to the X Axis. 
		// Values:
		// -1 = Throttle at 100%
		//  0 = Throttle at 50%
		//  1 = Throttle at 0%
		wheelValue = Input.GetAxis(axesName);
		LogitechGSDK.LogiUpdate();
        Debug.Log("Wheel Value =" + wheelValue);



        if (valveCurrState != IsWheelValveClosed)
        {
            valveCurrState = IsWheelValveClosed;

            if (IsWheelValveClosed)
                Debug.Log("Valve Closed");
            else
                Debug.Log("Valve Open");
        }

		//Debug.Log(wheelValue);

        if (wheelValue >= 0.85f)
        {
            IsWheelValveClosed = true;
        }
        else if (wheelValue <= -0.85f)
        {
            IsWheelValveClosed = false;
        }

    }
   
}
