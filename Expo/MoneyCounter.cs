using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour {

	// Use this for initialization
    public GameObject counterPanel;
    public Text counterText;
    DateTime startTime;
    public GameObject theLever;
    bool moneyCounterStarted = false;

	void Start () 
    {
        counterText.text = "";
        counterPanel.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (theLever.GetComponent<Lever>().IsCompressorActive() == false && moneyCounterStarted == false)
        {
            moneyCounterStarted = true;
            startTime = DateTime.Now;
            counterPanel.SetActive(true);

        }
        else if (theLever.GetComponent<Lever>().IsCompressorActive() == true && moneyCounterStarted == true)
        {
            moneyCounterStarted = false;
        }
        else if(theLever.GetComponent<Lever>().IsCompressorActive() == false && moneyCounterStarted == true)
        {
            double countervalue =(DateTime.Now - startTime).TotalSeconds;
            countervalue =(int)(countervalue * 50);
            counterText.text = countervalue.ToString("C2");
        }

	}
}
