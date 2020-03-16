using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInfography : MonoBehaviour
{
    //    [SerializeField] private UnityEngine.UI.Image image = null;
    private GameObject plane;
    private int currentPage;

    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        plane.transform.parent = gameObject.transform;
        plane.transform.position = new Vector3(1.5f, 0f, 1.5f);
        plane.transform.localScale = new Vector3(0.15f, 0.2f, 0.2f);
        plane.transform.Rotate(90, -180, -180);

        Renderer rend = plane.GetComponent<Renderer>();
        currentPage = 1;
        rend.material.mainTexture = Resources.Load("3/" + currentPage.ToString()) as Texture;
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
}
