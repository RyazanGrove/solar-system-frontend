using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        //keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, keyboardText);
        /*    InputField inputField = gameObject.AddComponent<InputField>(); //<InputField>();//InputTextBox
            inputField.targetGraphic = null; // = inputField;
            */

        // Single-line password box with title
        //keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, false, "Secure Single-line Title");
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
}
