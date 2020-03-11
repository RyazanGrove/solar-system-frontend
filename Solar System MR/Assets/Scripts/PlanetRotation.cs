using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField]
    private Transform centralPoint;

    [SerializeField]
    [Range(0.1f,50f)]
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if(centralPoint == null)
        {
            Debug.Log("centralPoint is not assing onto object " + gameObject.name + ". Default value will be used");
            centralPoint =  gameObject.transform.parent != null ? gameObject.transform.parent: gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(centralPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
