using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_holder_script : MonoBehaviour
{

    [SerializeField] speeking_manager speeking_manager;

    private GameObject bubble_canvas;
    private GameObject conteiner;

    private float curr_angle = 0;

    private void Start()
    {
        bubble_canvas = transform.GetChild(0).gameObject;
        conteiner = transform.parent.gameObject;
    }

    private void OnMouseDown()
    {
        speeking_manager.skip_sent_imput();
    }

    private void OnTriggerEnter()
    {
        /*
        if (curr_angle == 0)
        {
            curr_angle = 180;
        }
        else
        {
            curr_angle = 0;
        }
        turn(curr_angle);
        */
        turn();
    }

    public void turn() //180 or 0 only
    {
        /*
        Debug.Log(angle);
        Vector3 new_rot = transform.eulerAngles;
        new_rot.y = angle;
        transform.eulerAngles = new_rot;
        Vector3 new_canvas_rot = bubble_canvas.transform.eulerAngles;
        new_canvas_rot.y = angle;
        bubble_canvas.transform.eulerAngles = new_canvas_rot;
        */
        conteiner.transform.RotateAround(Vector3.up, Mathf.PI);
        bubble_canvas.transform.RotateAround(Vector3.up, Mathf.PI);
    }
}
