using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUserProgress : MonoBehaviour
{
    [SerializeField]
    public string userProgress;

    [SerializeField]
    [Range(-2f, 0f)]
    public float downDistance;

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
        textObject.transform.Translate(new Vector3(0f, downDistance, 0f));
        textObject.transform.Rotate(90f, 0f, 180f);
        textMesh = textObject.AddComponent<TextMesh>();

        ObjectActivator objectActivator = gameObject.GetComponent<ObjectActivator>();
        if(objectActivator == null)
        {
            Debug.LogWarning("ObjectActivator is not assigned on object " + gameObject.name);
        }
        else
        {
            int planetNumber = objectActivator.GetPlanetNumber();
            //Load user progress
            string userProgressLoad = PlayerPrefs.GetString("planetProgress" + planetNumber.ToString());
            if (userProgressLoad == "")
            {
                userProgressLoad = "0000";
            }
            userProgress = SetProgressToUserProgressComponent(userProgressLoad);
        }

        //textMesh settings
        textMesh.text = userProgress;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.characterSize = 0.2f;
        textMesh.fontSize = 18;
        textMesh.color = Color.green;

        if (userProgress == "")
        {
            Debug.LogWarning("There is no user progress on object " + gameObject.name);
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
        textMesh.text = userProgress;
        textObject.transform.LookAt(cameraTransform);
        textObject.transform.Rotate(0f, -180f, 0f);
    }

    public void SetCameraTransform(Transform cameraTransform)
    {
        this.cameraTransform = cameraTransform;
    }

    private string SetProgressToUserProgressComponent(string userProgress)
    {
        int totalValue = 0;
        char[] charArray = userProgress.ToCharArray();
        foreach (char item in charArray)
        {
            if (item == '1')
            {
                totalValue += 25;
            }
        }
        return totalValue.ToString() + "%";
    }
}
