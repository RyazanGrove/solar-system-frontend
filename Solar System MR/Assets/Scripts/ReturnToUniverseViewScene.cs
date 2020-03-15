using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToUniverseViewScene : MonoBehaviour
{
    public void OnSelect()
    {
        SceneManager.LoadScene("UniverseView");
    }
}
