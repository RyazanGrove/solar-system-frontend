using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePage : MonoBehaviour
{
    [SerializeField]
    private LoadInfography loadInfographyComponent;

    [SerializeField]
    private bool nextPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect()
    {
        if(loadInfographyComponent != null)
        {
            if (nextPage)
            {
                loadInfographyComponent.ShowNextPicture();
            }
            else
            {
                loadInfographyComponent.ShowPreviousPicture();
            }
        }
        else
        {
            Debug.LogWarning("LoadInfography has not been found on object " + gameObject.name);
        }
    }
}
