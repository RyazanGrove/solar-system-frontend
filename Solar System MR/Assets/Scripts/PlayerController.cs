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
    private GameObject submitAnswerButtonObject;

    private ExperimentController experimentController;

    private GameObject informationTask;
    private GameObject informationWrongAnswer;
    private GameObject informationCorrectAnswer;

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
        if (isLocalPlayer)
        {
            submitAnswerButtonObject.SetActive(false);
            informationTask.SetActive(false);
        }

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
        if (isLocalPlayer)
        {
            submitAnswerButtonObject.SetActive(true);
            tryAgainButtonObject.SetActive(false);
            informationTask.SetActive(true);
            informationCorrectAnswer.SetActive(false);
            informationWrongAnswer.SetActive(false);
        }
        experimentController.ResetExperement();
    }

    void Start()
    {
        experimentController = GameObject.FindGameObjectWithTag("ExperimentControl").GetComponent<ExperimentController>();

        informationTask = GameObject.FindGameObjectWithTag("InformationTask");
        informationWrongAnswer = GameObject.FindGameObjectWithTag("InformationWrongAnswer");
        informationCorrectAnswer = GameObject.FindGameObjectWithTag("InformationCorrectAnswer");

        

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
            
            submitAnswerButtonObject = GameObject.FindGameObjectWithTag("SubmitAnswerButton");
            submitAnswerButton = submitAnswerButtonObject.GetComponent<Button>();
            submitAnswerButton.onClick.AddListener(() => { SubmitAnswer(); });
            tryAgainButtonObject = GameObject.FindGameObjectWithTag("TryAgainButton");
            tryAgainButton = tryAgainButtonObject.GetComponent<Button>();
            tryAgainButton.onClick.AddListener(() => { ResetExperement(); });
            tryAgainButtonObject.SetActive(false);

            informationTask.SetActive(true);
            informationWrongAnswer.SetActive(false);
            informationCorrectAnswer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(experimentController.getUsersAnswer() != 0)
        {
            if (isLocalPlayer)
            {
                if (experimentController.getUsersAnswer() == 1)
                {
                    informationCorrectAnswer.SetActive(true);
                }
                else
                {
                    informationWrongAnswer.SetActive(true);
                }

                tryAgainButtonObject.SetActive(true);
                experimentController.setUsersAnswer(0);
            }
        }
    }
}
