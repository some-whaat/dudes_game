using UnityEngine;
using UnityEngine.UI;

public class camera_whatch : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    [SerializeField] dude_visualaiser_scale_script dude_visualaiser_scale_script;
    choose_dude choose_dude;

    [SerializeField] float offset;
    [SerializeField] float rotation_speed = 180;
    [SerializeField] private float scroll_speed;

    [SerializeField] LayerMask mask;

    private Vector3 previouse_pos;

    public float zoom;


    private void Start()
    {
        cam = Camera.main;
        choose_dude = GameObject.FindGameObjectWithTag("spawner").gameObject.GetComponent<choose_dude>();
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

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse_pos = Input.mousePosition;
            mouse_pos.z = 10f;
            mouse_pos = cam.ScreenToWorldPoint(mouse_pos);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 999999, mask))
            {
                if (hit.collider.gameObject.tag == "dude")
                {
                    choose_dude.IsTheDude(hit.collider.GetComponent<head_changer>().prop, hit.collider.GetComponent<animation_script>());
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouse_pos = Input.mousePosition;
            mouse_pos.z = 10f;
            mouse_pos = cam.ScreenToWorldPoint(mouse_pos);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 999999, mask))
            {
                if (hit.collider.gameObject.tag != "dude")
                {
                    target = hit.collider.gameObject.transform;
                    change_pos();
                }
            }
        }
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

