﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPoint : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraTransform != null)
        {
            gameObject.transform.LookAt(cameraTransform);
            gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));
        }
    }
}
