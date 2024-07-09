using System.Collections;
using UnityEngine;
using DG.Tweening;

public class wolking_manadger : MonoBehaviour
{
    Ray ray;
    Ray rayup;
    Ray raydown;
    public LayerMask ground_mask;

    public Vector3 point;
    public float timer;
    public float long_timer;
    private Vector3 ellipseCenter;

    public prosigual_walking left_prosigual_walking;
    public prosigual_walking right_prosigual_walking;

    [SerializeField] private float step_movement_time = 0.7f;
    //[SerializeField] private float mid_step_vaiting_time = 0.0008f;

    public float math_method_influence = 0.75f;

    [SerializeField] private float step_hight = 1;
    [SerializeField] float step_time;
    [SerializeField] float min_step_dist;
    [SerializeField] float restep_time;

    public target_to_go left_target_to_go;
    public target_to_go right_target_to_go;

    [SerializeField] float raydist = 5;

    [SerializeField] movement_boids_script movement_boids_script;
    [SerializeField] AnimationCurve curve;

    bool is_stepping = false;
    bool is_left_turn = true;

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

        if ((((timer > step_time) && (point - ellipseCenter).magnitude > min_step_dist) || (long_timer > restep_time && (point != ellipseCenter))) && !is_stepping)
        {
            DesideAndMakeStep();

            timer = 0;
            long_timer = 0;
        }

        movement_boids_script.hight_y = point.y;
    }

    public void DesideAndMakeStep()
    {
        if (is_left_turn)//(FindFarest(left_transform, right_transform, point))
        {
            MakeStep(left_prosigual_walking, left_target_to_go);
            //MakeStep(left_prosigual_walking, ProjetOnPlain((point - right_prosigual_walking.transform.position) + (point - new Vector3())), left_target_to_go);
            is_left_turn = false;
        }
        else
        {
            MakeStep(right_prosigual_walking, right_target_to_go);
            //MakeStep(right_prosigual_walking, ProjetOnPlain((point - left_prosigual_walking.transform.position) + (point - new Vector3())), right_target_to_go);
            is_left_turn = true;
        }
    }

    void MakeStep(prosigual_walking prosigual_walking, target_to_go target_to_go)
    {
        Vector3 vec = 2*target_to_go.point - prosigual_walking.transform.position;
        vec.y = target_to_go.point.y;

        Vector3 end_pos = ProjetOnPlain(math_method_influence * vec + (1 - math_method_influence) * target_to_go.point);

        
        is_stepping = true;
        prosigual_walking.is_steping = true;
        prosigual_walking.transform.DOJump(end_pos, step_hight, 1, step_movement_time).SetEase(curve);
        prosigual_walking.carent_position = end_pos;
        prosigual_walking.is_steping = false;
        is_stepping = false;
    }

/*
        public IEnumerator smoothMakeStep(prosigual_walking prosigual_walking, target_to_go target_to_go)
    {
        Vector3 vec = (target_to_go.point - prosigual_walking.transform.position) + (target_to_go.point - new Vector3());
        vec.y = target_to_go.point.y;

        Vector3 origin = prosigual_walking.carent_position;
        Vector3 end_pos = ProjetOnPlain(math_method_influence * vec + (1 - math_method_influence) * target_to_go.point);
        Vector3 half_way = (end_pos + origin) / 2;
        half_way.y = origin.y + step_hight;

        is_stepping = true;
        float current_movement_time = 0f;

        while (Vector3.Distance(prosigual_walking.carent_position, half_way) > 0)
        {
            current_movement_time += Time.deltaTime;
            prosigual_walking.carent_position = Vector3.Lerp(origin, half_way, Mathf.Sqrt(current_movement_time / step_movement_time));
            yield return null;
        }

        current_movement_time = 0f;

        while (Vector3.Distance(prosigual_walking.carent_position, end_pos) > 0)
        {
            current_movement_time += Time.deltaTime;
            prosigual_walking.carent_position = Vector3.Lerp(half_way, end_pos, Mathf.Pow(current_movement_time / step_movement_time, 2));
            yield return null;
        }

        is_stepping = false;
    }

    
    public IEnumerator MakeStep1(prosigual_walking prosigual_walking, target_to_go target_to_go)
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
    */

    bool FindFarest(Vector3 point1, Vector3 point2, Vector3 calkpoint)
    {
        return (point1 - calkpoint).magnitude > (point2 - calkpoint).magnitude; //true if point1 is the farest
    }

    Vector3 ProjetOnPlain(Vector3 pos)
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
