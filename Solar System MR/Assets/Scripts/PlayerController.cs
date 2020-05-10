using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    private Button increaseButton;
    private Button decreaseButton;

    private ExperimentController experimentController;

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

    void Start()
    {
        experimentController = GameObject.FindGameObjectWithTag("ExperimentControl").GetComponent<ExperimentController>();

        if (isLocalPlayer)
        {
            increaseButton = GameObject.FindGameObjectWithTag("IncreaseAsteroidSizeButton").GetComponent<Button>();
            increaseButton.onClick.AddListener(() => { IncreaseAsteroidSize(); });
            decreaseButton = GameObject.FindGameObjectWithTag("DecreaseAsteroidSizeButton").GetComponent<Button>();
            decreaseButton.onClick.AddListener(() => { DecreaseAsteroidSize(); });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
