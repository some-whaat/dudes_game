using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class walk_to_point : MonoBehaviour
{
    
    public float min_velosyty;
    public float max_velosyty;
    public float lerp_velosyty = 0.01f;
    [SerializeField] private float max_dist;

    [SerializeField] Transform destination;
    [SerializeField] Transform[] destList;
    [SerializeField] float damping = 1;

    private spawner_script spawner_script;

    [SerializeField] float max_hight;
    [SerializeField] float hightmult;
    //private float hight;

    //[SerializeField] Transform left_target;
    //[SerializeField] Transform right_target;
    [SerializeField] wolking_manadger wolking_manadger;

    //[SerializeField] float needed_leg_dist;
    //private float leg_dist;

    private float transy;



    void Start()
    {
        //needed_leg_dist = (left_target.position - right_target.position).magnitude;
        spawner_script = GameObject.FindGameObjectWithTag("spawner").GetComponent<spawner_script>();
        destList = spawner_script.destList;
        destination = destList[Random.Range(0, destList.Length)];
    }


    void Update()
    {
        /*
        leg_dist = (left_target.position - right_target.position).magnitude;
        hight = max_hight / (max_hight - max_hight * (needed_leg_dist / leg_dist));

        transy = wolking_manadger.point.y * Mathf.Clamp((needed_leg_dist / leg_dist) / 10, 0, 1);
        */

        transy = wolking_manadger.point.y - Mathf.Clamp(hightmult, 0, 1);
        transform.position = new Vector3 (transform.position.x, transy, transform.position.z);

        if (transform.position != destination.position) 
        {
            /*
            Vector3 lerp = Vector3.Lerp(transform.position, target.position, lerp_velosyty);
            transform.position = new Vector3(lerp.x, transform.position.y, lerp.z);
            */
            Vector3 lookPos = destination.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(-lookPos.x, 0, -lookPos.z));

            //transform.rotation = rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

            float dist = Vector3.Distance(destination.position, transform.position);
            float step = max_velosyty * Mathf.Clamp(Mathf.InverseLerp(0, max_dist, dist), min_velosyty, 1);  
            Vector3 movtov = Vector3.MoveTowards(transform.position, destination.position, step * Time.deltaTime);
            transform.position = new Vector3(movtov.x, transy, movtov.z);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        destList = other.GetComponent<dest_skript>().near_targets;
        destination = destList[Random.Range(0, destList.Length)];
    }
}
