using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickentecktive_legs : MonoBehaviour
{
    [SerializeField] Transform stable_body;

    public bool do_fix_pos;

    private void Start()
    {
        //transform.localPosition += new Vector3(0, 15f, 0);
    }

    void Update()
    {
        if (do_fix_pos)
        {
            transform.position = stable_body.position;
        }
    }

    public void fix_pos()
    {
        stable_body.position = transform.position;
        do_fix_pos = true;
    }
}
