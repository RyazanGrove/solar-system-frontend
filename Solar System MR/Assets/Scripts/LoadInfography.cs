using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInfography : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransformForUserProgress;

    private GameObject plane;
    private int currentPage;
    private string currentProgress;
    private int planetNumber;

    [SerializeField]
    private GameObject completeButton;
    [SerializeField]
    private GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        plane.transform.parent = gameObject.transform;
        plane.transform.position = new Vector3(1.5f, 0f, 1.5f);
        plane.transform.localScale = new Vector3(0.15f, 0.2f, 0.2f);
        plane.transform.Rotate(90f, 0f, -180f);

        Renderer rend = plane.GetComponent<Renderer>();
        currentPage = 1;
        planetNumber = PlayerPrefs.GetInt("planetNumber");
        if(planetNumber < 1 || planetNumber > 10)
        {
            planetNumber = 0;
        }
        Debug.Log("Planet number is " + planetNumber);
        rend.material.mainTexture = Resources.Load(planetNumber.ToString() + "/" + currentPage.ToString()) as Texture;

        //Load user progress
        currentProgress = PlayerPrefs.GetString("planetProgress3");
        if (currentProgress == "")
        {
            currentProgress = "0000";
        }
        checkCompleteOrStartButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNextPicture()
    {
        if (currentPage < 4)
        {
            currentPage++;
            Renderer rend = plane.GetComponent<Renderer>();
            rend.material.mainTexture = Resources.Load(planetNumber.ToString() + "/" + currentPage.ToString()) as Texture;
            checkCompleteOrStartButton();
        }
    }

    public void ShowPreviousPicture()
    {
        if (currentPage >1)
        {
            currentPage--;
            Renderer rend = plane.GetComponent<Renderer>();
            rend.material.mainTexture = Resources.Load(planetNumber.ToString() + "/" + currentPage.ToString()) as Texture;
            checkCompleteOrStartButton();
        }
    }

    public void UpdateProgress()
    {
        char[] charArray = currentProgress.ToCharArray();
        if (charArray[currentPage] == '0') 
        {
            charArray[currentPage - 1] = '1';
            currentProgress = new string(charArray);
            PlayerPrefs.SetString("planetProgress3", currentProgress);
        }
        Debug.Log(currentProgress + " = is current Progress. Page number is " + currentPage.ToString());
        checkCompleteOrStartButton();
    }

    public void ClearProgress()
    {
        PlayerPrefs.SetString("planetProgress3", "0000");
        currentProgress = "0000";
        checkCompleteOrStartButton();
    }

    private void checkCompleteOrStartButton()
    {
        if(completeButton == null)
        {
            Debug.LogWarning("CompleteButton field is not assigned on object " + gameObject.name);
            return;
        }
        if (startButton == null)
        {
            Debug.LogWarning("StartButton field is not assigned on object " + gameObject.name);
            return;
        }

        char[] charArray = currentProgress.ToCharArray();
        if (charArray[currentPage - 1] == '0')
        {
            completeButton.SetActive(true);
            startButton.SetActive(false);
        }
        else
        {
            completeButton.SetActive(false);
            startButton.SetActive(true);
        }
    }
}
