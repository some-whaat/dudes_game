using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [SerializeField] float step_time;
    [SerializeField] float min_step_dist;
    [SerializeField] float restep_time;

    [SerializeField] target_to_go left_target_to_go;
    [SerializeField] target_to_go right_target_to_go;

    [SerializeField] float raydist = 5;


    void Update()
    {
        ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out RaycastHit hit, 10, ground_mask))
        {
            point = hit.point;
        }

        timer += Time.deltaTime;
        long_timer += Time.deltaTime;

        ellipseCenter = (left_prosigual_walking.transform.position + right_prosigual_walking.transform.position) / 2f;

        if (((timer > step_time) && (point -  ellipseCenter).magnitude > min_step_dist) || (long_timer > restep_time && (point != ellipseCenter)))
        {
            if (FindFarest(left_prosigual_walking.transform.position, right_prosigual_walking.transform.position, point))
            {
                MakeStep(left_prosigual_walking, left_target_to_go);
                //MakeStep(left_prosigual_walking, ProjetOnPlain((point - right_prosigual_walking.transform.position) + (point - new Vector3())), left_target_to_go);
            }
            else
            {
                MakeStep(right_prosigual_walking, right_target_to_go);
                //MakeStep(right_prosigual_walking, ProjetOnPlain((point - left_prosigual_walking.transform.position) + (point - new Vector3())), right_target_to_go);
            }

            timer = 0;
            long_timer = 0;
        }
    }

    public void MakeStep(prosigual_walking prosigual_walking, target_to_go target_to_go)
    {
        Vector3 vec = (target_to_go.point - prosigual_walking.transform.position) + (target_to_go.point - new Vector3());
        vec.y = target_to_go.point.y;
        prosigual_walking.carent_position = ProjetOnPlain((3*vec + 2*target_to_go.point) / 5);
        //prosigual_walking.carent_position = ProjetOnPlain(vec);
    }

    public bool FindFarest(Vector3 point1, Vector3 point2, Vector3 calkpoint)
    {
        return (point1 - calkpoint).magnitude > (point2 - calkpoint).magnitude; //true если дальше point1
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
