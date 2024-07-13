using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickentecktive_legs : MonoBehaviour
{
    [SerializeField] Transform stable_body;

    public bool do_fix_pos;

    void Update()
    {
        if (do_fix_pos)
        {
            transform.position = stable_body.position;
        }
    }
}
