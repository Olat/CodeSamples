using UnityEngine;
using System.Collections;
public enum PowerMeterPosition { BelowSweetSpot, InSweetSpot, AboveSweetSpot };

public class PowerMeterIcon_DinoFeast : MonoBehaviour 
{

    [SerializeField]
    private GameObject sweetSpot;
    private GameObject powerMeterIcon;
    static PowerMeterPosition isMeterInSweetSpot;


    void Start()
    {
        powerMeterIcon = this.gameObject;
    }

    void FixedUpdate()
    {
        SweetSpotCheck();
    }

    public void SweetSpotCheck()
    {

        if (powerMeterIcon.GetComponent<Collider>().bounds.Intersects(sweetSpot.GetComponent<Collider>().bounds))
        {
            isMeterInSweetSpot = PowerMeterPosition.InSweetSpot;
        }
        else
        {
            isMeterInSweetSpot = PowerMeterPosition.BelowSweetSpot;
        }
        
 
    }

    static public PowerMeterPosition IsInSweetSpot() { return isMeterInSweetSpot; }
}
