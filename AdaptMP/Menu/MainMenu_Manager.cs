using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public class MainMenu_Manager : MonoBehaviour
{

    #region MenuObjects
    [SerializeField]
    private GameObject LoginMenu;
    [SerializeField]
    private GameObject SpashScreen;
    [SerializeField]
    private GameObject ProfileMenu;
    public InputField uniqueIDInput;
    public InputField userNameInput;

    public Text fullName;
    public Text DOB;
    string urlLink = "https://adaptmpapidev.designinteractive.net";

    #endregion

    float timer = 3.0f;
    bool splashScreenShown = false;

    string uniqueID = "";
    UTF8Encoding encoding = new UTF8Encoding();
    Hashtable postHeader = new Hashtable();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!splashScreenShown)
        {
            SplashScreen();
        }
    }

    public void ReturnToLogin()
    {
        ProfileMenu.SetActive(false);
        LoginMenu.SetActive(true);
    }
    public void DisplayProfileView()
    {
        LoginMenu.SetActive(false);
        //SetUniqueID(uniqueIDInput.text);


        //HACK: FIX THIS
        string userNameText = userNameInput.text;
        string uniqueIDText = uniqueIDInput.text;

        //do web stuff
        //fullName = WebAPI Call;
        //DOB = WebAPI Call;

        //Sample post request
        string PostTokenRequestCall = urlLink + "/api/trainees/" + userNameText + "/traineeTokenGenerationCommand?passcode=" + uniqueIDText;

        //Dictionary<string, string> postParam = new Dictionary<string, string>();
        //postParam.Add("Content-Type", "application/json");
        //postParam.Add("Content-Length", "1");

        Debug.Log(PostTokenRequestCall);

        WWW request = new WWW(PostTokenRequestCall, Encoding.UTF8.GetBytes("N/A"));

        //Sample Get request
        //string getRequest = "https://adaptmpapidev.designinteractive.net/api/trainees";
        //WWW request = new WWW(getRequest);

        StartCoroutine(WaitForRequest(request));
        //// webstuff.
        // //Fiddler.
        ProfileMenu.SetActive(true);


    }
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    public void SplashScreen()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            SpashScreen.SetActive(false);
            LoginMenu.SetActive(true);
            splashScreenShown = true;
        }
    }

    private void SetUniqueID(string id)
    {
        uniqueID = id;
    }
}
