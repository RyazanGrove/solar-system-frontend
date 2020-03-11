using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectActivator : MonoBehaviour
{
    // Called by GazeGestureManager when the user performs a Select gesture
    public void OnSelect()
    {
        SceneManager.LoadScene("SinglePlanetScene");
    }
}