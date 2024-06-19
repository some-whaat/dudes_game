using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Image;

public class wolking_manadger : MonoBehaviour
{
    Ray ray;
    Ray rayup;
    Ray raydown;
    public LayerMask ground_mask;

    public Vector3 point;
    private float timer;
    private float long_timer;
    private Vector3 ellipseCenter;

    [SerializeField] prosigual_walking left_prosigual_walking;
    [SerializeField] prosigual_walking right_prosigual_walking;

    [SerializeField] private float step_movement_time = 0.7f;
    //[SerializeField] private float mid_step_vaiting_time = 0.0008f;

    [SerializeField] private float math_method_influence = 0.75f;

    [SerializeField] private float step_hight = 1;
    [SerializeField] float step_time;
    [SerializeField] float min_step_dist;
    [SerializeField] float restep_time;

    [SerializeField] target_to_go left_target_to_go;
    [SerializeField] target_to_go right_target_to_go;

    [SerializeField] float raydist = 5;

    private bool is_stepping;

    void Update()
    {
        ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out RaycastHit hit, 10, ground_mask))
        {
            point = hit.point;
        }

        timer += Time.deltaTime;
        long_timer += Time.deltaTime;

        Vector3 left_transform = left_prosigual_walking.transform.position;
        Vector3 right_transform = right_prosigual_walking.transform.position;

        ellipseCenter = (left_transform + right_transform) / 2f;

        if (((timer > step_time) && (point -  ellipseCenter).magnitude > min_step_dist) || (long_timer > restep_time && (point != ellipseCenter)) && !(is_stepping))
        {
            if (FindFarest(left_transform, right_transform, point))
            {
                StartCoroutine(MakeStep(left_prosigual_walking, left_target_to_go));
                //MakeStep(left_prosigual_walking, ProjetOnPlain((point - right_prosigual_walking.transform.position) + (point - new Vector3())), left_target_to_go);
            }
            else
            {
                StartCoroutine(MakeStep(right_prosigual_walking, right_target_to_go));
                //MakeStep(right_prosigual_walking, ProjetOnPlain((point - left_prosigual_walking.transform.position) + (point - new Vector3())), right_target_to_go);
            }

            timer = 0;
            long_timer = 0;
        }
    }

    public IEnumerator MakeStep(prosigual_walking prosigual_walking, target_to_go target_to_go)
    {
        Vector3 vec = (target_to_go.point - prosigual_walking.transform.position) + (target_to_go.point - new Vector3());
        vec.y = target_to_go.point.y;

        Vector3 origin = prosigual_walking.carent_position;
        Vector3 end_pos = ProjetOnPlain(math_method_influence * vec + (1 - math_method_influence) * target_to_go.point);
        Vector3 half_way = (end_pos + origin) / 2;
        half_way.y = origin.y + step_hight;

        StartCoroutine(MoveObject(prosigual_walking, origin, half_way));

        while (is_stepping)
        {
            yield return null;
        }

        /*
        float mid_step_time = 0f;
        while (mid_step_time < mid_step_vaiting_time)
        {
            mid_step_time += Time.deltaTime;
            yield return null;
        }
        */

        StartCoroutine(MoveObject(prosigual_walking, half_way, end_pos));
    }

    public IEnumerator MoveObject(prosigual_walking prosigual_walking, Vector3 origin, Vector3 destination)
    {
        is_stepping = true;
        float current_movement_time = 0f;

        while (Vector3.Distance(prosigual_walking.carent_position, destination) > 0)
        {
            current_movement_time += Time.deltaTime;
            prosigual_walking.carent_position = Vector3.Lerp(origin, destination, Mathf.Sqrt(current_movement_time / step_movement_time));
            yield return null;
        }

        is_stepping = false;
    }

    public bool FindFarest(Vector3 point1, Vector3 point2, Vector3 calkpoint)
    {
        return (point1 - calkpoint).magnitude > (point2 - calkpoint).magnitude; //true if point1 is the farest
    }

    public Vector3 ProjetOnPlain(Vector3 pos)
    {
        raydown = new Ray(pos, -transform.up);
        if (Physics.Raycast(raydown, out RaycastHit hitdown, raydist, ground_mask))
        {
            return hitdown.point;
        }
        
        else
        {
            rayup = new Ray(pos, transform.up);
            if (Physics.Raycast(rayup, out RaycastHit hitup, raydist, ground_mask))
            {
                return hitup.point;
            }

        }
        
        return pos;
    }
}
