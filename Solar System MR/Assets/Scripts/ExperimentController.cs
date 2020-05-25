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
    [Range(0,20)]
    private int numberOfCopies;
    [SerializeField]
    private GameObject asteroidMainPrefab;

    [SerializeField]
    private Vector3 centerOfPlanet;

    [SerializeField]
    private float sphereSpawnRadius = 1f;
    [SerializeField]
    private Vector3 asteroidMainPosition;
    private Vector3 asteroidMainPositionCurrent;

    [SerializeField]
    [Range(0.1f,1f)]
    private float asteroidMainColliderRadious;
    [SerializeField]
    [Range(0.1f,2f)]
    private float asteroidSpeed;
    [SerializeField]
    [Range(0.1f, 0.5f)]
    private float speedUnit;

    [SerializeField]
    private Vector3 asteroidSize;
    [SerializeField]
    private Vector3 sizeUnit;

    private GameObject asteroid1;
    private GameObject asteroid2;
    private GameObject asteroid3;
    private GameObject asteroid4;
    private GameObject asteroidMain;
    public GameObject[] asteroidArray;

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

    [SerializeField]
    [Range(0f, 1f)]
    private float numberOfPixelsToMoveOneUnit;

    [SerializeField]
    [Range(1,20)]
    private int numberOfUnitsInSlider;

    [SerializeField]
    private GameObject asteroidSizePin;
    private int currentAsteroidSizePinUnit;

    [SerializeField]
    private GameObject asteroidSpeedPin;
    private int currentAsteroidSpeedPinUnit;

    private GameObject textObjectSize;
    private TextMesh textMeshSize;

    private GameObject textObjectSpeed;
    private TextMesh textMeshSpeed;

    private int usersAnswer;


    // Start is called before the first frame update
    void Start()
    {
        asteroidIsMoving = false;
        asteroidHasNotExploded = true;
        SpawnEarth();
        SpawnAsteroidMain();

        //sliders
        if(numberOfUnitsInSlider == 0)
        {
            numberOfUnitsInSlider = 1;
        }
        currentAsteroidSizePinUnit = 10;
        asteroidSizePin = GameObject.FindGameObjectWithTag("AsteroidSizeSliderPin");
        currentAsteroidSpeedPinUnit = 10;
        asteroidSpeedPin = GameObject.FindGameObjectWithTag("AsteroidSpeedSliderPin");

        //size text
        textObjectSize = new GameObject();
        textObjectSize.transform.parent = asteroidSizePin.transform;
        textObjectSize.transform.position = asteroidSizePin.transform.position;
        textObjectSize.transform.Translate(new Vector3(-0.9f, 0f, 0f));
        textMeshSize = textObjectSize.AddComponent<TextMesh>();
        textMeshSize.text = currentAsteroidSizePinUnit.ToString() + " км";
        textMeshSize.anchor = TextAnchor.MiddleCenter;
        textMeshSize.characterSize = 0.2f;
        textMeshSize.fontSize = 18;

        //speed text
        textObjectSpeed = new GameObject();
        textObjectSpeed.transform.parent = asteroidSpeedPin.transform;
        textObjectSpeed.transform.position = asteroidSpeedPin.transform.position;
        textObjectSpeed.transform.Translate(new Vector3(-0.9f, 0f, 0f));
        textMeshSpeed = textObjectSpeed.AddComponent<TextMesh>();
        textMeshSpeed.text = currentAsteroidSpeedPinUnit.ToString() + " км/с";
        textMeshSpeed.anchor = TextAnchor.MiddleCenter;
        textMeshSpeed.characterSize = 0.2f;
        textMeshSpeed.fontSize = 18;

        usersAnswer = 0;
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
        if (Input.GetKey(KeyCode.Q))
        {
            IncreaseAsteroidSpeed();
        }
        if (Input.GetKey(KeyCode.A))
        {
            DecreaseAsteroidSpeed();
        }
        if (Input.GetKey(KeyCode.E))
        {
            IncreaseAsteroidSize();
        }
        if (Input.GetKey(KeyCode.D))
        {
            DecreaseAsteroidSize();
        }
    }


    private void SpawnAsteroids()
    {
        asteroidArray = new GameObject[numberOfCopies * 4];
        for (int i = 0; i < numberOfCopies; i++)
        {
            //spawn objects
            Vector3 asteroidPosition1 = new Vector3(Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.x, Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.y, Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.z);
            asteroid1 = Instantiate(asteroidPrefab1, asteroidPosition1, Quaternion.identity);
            asteroidArray[4 * i] = asteroid1;
            Vector3 asteroidPosition2 = new Vector3(Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.x, Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.y, Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.z);
            asteroid2 = Instantiate(asteroidPrefab2, asteroidPosition2, Quaternion.identity);
            asteroidArray[4 * i + 1] = asteroid2;
            Vector3 asteroidPosition3 = new Vector3(Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.x, Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.y, Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.z);
            asteroid3 = Instantiate(asteroidPrefab3, asteroidPosition3, Quaternion.identity);
            asteroidArray[4 * i + 2] = asteroid3;
            Vector3 asteroidPosition4 = new Vector3(Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.x, Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.y, Random.Range(-sphereSpawnRadius / 2, sphereSpawnRadius / 2) + centerOfPlanet.z);
            asteroid4 = Instantiate(asteroidPrefab4, asteroidPosition4, Quaternion.identity);
            asteroidArray[4 * i + 3] = asteroid4;

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
        asteroidMain.transform.localScale = asteroidSize;
        Rigidbody asteroidMainRigidBody = asteroidMain.AddComponent<Rigidbody>();
        asteroidMainRigidBody.useGravity = false;
        SphereCollider asteroidMainSphereCollider = asteroidMain.AddComponent<SphereCollider>();
        asteroidMainSphereCollider.radius = asteroidMainColliderRadious;
        StartExplosion asteroidStartExplosionComponent = asteroidMain.AddComponent<StartExplosion>();
        asteroidStartExplosionComponent.experimentController = gameObject.GetComponent<ExperimentController>();
        
        asteroidHasNotExploded = true;
        asteroidMainPositionCurrent = asteroidMainPosition;
    }
    public void StartAsteroidMovement()
    {
        asteroidIsMoving = true;
    }
    private void AsteroidMovement()
    {

        Vector3 direction = centerOfPlanet - asteroidMainPositionCurrent;
        asteroidMain.transform.Translate(direction * asteroidSpeed * Time.deltaTime);
    }

    public void TriggerExplosion()
    {
        //check correct answer
        if (currentAsteroidSizePinUnit == 11 && currentAsteroidSpeedPinUnit == 2)
        {
            SpawnAsteroids();
            asteroidMain.SetActive(false);
            asteroidHasNotExploded = false;
            usersAnswer = 1;
            //Send data. Or send data in special component
        }
        else
        {
            if(currentAsteroidSizePinUnit * currentAsteroidSpeedPinUnit < 22)
            {
                asteroidMain.SetActive(false);
                asteroidHasNotExploded = false;
                ActivateExplosionEffectSmall();
                usersAnswer = 2;
            }
            else
            {
                SpawnAsteroids();
                asteroidMain.SetActive(false);
                asteroidHasNotExploded = false;
                usersAnswer = 3;
            }
        }

    }

    private void MakeEarthVisisble()
    {
        planetObject.SetActive(true);
    }

    private void SpawnEarth()
    {
        planetObject = Instantiate(planetPrefab, centerOfPlanet, Quaternion.identity);
        //planetObject.transform.position = Vector3.zero;
        planetObject.transform.localScale = new Vector3(sphereSpawnRadius * 3.5f, sphereSpawnRadius * 3.5f, sphereSpawnRadius * 3.5f);
    }

    public void ResetExperement()
    {
        MakeEarthVisisble();
        SpawnAsteroidMain();
        DestroyAsteroids();
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

    private void ActivateExplosionEffectSmall()
    {
        if (explosionEffectObject != null)
        {
            Destroy(explosionEffectObject);
        }
        if (ExplosionEffectPrefab != null)
        {
            SphereCollider asteroidCollider = asteroidMain.GetComponent<SphereCollider>();
            if (asteroidCollider != null) 
            {
                Vector3 placeOfExplosion = asteroidCollider.ClosestPointOnBounds(centerOfPlanet);
                explosionEffectObject = Instantiate(ExplosionEffectPrefab, placeOfExplosion, Quaternion.identity);
                explosionEffectObject.transform.localScale = Vector3.one * 0.3f;
            }
            else
            {
                Debug.LogWarning("There is no SphereCollider component on object " + asteroidMain.name);
            }
        }
        else
        {
            Debug.LogWarning("ExplosionEffectPrefab field is not assigned on object " + gameObject.name);
        }
    }

    private void DestroyAsteroids()
    {
        if (asteroidArray != null && asteroidArray.Length > 1)
        {
            for (int i = 0; i < numberOfCopies * 4; i++)
            {
                Destroy(asteroidArray[i]);
            }
        }
    }

    public void IncreaseAsteroidSpeed()
    {
        asteroidSpeed += speedUnit;
        if(asteroidSpeed > 2.01f)
        {
            asteroidSpeed = 2f;
        }
        else
        {
            IncreaseAsteroidSpeedOnSlider();
        }
    }

    public void DecreaseAsteroidSpeed()
    {
        asteroidSpeed -= speedUnit;
        if (asteroidSpeed < 0.09f)
        {
            asteroidSpeed = 0.1f;
        }
        else
        {
            DecreaseAsteroidSpeedOnSlider();
        }
    }

    public void IncreaseAsteroidSize()
    {
        Vector3 tempSize = asteroidMain.transform.localScale + sizeUnit;
        if(tempSize.x < 2.01f && tempSize.y < 2.01f && tempSize.z < 2.01f)
        {
            asteroidSize += sizeUnit;
            asteroidMain.transform.localScale = asteroidSize;

            IncreaseAsteroidSizeOnSlider();
        }
    }

    public void DecreaseAsteroidSize()
    {
        Vector3 tempSize = asteroidMain.transform.localScale - sizeUnit;
        if (tempSize.x > 0.09f && tempSize.y > 0.09f && tempSize.z > 0.09f)
        {
            asteroidSize -= sizeUnit;
            asteroidMain.transform.localScale = asteroidSize;

            DecreaseAsteroidSizeOnSlider();
        }
    }

    public void IncreaseAsteroidSizeOnSlider()
    {
        if(asteroidSizePin != null)
        {
            if(currentAsteroidSizePinUnit <= numberOfUnitsInSlider)
            {
                currentAsteroidSizePinUnit++;
                asteroidSizePin.transform.Translate(Vector3.up * numberOfPixelsToMoveOneUnit);
                UpdateAsteroidSizeText();
            }
        }
        else
        {
            Debug.Log("asteroidSizePin is empty on " + gameObject.name + ". There is no game object with tag \"AsteroidSizeSliderPin\" ");
        }
    }

    public void DecreaseAsteroidSizeOnSlider()
    {
        if (asteroidSizePin != null)
        {
            if (currentAsteroidSizePinUnit > 1)
            {
                currentAsteroidSizePinUnit--;
                asteroidSizePin.transform.Translate(Vector3.up * - numberOfPixelsToMoveOneUnit);
                UpdateAsteroidSizeText();
            }
        }
        else
        {
            Debug.Log("asteroidSizePin is empty on " + gameObject.name + ". There is no game object with tag \"AsteroidSizeSliderPin\" ");
        }
    }

    public void IncreaseAsteroidSpeedOnSlider()
    {
        if (asteroidSizePin != null)
        {
            if (currentAsteroidSpeedPinUnit <= numberOfUnitsInSlider)
            {
                currentAsteroidSpeedPinUnit++;
                asteroidSpeedPin.transform.Translate(Vector3.up * numberOfPixelsToMoveOneUnit);
                UpdateAsteroidSpeedText();
            }
        }
        else
        {
            Debug.Log("asteroidSpeedPin is empty on " + gameObject.name + ". There is no game object with tag \"AsteroidSpeedSliderPin\" ");
        }
    }

    public void DecreaseAsteroidSpeedOnSlider()
    {
        if (asteroidSpeedPin != null)
        {
            if (currentAsteroidSpeedPinUnit > 1)
            {
                currentAsteroidSpeedPinUnit--;
                asteroidSpeedPin.transform.Translate(Vector3.up * -numberOfPixelsToMoveOneUnit);
                UpdateAsteroidSpeedText();
            }
        }
        else
        {
            Debug.Log("asteroidSizePin is empty on " + gameObject.name + ". There is no game object with tag \"AsteroidSpeedSliderPin\" ");
        }
    }

    public int getUsersAnswer()
    {
        return usersAnswer;
    }

    public void setUsersAnswer(int zero)
    {
        usersAnswer = zero;
    }

    private void UpdateAsteroidSizeText()
    {
        textMeshSize.text = currentAsteroidSizePinUnit.ToString() + " км";
    }

    private void UpdateAsteroidSpeedText()
    {
        textMeshSpeed.text = currentAsteroidSpeedPinUnit.ToString() + " км/с";
    }
}
