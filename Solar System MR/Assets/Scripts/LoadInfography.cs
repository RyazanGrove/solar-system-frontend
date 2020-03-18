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
        rend.material.mainTexture = Resources.Load("3/" + currentPage.ToString()) as Texture;

        //Load user progress
        currentProgress = PlayerPrefs.GetString("planetProgress3");
        if (currentProgress == "")
        {
            currentProgress = "0000";
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
            rend.material.mainTexture = Resources.Load("3/" + currentPage.ToString()) as Texture;
        }
    }

    public void ShowPreviousPicture()
    {
        if (currentPage >1)
        {
            currentPage--;
            Renderer rend = plane.GetComponent<Renderer>();
            rend.material.mainTexture = Resources.Load("3/" + currentPage.ToString()) as Texture;
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
    }

    public void ClearProgress()
    {
        PlayerPrefs.SetString("planetProgress3", "0000");
        currentProgress = "0000";
    }
}
