using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//"http://localhost:5000/echo"; //
public class SaveUserProgressAtUniverseScene : MonoBehaviour
{
    [SerializeField]
    private string saveLastSceneURL = "http://localhost/saveLastScene.php";
    private bool isAuthorized;

    // Start is called before the first frame update
    void Start()
    {
        isAuthorized = PlayerPrefs.GetInt("isAuthorized") == 1 ? true : false;
        if (isAuthorized)
        {
            //save scene to DB
            WWWForm form = new WWWForm();
            form.AddField("userId", PlayerPrefs.GetInt("id_user"));
            form.AddField("sceneId", 10);
            WWW www = new WWW(saveLastSceneURL, form);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
