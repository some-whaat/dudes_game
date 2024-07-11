using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_animation : MonoBehaviour
{
    [SerializeField] Transform target_obj;

    void Start()
    {
        
    }

    private void Update()
    {
        float dist = Vector3.Distance(target_obj.position, transform.position);

        Vector2.MoveTowards(transform.position, target_obj.position, (dist * 0.001f) + dist * 0.001f * Time.deltaTime);
    }

}
