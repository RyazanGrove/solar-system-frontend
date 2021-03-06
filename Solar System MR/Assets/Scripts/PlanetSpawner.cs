﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField]
    private int planetToSpawn;

    [SerializeField]
    private Transform centralPoint;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private GameObject sun;
    [SerializeField]
    private GameObject mercury;
    [SerializeField]
    private GameObject venus;
    [SerializeField]
    private GameObject earth;
    [SerializeField]
    private GameObject mars;
    [SerializeField]
    private GameObject jupyter;
    [SerializeField]
    private GameObject saturn;
    [SerializeField]
    private GameObject uranus;
    [SerializeField]
    private GameObject neptune;
    [SerializeField]
    private GameObject pluto;

    private GameObject planet;

    // Start is called before the first frame update
    void Start()
    {
        if(centralPoint == null)
        {
            centralPoint = gameObject.transform;
            Debug.LogWarning("centralPoint is not assing in object " + gameObject.name);
        }
        planetToSpawn = PlayerPrefs.GetInt ("planetNumber");

        switch (planetToSpawn)
        {
            case 1:
                planet = Instantiate(mercury, centralPoint.position, Quaternion.identity);
                break;
            case 2:
                planet = Instantiate(venus, centralPoint.position, Quaternion.identity);
                break;
            case 3:
                planet = Instantiate(earth, centralPoint.position, Quaternion.identity);
                break;
            case 4:
                planet = Instantiate(mars, centralPoint.position, Quaternion.identity);
                break;
            case 5:
                planet = Instantiate(jupyter, centralPoint.position, Quaternion.identity);
                break;
            case 6:
                planet = Instantiate(saturn, centralPoint.position, Quaternion.identity);
                break;
            case 7:
                planet = Instantiate(uranus, centralPoint.position, Quaternion.identity);
                break;
            case 8:
                planet = Instantiate(neptune, centralPoint.position, Quaternion.identity);
                break;
            case 9:
                planet = Instantiate(pluto, centralPoint.position, Quaternion.identity);
                break;
            default:
                planet = Instantiate(sun, centralPoint.position, Quaternion.identity);
                break;
        }
        planet.transform.parent = gameObject.transform;
        standardizedPrefabParameters(planet);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void standardizedPrefabParameters(GameObject prefab)
    {
        PlanetRotation planetRotationComponent = prefab.GetComponentInChildren<PlanetRotation>();
        if(planetRotationComponent != null)
        {
            Debug.Log("PlanetRotation component will be standardized onto object " + planetRotationComponent.name);
            planetRotationComponent.rotationSpeed = 20f;
            planetRotationComponent.centralPoint = centralPoint;
            Transform planetModelContainerTransform = prefab.transform.GetChild(0);
            planetModelContainerTransform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogWarning("PlanetRotation script component has not been found anywhere in object " + prefab.name);
        }

        if (cameraTransform != null)
        {
            ShowPlanetName showPlanetNameComponent = prefab.GetComponentInChildren<ShowPlanetName>();
            if (showPlanetNameComponent != null)
            {
                Debug.Log("ShowPlanetName component will be standardized onto object " + showPlanetNameComponent.name);
                showPlanetNameComponent.cameraTransform = cameraTransform;
                showPlanetNameComponent.upDistance = 0.7f;
            }
            else
            {
                Debug.LogWarning("ShowPlanetName script component has not been found anywhere in object " + prefab.name);
            }
        }
    }
}
