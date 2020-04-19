using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartExplosion : MonoBehaviour
{
    public ExperimentController experimentController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(experimentController != null)
        {
            experimentController.TriggerExplosion();
        }
        else
        {
            Debug.LogWarning("ExperimentController field is not assigned on object " + gameObject.name);
        }
    }
}
