using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    private Button increaseSizeButton;
    private Button decreaseSizeButton;
    private Button increaseSpeedButton;
    private Button decreaseSpeedButton;
    private Button submitAnswerButton;
    private Button tryAgainButton;

    private GameObject tryAgainButtonObject;

    private ExperimentController experimentController;

    //size
    public void IncreaseAsteroidSize()
    {
        CmdIncreaseAsteroidSize();
    }

    [Command]
    public void CmdIncreaseAsteroidSize()
    {
        RpcIncreaseAsteroidSize();
    }
    
    [ClientRpc]
    public void RpcIncreaseAsteroidSize()
    {
        experimentController.IncreaseAsteroidSize();
    }

    public void DecreaseAsteroidSize()
    {
        CmdDecreaseAsteroidSize();
    }

    [Command]
    public void CmdDecreaseAsteroidSize()
    {
        RpcDecreaseAsteroidSize();
    }

    [ClientRpc]
    public void RpcDecreaseAsteroidSize()
    {
        experimentController.DecreaseAsteroidSize();
    }

    //speed
    public void IncreaseAsteroidSpeed()
    {
        CmdIncreaseAsteroidSpeed();
    }

    [Command]
    public void CmdIncreaseAsteroidSpeed()
    {
        RpcIncreaseAsteroidSpeed();
    }

    [ClientRpc]
    public void RpcIncreaseAsteroidSpeed()
    {
        experimentController.IncreaseAsteroidSpeed();
    }

    public void DecreaseAsteroidSpeed()
    {
        CmdDecreaseAsteroidSpeed();
    }

    [Command]
    public void CmdDecreaseAsteroidSpeed()
    {
        RpcDecreaseAsteroidSpeed();
    }

    [ClientRpc]
    public void RpcDecreaseAsteroidSpeed()
    {
        experimentController.DecreaseAsteroidSpeed();
    }

    //sumbit/repeat
    public void SubmitAnswer()
    {
        CmdSubmitAnswer();
    }

    [Command]
    public void CmdSubmitAnswer()
    {
        RpcSubmitAnswer();
    }

    [ClientRpc]
    public void RpcSubmitAnswer()
    {
        experimentController.StartAsteroidMovement();
    }

    public void ResetExperement()
    {
        CmdResetExperement();
    }

    [Command]
    public void CmdResetExperement()
    {
        RpcResetExperement();
    }

    [ClientRpc]
    public void RpcResetExperement()
    {
        experimentController.ResetExperement();
    }

    void Start()
    {
        experimentController = GameObject.FindGameObjectWithTag("ExperimentControl").GetComponent<ExperimentController>();

        if (isLocalPlayer)
        {
            increaseSizeButton = GameObject.FindGameObjectWithTag("IncreaseAsteroidSizeButton").GetComponent<Button>();
            increaseSizeButton.onClick.AddListener(() => { IncreaseAsteroidSize(); });
            decreaseSizeButton = GameObject.FindGameObjectWithTag("DecreaseAsteroidSizeButton").GetComponent<Button>();
            decreaseSizeButton.onClick.AddListener(() => { DecreaseAsteroidSize(); });
            increaseSpeedButton = GameObject.FindGameObjectWithTag("IncreaseAsteroidSpeedButton").GetComponent<Button>();
            increaseSpeedButton.onClick.AddListener(() => { IncreaseAsteroidSpeed(); });
            decreaseSpeedButton = GameObject.FindGameObjectWithTag("DecreaseAsteroidSpeedButton").GetComponent<Button>();
            decreaseSpeedButton.onClick.AddListener(() => { DecreaseAsteroidSpeed(); });
            //change Image and Interactible
            submitAnswerButton = GameObject.FindGameObjectWithTag("SubmitAnswerButton").GetComponent<Button>();
            submitAnswerButton.onClick.AddListener(() => { SubmitAnswer(); });
            tryAgainButtonObject = GameObject.FindGameObjectWithTag("TryAgainButton");
            tryAgainButton = tryAgainButtonObject.GetComponent<Button>();
            tryAgainButton.onClick.AddListener(() => { ResetExperement(); });
            tryAgainButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(experimentController.getUsersAnswer() != 0)
        {
            tryAgainButton.interactable = true;
        }
    }
}
