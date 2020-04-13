using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthorizationController : MonoBehaviour
{
    private string loginText = "Enter login";
    private string passwordText = "Enter password";

    private string userLogin = "";
    private string userPassword = "";

    UnityEngine.TouchScreenKeyboard keyboardLogin;
    UnityEngine.TouchScreenKeyboard keyboardPassword;

    [SerializeField]
    private Text loginButtonText;
    [SerializeField]
    private Text passwordButtonText;
    [SerializeField]
    private string loginURL = "http://localhost/unity_login.php";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TouchScreenKeyboard.visible == false && keyboardLogin != null)
        {
            if (keyboardLogin.done == true)
            {
                userLogin = keyboardLogin.text;
                keyboardLogin = null;
                loginButtonText.text = userLogin;
            }
        }

        if (TouchScreenKeyboard.visible == false && keyboardPassword != null)
        {
            if (keyboardPassword.done == true)
            {
                userPassword = keyboardPassword.text;
                keyboardPassword = null;
                passwordButtonText.text = changeStringToAsterics(userPassword);
            }
        }
    }

    public void EnterLogin()
    {
        keyboardLogin = TouchScreenKeyboard.Open("Login", TouchScreenKeyboardType.Default, false, false, false, false, loginText);
    }

    public void EnterPassword()
    {
        keyboardPassword = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, false, passwordText);
    }

    public static string changeStringToAsterics(string str)
    {
        return new string('*', str.Length);
    }

    public void LoginWithoutAuthorization()
    {
        PlayerPrefs.SetInt("isAuthorized", 0);
        SceneManager.LoadScene("UniverseView");
    }

    public void LoginWithAuthorization()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", userLogin);
        form.AddField("password", userPassword);
        WWW www = new WWW(loginURL, form);
        StartCoroutine(LoginFunc(www));
    }

	IEnumerator LoginFunc(WWW www)
	{
		yield return www;

		if (www.error == null)
		{
            AuthorizationParser answerWWW = AuthorizationParser.CreateFromJSON(www.text);
         
            if (!answerWWW.error)
            {
                
                PlayerPrefs.SetInt("id_user", answerWWW.id_user);
                PlayerPrefs.SetInt("planetNumber", answerWWW.last_scene);
                for (int i=0; i<10; i++)
                {
                    PlayerPrefs.SetString("planetProgress"+i, answerWWW.planetProgress[i]);
                }
               
                if (answerWWW.last_scene == 10)
                {
                    PlayerPrefs.SetInt("isAuthorized", 1);
                    SceneManager.LoadScene("UniverseView", LoadSceneMode.Single);
                }
                else
                {
                    PlayerPrefs.SetInt("isAuthorized", 1);
                    SceneManager.LoadScene("SinglePlanetScene", LoadSceneMode.Single);
                }
            }
            else
            {
                Debug.Log(answerWWW.errorMessage);
            }
		
			
		}
		else
		{
			Debug.Log("Error: " + www.error);
		}
	}
}
