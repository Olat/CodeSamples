using UnityEngine;
using System.Collections;
using System;

public class Weather : MonoBehaviour {

    GameObject rain;
    GameObject lightning;
    bool hasWeatherStarted = false;
    DateTime weaterStartTime;
    bool weatherIncreased = false;
    byte weatherIntensity;
    UDPSender udpSender;
	// Use this for initialization
	void Start () 
    {
        rain = GameObject.Find("Rain Storm");
        lightning = GameObject.Find("Lightning");
        udpSender = new UDPSender(10000);
        weatherIntensity = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //if the weather has begun then increase rain every 5 seconds
        if (hasWeatherStarted && weatherIncreased == false)
        {
            Invoke("IncreaseWeather", 5);
          
            weatherIncreased = true;
        }
	}
    public void ActivateWeather(float time)
    {
        Invoke("ActivateWeather", time);
    }
    void ActivateWeather()
    {
        rain.particleSystem.Play();
        lightning.particleSystem.Play();
        
        TheatricsMessage windMessage = new TheatricsMessage(weatherIntensity, DateTime.Now);
        udpSender.SendMessage(windMessage);
        hasWeatherStarted = true;
        weaterStartTime = DateTime.Now;
    }
    public void ActivateRain(float time)
    {
        Invoke("ActivateRain", time);

    }
    void ActivateRain()
    {
        rain.particleSystem.Play();

    }
    public void ActivateLightning(float time)
    {
        Invoke("ActivateLightning", time);

    }
    void ActivateLightning()
    {
        lightning.particleSystem.Play();

    }
    void IncreaseWeather()
    {
            rain.particleSystem.emissionRate += 200;
            weatherIntensity += 1;
            if (weatherIntensity >= 10)
            {
                weatherIntensity = 10;
            }
            TheatricsMessage windMessage = new TheatricsMessage(weatherIntensity, DateTime.Now);
            udpSender.SendMessage(windMessage);
            if (RenderSettings.fogDensity < 0.08)
            {
                RenderSettings.fogDensity += 0.005f;
            }
                weatherIncreased = false;
    }
}
