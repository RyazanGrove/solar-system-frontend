using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    [SerializeField]
    private bool increase = true;

    [SerializeField]
    private Transform target;

    [SerializeField]
    [Range(0.1f, 2f)]
    private float value = 0.1f;

    public void OnSelect()
    {
        if(target != null)
        {
            Vector3 temp = target.transform.localScale;
            if (temp.x > 0.2f && temp.x < 2.3f)
            {
                target.transform.localScale += increase ? new Vector3(value, value, value) : new Vector3(-value, -value, -value);
            }
            else
            {
                if(temp.x <= 0.2f && increase)
                {
                    target.transform.localScale += new Vector3(value, value, value);
                }
                if(temp.x >= 2.3f && !increase)
                {
                    target.transform.localScale += new Vector3(-value, -value, -value);
                }
            }

        }
    }
}

