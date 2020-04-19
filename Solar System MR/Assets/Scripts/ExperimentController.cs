using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentController : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab1;
    [SerializeField]
    private GameObject asteroidPrefab2;
    [SerializeField]
    private GameObject asteroidPrefab3;
    [SerializeField]
    private GameObject asteroidPrefab4;
    [SerializeField]
    private int numberOfCopies;
    [SerializeField]
    private GameObject asteroidMainPrefab;

    [SerializeField]
    private Vector3 centerOfPlanet;
    //public Vector3 size;
    [SerializeField]
    private float sphereSpawnRadius = 1f;
    [SerializeField]
    private Vector3 asteroidMainPosition;
    private Vector3 asteroidMainPositionCurrent;

    [SerializeField]
    [Range(0.1f,1f)]
    private float asteroidMainColliderRadious;

    private GameObject asteroid1;
    private GameObject asteroid2;
    private GameObject asteroid3;
    private GameObject asteroid4;
    private GameObject asteroidMain;

    private Rigidbody rigidBodyAsteroid1;
    private Rigidbody rigidBodyAsteroid2;
    private Rigidbody rigidBodyAsteroid3;
    private Rigidbody rigidBodyAsteroid4;

    [SerializeField]
    [Range(0f,1000f)]
    private float explosionForce = 100.0f;
    
    [SerializeField]
    private GameObject planetPrefab;
    private GameObject planetObject;
    [SerializeField]
    private GameObject ExplosionEffectPrefab;
    private GameObject explosionEffectObject;

    private bool asteroidIsMoving;
    private bool asteroidHasNotExploded;


    // Start is called before the first frame update
    void Start()
    {
        asteroidIsMoving = false;
        asteroidHasNotExploded = true;
        SpawnEarth();
        SpawnAsteroidMain();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            asteroidIsMoving = true;
        }
        if (asteroidIsMoving && asteroidHasNotExploded)
        {
            AsteroidMovement();
        }
        if (Input.GetKey(KeyCode.R))
        {
            ResetExperement();
        }
    }


    private void SpawnAsteroids()
    {
        for (int i = 0; i < numberOfCopies; i++)
        {
            //spawn objects
            Vector3 asteroidPosition1 = new Vector3(Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2), Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2), Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2));
            asteroid1 = Instantiate(asteroidPrefab1, asteroidPosition1, Quaternion.identity);
            Vector3 asteroidPosition2 = new Vector3(Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2), Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2), Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2));
            asteroid2 = Instantiate(asteroidPrefab2, asteroidPosition2, Quaternion.identity);
            Vector3 asteroidPosition3 = new Vector3(Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2), Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2), Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2));
            asteroid3 = Instantiate(asteroidPrefab3, asteroidPosition3, Quaternion.identity);
            Vector3 asteroidPosition4 = new Vector3(Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2), Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2), Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2));
            asteroid4 = Instantiate(asteroidPrefab4, asteroidPosition4, Quaternion.identity);

            //add rigidbogy to spawn objects
            rigidBodyAsteroid1 = asteroid1.AddComponent<Rigidbody>();
            rigidBodyAsteroid1.useGravity = false;
            rigidBodyAsteroid2 = asteroid2.AddComponent<Rigidbody>();
            rigidBodyAsteroid2.useGravity = false;
            rigidBodyAsteroid3 = asteroid3.AddComponent<Rigidbody>();
            rigidBodyAsteroid3.useGravity = false;
            rigidBodyAsteroid4 = asteroid4.AddComponent<Rigidbody>();
            rigidBodyAsteroid4.useGravity = false;

            //apply explosion force
            rigidBodyAsteroid1.AddExplosionForce(explosionForce, centerOfPlanet, sphereSpawnRadius);
            rigidBodyAsteroid2.AddExplosionForce(explosionForce, centerOfPlanet, sphereSpawnRadius);
            rigidBodyAsteroid3.AddExplosionForce(explosionForce, centerOfPlanet, sphereSpawnRadius);
            rigidBodyAsteroid4.AddExplosionForce(explosionForce, centerOfPlanet, sphereSpawnRadius);
        }

        //hide-show effects
        planetObject.SetActive(false);
        ActivateExplosionEffect();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(centerOfPlanet, sphereSpawnRadius);
    }

    private void SpawnAsteroidMain()
    {
        if(asteroidMain != null)
        {
            Destroy(asteroidMain);
        }
        //add necessary scripts for collision
        asteroidMain = Instantiate(asteroidMainPrefab, asteroidMainPosition, Quaternion.identity);
        Rigidbody asteroidMainRigidBody = asteroidMain.AddComponent<Rigidbody>();
        asteroidMainRigidBody.useGravity = false;
        SphereCollider asteroidMainSphereCollider = asteroidMain.AddComponent<SphereCollider>();
        asteroidMainSphereCollider.radius = asteroidMainColliderRadious;
        StartExplosion asteroidStartExplosionComponent = asteroidMain.AddComponent<StartExplosion>();
        asteroidStartExplosionComponent.experimentController = gameObject.GetComponent<ExperimentController>();
        
        asteroidHasNotExploded = true;
        asteroidMainPositionCurrent = asteroidMainPosition;
    }

    private void AsteroidMovement()
    {

        Vector3 direction = centerOfPlanet - asteroidMainPositionCurrent;
        asteroidMain.transform.Translate(direction * Time.deltaTime);
    }

    public void TriggerExplosion()
    {
        SpawnAsteroids();
        asteroidMain.SetActive(false);
        asteroidHasNotExploded = false;
    }

    private void MakeEarthVisisble()
    {
        planetObject.SetActive(true);
    }

    private void SpawnEarth()
    {
        planetObject = Instantiate(planetPrefab, centerOfPlanet, Quaternion.identity);
        planetObject.transform.position = Vector3.zero;
        planetObject.transform.localScale = new Vector3(sphereSpawnRadius * 3.5f, sphereSpawnRadius * 3.5f, sphereSpawnRadius * 3.5f);
    }

    public void ResetExperement()
    {
        MakeEarthVisisble();
        SpawnAsteroidMain();
        asteroidIsMoving = false;
    }

    private void ActivateExplosionEffect()
    {
        if(explosionEffectObject != null)
        {
            Destroy(explosionEffectObject);
        }
        if (ExplosionEffectPrefab != null)
        {
            explosionEffectObject = Instantiate(ExplosionEffectPrefab, centerOfPlanet, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("ExplosionEffectPrefab field is not assigned on object " + gameObject.name);
        }
    }
}
