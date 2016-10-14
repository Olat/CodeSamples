using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IsolationMeter_DinoFeast : MonoBehaviour 
{


    private Slider isolationMeter;

    // Use this for initialization
    void Start()
    {
        isolationMeter = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnIsolationChange(float newIsolationLevel)
    {
        isolationMeter.value = newIsolationLevel;
    }
    public void OnFinalIsolation(float finalIsolation)
    {
        isolationMeter.value = 0.0f;
    }

}
