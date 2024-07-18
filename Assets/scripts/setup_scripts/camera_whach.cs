using UnityEngine;

public class camera_whatch : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    [SerializeField] dude_visualaiser_scale_script dude_visualaiser_scale_script;

    [SerializeField] float offset;
    [SerializeField] float rotation_speed = 180;

    private Vector3 previouse_pos;

    public float zoom;
    [SerializeField] private float scroll_speed;


    private void Start()
    {
        cam = Camera.main;
        zoom = cam.orthographicSize;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previouse_pos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 dir = previouse_pos - cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 pos = cam.transform.position;

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), angle: dir.y * rotation_speed);
            cam.transform.Rotate(new Vector3(0, 1, 0), angle: -dir.x * rotation_speed, relativeTo: Space.World);
            cam.transform.Translate(new Vector3(0, 0, -offset));

            previouse_pos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        zoom -= Input.mouseScrollDelta.y * scroll_speed;

        if (zoom < 2)
        {
            zoom = 2;
        }
        cam.orthographicSize = zoom;
        dude_visualaiser_scale_script.change_scale(zoom);
    }

    public void change_pos()
    {
        previouse_pos = cam.ScreenToViewportPoint(Input.mousePosition);

        Vector3 dir = previouse_pos - cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 pos = cam.transform.position;
        cam.transform.position = target.position;

        cam.transform.Rotate(new Vector3(1, 0, 0), angle: dir.y * rotation_speed);
        cam.transform.Rotate(new Vector3(0, 1, 0), angle: -dir.x * rotation_speed, relativeTo: Space.World);
        cam.transform.Translate(new Vector3(0, 0, -offset));

        previouse_pos = cam.ScreenToViewportPoint(Input.mousePosition);
    }
}

