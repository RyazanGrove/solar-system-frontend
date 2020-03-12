using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField]
    private int planetNumber;
    // Called by GazeGestureManager when the user performs a Select gesture
    public void OnSelect()
    {
        PlayerPrefs.SetInt ("planetNumber", planetNumber);
//        Application.LoadLevel("NewScene");
        SceneManager.LoadScene("SinglePlanetScene");
    }
}
