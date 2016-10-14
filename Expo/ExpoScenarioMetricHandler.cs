using UnityEngine;
using System.Collections;
using System;

public class ExpoScenarioMetricHandler : MonoBehaviour
{

    public DateTime leverOffTime;
    public DateTime leverOnTime;
    public DateTime wheelClosedTime;
    public DateTime scenarioStartTime;
    public DateTime scenarioEndTime;
    public UnityTCPClient unityTCPClient;

    GameObject lever;
    GameObject wheel;
    Wheel wheelScript;
    private bool isLeverOff = false;
    bool wheelValveCloseTimeSet = false;

    // Use this for initialization
    void Start()
    {
        lever = GameObject.Find("Lever_GO");
        wheel = GameObject.Find("Wheel_GO");
        wheelScript = wheel.GetComponent<Wheel>();

    }

    // Update is called once per frame
    void Update()
    {

        if (lever.GetComponent<Lever>().IsCompressorActive() == false && isLeverOff == false)
        {
            SetLeverOffTime();
        }
        else if (lever.GetComponent<Lever>().IsCompressorActive() == true && isLeverOff == true && leverOffTime != leverOnTime)
        {
            SetLeverOnTime();
        }

        if (wheelScript.IsWheelValveClosed == true && wheelValveCloseTimeSet == false)
        {
            SetWheelValveClosedTime();
            wheelValveCloseTimeSet = true;
        }



    }

    private void SetLeverOffTime()
    {
        leverOffTime = DateTime.Now;
        isLeverOff = true;
        unityTCPClient.SendMessage(CreateScenarioStartTimeToLeverOffMetric());
    }

    private void SetLeverOnTime()
    {

        leverOnTime = DateTime.Now;
        isLeverOff = false;
        unityTCPClient.SendMessage(CreateScenarioStartTimeToLeverOnMetric());

    }

    private void SetWheelValveClosedTime()
    {

        wheelClosedTime = DateTime.Now;
        unityTCPClient.SendMessage(CreateScenarioStartTimeToWheelValveCLosedMetric());

    }

    public void CalculateEndOfScenarioMetrics()
    {
        unityTCPClient.SendMessage(CreateTotalScenarioTimeMetric());

    }

    private MetricMessage CreateScenarioStartTimeToLeverOffMetric()
    {
        return new MetricMessage(0, Math.Round((leverOffTime - scenarioStartTime).TotalSeconds, 2), DateTime.Now);
    }
    private MetricMessage CreateScenarioStartTimeToLeverOnMetric()
    {
        return new MetricMessage(1, Math.Round((leverOnTime - scenarioStartTime).TotalSeconds, 2), DateTime.Now);
    }
    private MetricMessage CreateScenarioStartTimeToWheelValveCLosedMetric()
    {
        return new MetricMessage(2, Math.Round((wheelClosedTime - scenarioStartTime).TotalSeconds, 2), DateTime.Now);
    }
    private MetricMessage CreateTotalScenarioTimeMetric()
    {
        return new MetricMessage(3, Math.Round((scenarioEndTime - scenarioStartTime).TotalSeconds, 2), DateTime.Now);
    }
}
