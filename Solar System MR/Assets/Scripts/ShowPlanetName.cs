using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlanetName : MonoBehaviour
{
    [SerializeField]
    public string planetName;

    [SerializeField]
    [Range(0f,2f)]
    public float upDistance;

    [SerializeField]
    public Transform cameraTransform;

    private TextMesh textMesh;
    private GameObject textObject;

    // Start is called before the first frame update
    void Start()
    {
        textObject = new GameObject();
        textObject.transform.parent = gameObject.transform;
        textObject.transform.position = gameObject.transform.position;
        textObject.transform.Translate(new Vector3(0f,upDistance,0f));
        textObject.transform.Rotate(90f,0f,180f);
        textMesh = textObject.AddComponent<TextMesh>();
        textMesh.text = planetName;
        if(planetName == "")
        {
            Debug.LogWarning("There is no name for planet on object " + gameObject.name);
        }
        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera Transform is not assigned to camera object on object " + gameObject.name);
            textObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        textObject.transform.LookAt(cameraTransform);
        textObject.transform.Rotate(0f,-180f,0f);
    }
}
