using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivationZone : MonoBehaviour
{

    int powerBarTop = 139;
    private Image activationZone;

    // Use this for initialization
    void Start()
    {
        activationZone = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Debug.Log("Increasing the Activation Zone Size");
            SetSize(activationZone.rectTransform.rect.height + 5);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Debug.Log("Decreasing the Activation Zone Size");
            SetSize(activationZone.rectTransform.rect.height - 5);
        }
    }

    public void SetSize(float newHeight)
    {

        Vector3 newPos = activationZone.transform.localPosition;
        newPos.y = powerBarTop - (activationZone.rectTransform.rect.height / 2);

        activationZone.rectTransform.sizeDelta = new Vector2(activationZone.rectTransform.rect.width, newHeight);

        BoxCollider newCollider = this.GetComponent<BoxCollider>();
        newCollider.size = new Vector3(newCollider.size.x,newHeight,newCollider.size.z);
        activationZone.transform.localPosition = newPos;


    }
}
