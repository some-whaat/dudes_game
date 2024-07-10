using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickentecktive_legs : MonoBehaviour
{
    [SerializeField] Transform stable_body;
    /*
    Vector3 local_pos;

    void Start()
    {
    }
    */
    void Update()
    {
        transform.position = stable_body.position;// + transform.parent.transform.position;
    }
}
