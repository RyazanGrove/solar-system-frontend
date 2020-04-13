using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AuthorizationParser
{
    public int id_user;
    public bool error;
    public int last_scene;
    public string errorMessage;
    public string[] planetProgress;


    public static AuthorizationParser CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<AuthorizationParser>(jsonString);
    }

}

