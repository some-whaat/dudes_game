using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_whatch : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public Transform target;

    [SerializeField] float offset;

    [SerializeField] float rotation_speed = 180  ;

    private Vector3 previouse_pos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previouse_pos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 dir = previouse_pos - cam.ScreenToViewportPoint(Input.mousePosition);
            /*
            var newPos = (cam.transform.position - target.position).normalized * radius;
            newPos += target.position;
            cam.transform.position = newPos;
            */
            Vector3 pos = cam.transform.position;
            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), angle: dir.y * rotation_speed);
            cam.transform.Rotate(new Vector3(0, 1, 0), angle: -dir.x * rotation_speed, relativeTo: Space.World);
            cam.transform.Translate(new Vector3(0, 0, -offset));

            previouse_pos = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    public void change_pos()
    {
        previouse_pos = cam.ScreenToViewportPoint(Input.mousePosition);

        Vector3 dir = previouse_pos - cam.ScreenToViewportPoint(Input.mousePosition);
        /*
        var newPos = (cam.transform.position - target.position).normalized * radius;
        newPos += target.position;
        cam.transform.position = newPos;
        */
        Vector3 pos = cam.transform.position;
        cam.transform.position = target.position;

        cam.transform.Rotate(new Vector3(1, 0, 0), angle: dir.y * rotation_speed);
        cam.transform.Rotate(new Vector3(0, 1, 0), angle: -dir.x * rotation_speed, relativeTo: Space.World);
        cam.transform.Translate(new Vector3(0, 0, -offset));

        previouse_pos = cam.ScreenToViewportPoint(Input.mousePosition);
    }
}

