using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{

    [Serializable]
    public class FloatEvent : UnityEvent<float> { }
    public FloatEvent JumpForceCallback;
    public FloatEvent PowerMeterCallback;
    public FloatEvent IsolationMeterCallback;

    [SerializeField]
    private GameObject theDino;
    [SerializeField]
    private Transform hamSpawnLocation;
    [SerializeField]
    float neededTimeInSweetSpot = 0.5f;
    [SerializeField]
    private float neededTimeInIsolation = 0.5f;

    [SerializeField]
    private GameObject isolationBar = null;


    private bool onGround = true;
    private float jumpforce = 35000;
    private float heldTime = 0;
    private float buttonPressedTime;

    private float currentPower = 0.0f;

    private Rigidbody dinoBody;

    private float currentTimeInSweetSpot = 0.0f;
    private bool enteredSweetSpot = false;
    public LevelManager_DinoFeast levelManager;
    public LevelFourManager_DinoFeast levelFourManager;
    private float currentIsolation;
    private bool enteredIsolationThreshold;
    private float currentTimeInIsolation;
    private bool isolationReady;

    // Use this for initialization
    void Start()
    {

        dinoBody = theDino.GetComponent<Rigidbody>();
        theDino.GetComponent<Dino_DinoFeast>().dinoGroundedDelegate = OnDinoGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        IsolationCheck();
    }


    private Vector3 GetNeededHamVelocity()
    {
        Bounds dinoBounds = theDino.GetComponent<Collider>().bounds;
        float dinoTop = dinoBounds.max.y - theDino.transform.position.y;
        float neededHeight = hamSpawnLocation.position.y - dinoTop;

        float yVelocity = Mathf.Sqrt(2 * -Physics.gravity.y * neededHeight);
        return new Vector3(0, yVelocity, 0);
    }

    public void HandleInput()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("GameSelection");

        //Turned off Input for Keyboard
        //
        //if (onGround == true && levelManager.GetCurrentHam() != null)
        //{
        //    if (Input.GetKey(KeyCode.Space))
        //    {
        //        currentPower += Time.deltaTime;
        //        PowerMeterCallback.Invoke(currentPower);
        //    }

        //    if (Input.GetKeyUp(KeyCode.Space))
        //    {
        //        OnJump();

        //    }
        //}
    }

    public void SetCurrentPower(float power)
    {
        if (true == onGround)
        {
            this.currentPower = power;
            PowerMeterCallback.Invoke(currentPower);
            ActivationThresholdCheck();
        }
    }

    public void SetCurrentIsolation(float iso)
    {
        if (true == onGround)
        {
            this.currentIsolation = iso;
            IsolationMeterCallback.Invoke(currentIsolation);
            
        }
    }

    public void ActivationThresholdCheck()
    {
        if (PowerMeterIcon_DinoFeast.IsInSweetSpot() == PowerMeterPosition.InSweetSpot)
        {
            enteredSweetSpot = true;
            currentTimeInSweetSpot += Time.deltaTime;
          
            if (currentTimeInSweetSpot >= neededTimeInSweetSpot)
            {
                OnJump();
            }
        }

        else
        {
            if (true == enteredSweetSpot)
            {
                OnJump();
            }
        }
    }

    private bool IsolationCheck()
    {
        if (IsolationMeterIcon_DinoFeast.IsInIsolationZone() == IsolationMeterPosition.InsideIsolationZone)
        {
            currentTimeInIsolation += Time.deltaTime;
            if (currentTimeInIsolation >= neededTimeInIsolation)
            {
                isolationReady = true;
            }

            else
            {
                isolationReady = false;

            }
        }
        else
        {
            currentTimeInIsolation = 0.0f;
            isolationReady = false;
        }

        return isolationReady;
    }

    public void OnJump()
    {
        Vector3 jumpVelocity;

        #region No isolation Bar
        if (isolationBar == null) // If the game does not have an Isolation bar (Lvl 1 or 2)
        {
            switch (PowerMeterIcon_DinoFeast.IsInSweetSpot())
            {
                case PowerMeterPosition.InSweetSpot:
                    {
                        jumpVelocity = GetNeededHamVelocity();
                        break;
                    }
                case PowerMeterPosition.AboveSweetSpot:
                    {
                        jumpVelocity = GetNeededHamVelocity() * 1.5f;
                        break;
                    }
                case PowerMeterPosition.BelowSweetSpot:
                default:
                    {
                        jumpVelocity = GetNeededHamVelocity() * 0.75f;
                        break;
                    }

            }
        }
        #endregion
        else
        {
            switch (PowerMeterIcon_DinoFeast.IsInSweetSpot())
            {
                case PowerMeterPosition.InSweetSpot:
                    {
                        jumpVelocity = GetNeededHamVelocity();
                        break;
                    }
                case PowerMeterPosition.AboveSweetSpot:
                    {
                        if (levelManager.isLevelEndless)
                            levelFourManager.LoseLife();

                        jumpVelocity = GetNeededHamVelocity() * 1.5f;
                        break;
                    }
                case PowerMeterPosition.BelowSweetSpot:
                default:
                    {
                        if (levelManager.isLevelEndless)
                            levelFourManager.LoseLife();

                        jumpVelocity = GetNeededHamVelocity() * 0.75f;
                        break;
                    }

            }
            //Threshold Failed but Power Checked = FAIL
            if (IsolationCheck() == false && PowerMeterIcon_DinoFeast.IsInSweetSpot() == PowerMeterPosition.InSweetSpot)
            {
                if (levelManager.isLevelEndless)
                    levelFourManager.LoseLife();

                jumpVelocity = GetNeededHamVelocity() * 0.75f;
            }
        }

        //Dont think this is needed.
        //else
        //{
        //    if (levelManager.isLevelEndless)
        //        levelFourManager.LoseLife();
        //    jumpVelocity = GetNeededHamVelocity() * 0.75f;

        //}
        JumpForceCallback.Invoke(jumpVelocity.y);
        enteredSweetSpot = false;

        currentPower = 0.0f;
        currentIsolation = 0.0f;
        currentTimeInIsolation = 0;
        currentTimeInSweetSpot = 0.0f;

        PowerMeterCallback.Invoke(currentPower);
        isolationReady = false;
        onGround = false;
    }


    public void OnDinoGrounded()
    {
        Debug.Log("Dino has been Grounded.");
        onGround = true;
    }




}
