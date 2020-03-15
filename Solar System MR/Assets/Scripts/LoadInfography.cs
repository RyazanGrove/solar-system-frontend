using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInfography : MonoBehaviour
{
    //    [SerializeField] private UnityEngine.UI.Image image = null;
    private GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        plane.transform.localScale = new Vector3(0.7f, 1f, 1f);
        
        plane.transform.position = Vector3.zero;
        plane.transform.parent = gameObject.transform;
        plane.transform.Rotate(90, -180, -180);
        Renderer rend = plane.GetComponent<Renderer>();
        rend.material.mainTexture = Resources.Load("3/1") as Texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
