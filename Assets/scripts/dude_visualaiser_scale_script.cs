using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dude_visualaiser_scale_script : MonoBehaviour
{
    //[SerializeField] camera_whatch camera_whatch;

    public void change_scale(float zoom)
    {
        transform.localScale = Vector3.one * zoom;
    }
}
