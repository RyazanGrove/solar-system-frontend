using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkAsCompleted : MonoBehaviour
{
    [SerializeField]
    private LoadInfography loadInfographyComponent;

    public void OnSelect()
    {
        if (loadInfographyComponent != null)
        {
            loadInfographyComponent.UpdateProgress();
        }
        else
        {
            Debug.LogWarning("LoadInfography has not been found on object " + gameObject.name);
        }
    }
}
