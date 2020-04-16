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

    [SerializeField]
    private string saveLastSceneURL = "http://localhost/saveLastScene.php";
    [SerializeField]
    private string saveProgressURL = "http://localhost/saveProgress.php";
    Dictionary<int, string> pageId = new Dictionary<int, string>();
    private bool isAuthorized;

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
        currentProgress = PlayerPrefs.GetString("planetProgress" + planetNumber.ToString());
        if (currentProgress == "")
        {
            currentProgress = "0000";
        }
        checkCompleteOrStartButton();

        isAuthorized = PlayerPrefs.GetInt("isAuthorized") == 1 ? true : false;
        if (isAuthorized)
        {
            //save scene to DB
            WWWForm form = new WWWForm();
            form.AddField("userId", PlayerPrefs.GetInt("id_user"));
            form.AddField("sceneId", planetNumber);
            WWW www = new WWW(saveLastSceneURL, form);
        }
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
            PlayerPrefs.SetString("planetProgress" + planetNumber.ToString(), currentProgress);
        }
        checkCompleteOrStartButton();

        if (isAuthorized)
        {
            //save progress to DB
            WWWForm form = new WWWForm();
            form.AddField("userId", PlayerPrefs.GetInt("id_user"));
            form.AddField("sceneId", planetNumber);
            form.AddField("pageId", pageId[currentPage - 1]);
            WWW www = new WWW(saveProgressURL, form);
        }
    }

    public void ClearProgress()
    {
        PlayerPrefs.SetString("planetProgress" + planetNumber.ToString(), "0000");
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
