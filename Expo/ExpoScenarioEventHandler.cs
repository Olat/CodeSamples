using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.IO;

public class ExpoScenarioEventHandler : MonoBehaviour, IScenarioEventHandler
{

    public readonly List<string> Messages = new List<string>();
    public ExpoScenarioMetricHandler expoScenarioMetricHandler;
    public AVProMovieCaptureFromCamera avProCapture;
    public UnityTCPClient unityTCPClient;
    public bool areWeRecording;
    public bool startOrStopRecording;


    int eventCode = -1;

    public bool printDebugText = false;

    // Use this for initialization
    void Start()
    {
        areWeRecording = false;
        startOrStopRecording = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleVideoCapture();


        //if (eventCode < 0)
        //    return;

        //switch (eventCode)
        //{
            
        //    default:
        //        break;
        //}

        //eventCode = -1;

    }

    private void HandleVideoCapture()
    {
        if (startOrStopRecording == true && areWeRecording == false)
        {
            avProCapture.StartCapture();
            startOrStopRecording = false;
            areWeRecording = true;
        }
        else if (startOrStopRecording == true && areWeRecording == true)
        {
            avProCapture.StopCapture();
            startOrStopRecording = false;
            areWeRecording = false;

        }
    }


    public void ReceiveEvent(int eventCode)
    {
        this.eventCode = eventCode;

        AddDebugMessage(string.Format("Event code {0} received", eventCode));
    }

    public void StartScenario()
    {
        AddDebugMessage("StartScenario message received");
        expoScenarioMetricHandler.scenarioStartTime = DateTime.Now;
        startOrStopRecording = true;
    }

    public void EndScenario()
    {
        AddDebugMessage("EndScenario message received");
        expoScenarioMetricHandler.scenarioEndTime = DateTime.Now;
        expoScenarioMetricHandler.CalculateEndOfScenarioMetrics();
        startOrStopRecording = true;
        unityTCPClient.SendMessage(SendVideoNameAndTimeStamp());
    }
    private VideoMessage SendVideoNameAndTimeStamp()
    {
        return new VideoMessage(Path.GetFileName(avProCapture.LastFilePath), DateTime.Now);
    }

    #region Debug Text
    private void AddDebugMessage(string msg)
    {
        Debug.Log(msg);

        if (printDebugText)
            Messages.Add(msg);
    }

    // Print Debug Text
    void OnGUI()
    {
        if (!printDebugText)
            return;

        GUI.Label(new Rect(10, 0, 800, 25), "Press the \'A\' key to send a MetricMessage with code 80 and value 32.5");
        GUI.Label(new Rect(10, 20, 800, 25), "Received event messages: ");

        for (int i = 0; i < Messages.Count; i++)
            GUI.Label(new Rect(10, 20 * i + 40, 800, 25), Messages[i]);
    }
    #endregion
}
