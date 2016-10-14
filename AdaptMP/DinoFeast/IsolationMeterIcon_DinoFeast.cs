using UnityEngine;
using System.Collections;
public enum IsolationMeterPosition { OutsideIsolationZone, InsideIsolationZone };

public class IsolationMeterIcon_DinoFeast : MonoBehaviour 
{

    [SerializeField]
    private GameObject isolationZone;
    private GameObject isolationMeterIcon;
    static IsolationMeterPosition isMeterInIsolationZone;


    void Start()
    {
        isolationMeterIcon = this.gameObject;
    }

    void FixedUpdate()
    {
        IsolationZoneCheck();
    }

    public void IsolationZoneCheck()
    {

        if (isolationMeterIcon.GetComponent<Collider>().bounds.Intersects(isolationZone.GetComponent<Collider>().bounds))
        {
            isMeterInIsolationZone = IsolationMeterPosition.InsideIsolationZone;
        }
        else
        {
            isMeterInIsolationZone = IsolationMeterPosition.OutsideIsolationZone;
        }


    }

    static public IsolationMeterPosition IsInIsolationZone() { return isMeterInIsolationZone; }
}
