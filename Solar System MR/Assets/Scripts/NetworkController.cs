using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkController : NetworkBehaviour
{
    [SerializeField]
    private string networkAddress;

    NetworkManager networkManager;

    void Start()
    {
        networkManager = GetComponent<NetworkManager>();

        if (networkAddress.Equals("") || networkAddress.Equals("localhost"))
        {
            StartHost();
        }
        else
        {
            networkManager.networkAddress = networkAddress;
            StartClient();
        }
    }
    public void StartHost()
    {
        Debug.Log("Starting host");
        networkManager.StartHost();
        if (networkManager.spawnPrefabs.Count > 0)
        {

            if (networkManager.spawnPrefabs[0] != null)
            {
                GameObject experementGUIControl = Instantiate(networkManager.spawnPrefabs[0]);
                NetworkServer.Spawn(experementGUIControl);
            }
            else
            {
                Debug.LogWarning("NetworkController can't find controlPanel prefab in spawnPrefabs on " + gameObject.name);
            }
        }
        else
        {
            Debug.LogWarning("NetworkController can't find controlPanel prefab in spawnPrefabs on " + gameObject.name);
        }

        Debug.Log("Started host on " + networkManager.networkAddress + " and port " + networkManager.networkPort);
    }
    public void StartClient()
    {
        Debug.Log("Starting client");
        //TODO: make good check for connection of client
        NetworkClient client = networkManager.StartClient();

        if (client != null)
        {
            Debug.Log("Started client on " + networkManager.networkAddress + " and port " + networkManager.networkPort);
        }
        else
        {
            Debug.LogWarning("Client has not connected to " + networkManager.networkAddress + " and port " + networkManager.networkPort);
        }
    }
    public void OnDestroy()
    {
        networkManager.StopClient();
        networkManager.StopHost();
    }
}
