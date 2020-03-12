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
    [Range(-2f, 2f)]
    public float leftRightDistance;

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
        textObject.transform.Translate(new Vector3(leftRightDistance,upDistance,0f));
        textObject.transform.Rotate(90f,0f,180f);
        textMesh = textObject.AddComponent<TextMesh>();

        //textMesh settings
        textMesh.text = planetName;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.characterSize = 0.2f;
        textMesh.fontSize = 18;

        if (planetName == "")
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
