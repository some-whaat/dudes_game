using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class movement_boids_script : MonoBehaviour
{
    private Rigidbody rb;
    private ArrayList near_objects;

    [SerializeField] private float speed;
    [SerializeField] private float rot_speed = 720;
    [SerializeField] private float seporation_intensity = 5;

    public float hight_y;

    [SerializeField] private float distance_of_vieuving;
    [SerializeField] private float hight_of_raycast = 0;
    [SerializeField] private float angle_of_vieuving;
    [SerializeField] private float amount_of_rays;

    private float fraction_angle;
    private float srart_of_raycast;
    private float end_of_raycast;
    private Ray ray;

    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fraction_angle = angle_of_vieuving / amount_of_rays;
        srart_of_raycast = ( 180 - fraction_angle) / 2;
        end_of_raycast = srart_of_raycast + fraction_angle * amount_of_rays;
        hight_y = transform.position.y;
        //direction = transform.forward;

    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        //transform.rotation.z = 0;
        direction = (Seporation() + direction).normalized;
        //direction = Seporation() + direction + Vector3.forward;
        direction.y = 0;
        Debug.Log(direction);

        if (direction == Vector3.zero)
        {
            direction = -rb.transform.forward;
        }


        //rb.transform.Translate(direction * speed * Time.deltaTime, Space.World);
        //transform.forward = -direction;


        
        Vector3 next_pos = rb.transform.position + direction * speed * Time.deltaTime;
        next_pos.y = hight_y;

        //rb.transform.LookAt(-next_pos);
        rb.transform.position = next_pos;

        Quaternion end_rot = Quaternion.LookRotation(-direction, Vector3.up);
        rb.transform.rotation = Quaternion.RotateTowards(transform.rotation, end_rot, rot_speed * Time.deltaTime);

        //transform.forward = -direction;
        /*

        rb.transform.eulerAngles = new Vector3(0, rb.transform.eulerAngles.y, 0);

        rb.rotation = Quaternion.LookRotation(direction, Vector3.up);

        rb.transform.eulerAngles = new Vector3(0, rb.transform.eulerAngles.y, 0);

        //rb.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
        //rb.AddRelativeForce((direction) * speed, ForceMode.VelocityChange);
        //rb.velocity = (Vector3.forward + direction) * speed;
        */
    }

    private Vector3 Seporation()
    {
        /*
        Vector3 near_object = get_raycast_interseptions();
        Vector3 seporation_velosity = Vector3.zero;

        float dist = Vector3.Distance(near_object, rb.position);
        Vector3 direction_away = (rb.position - near_object).normalized;
        seporation_velosity += direction_away / dist;

        seporation_velosity.y = 0f;

        return seporation_velosity * seporation_intensity;
        */


        near_objects = get_raycast_interseptions();
        Vector3 seporation_velosity = Vector3.zero;

        foreach (Vector3 pos in near_objects)
        {
            float dist = Vector3.Distance(pos, rb.position);
            Vector3 direction_away = (rb.position - pos).normalized;
            seporation_velosity += direction_away / dist;
        }

        seporation_velosity /= (float)near_objects.Count;
        seporation_velosity.y = 0f;

        return seporation_velosity * seporation_intensity;
    }

    private ArrayList get_raycast_interseptions()
    {
        /*
        Vector3 raycast_interseption = Vector3.zero;

        ray = new Ray(rb.position + new Vector3(0, hight_of_raycast, 0), direction);
        Debug.DrawRay(rb.position + new Vector3(0, hight_of_raycast, 0), direction);

        if (Physics.Raycast(ray, out RaycastHit hit, distance_of_vieuving))
        {
            raycast_interseption = hit.point;
        }


        return raycast_interseption;



        Vector3 raycast_interseption = Vector3.zero;

        ray = new Ray(rb.position + new Vector3(0, hight_of_raycast, 0), -rb.transform.forward);
            Debug.DrawRay(rb.position + new Vector3(0, hight_of_raycast, 0), -rb.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, distance_of_vieuving))
            {
                raycast_interseption = hit.point;
            }
        

        return raycast_interseption;
        */



        ArrayList raycast_interseptions = new ArrayList();

        for (float a = srart_of_raycast; a <= end_of_raycast; a += fraction_angle)
        {
            Vector3 position_of_raycast = rb.position + new Vector3( 0, hight_of_raycast, 0);

            float ratio_angle = Vector3.Angle(Vector3.forward, rb.transform.forward) + 90;
            //float ratio_angle = Vector3.Angle(rb.transform.forward, Vector3.forward);

            Vector3 ray_direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * a), 0, Mathf.Cos(Mathf.Deg2Rad * a));
            ray_direction = Quaternion.AngleAxis(ratio_angle, rb.transform.up) * ray_direction;
            
            ray = new Ray(position_of_raycast, ray_direction);
            Debug.DrawRay(position_of_raycast, ray_direction);

            if (Physics.Raycast(ray, out RaycastHit hit, distance_of_vieuving))
            {
                raycast_interseptions.Add(hit.point);
            }
        }

        return raycast_interseptions;

    }
}
