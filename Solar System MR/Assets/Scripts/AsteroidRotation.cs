using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    private Vector3 rotationSpeed;
    [SerializeField]
    [Range(0f,10f)]
    private float maxRandomNuber;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = new Vector3(Random.Range(0f, maxRandomNuber), Random.Range(0f, maxRandomNuber), Random.Range(0f, maxRandomNuber));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed);
    }
}
