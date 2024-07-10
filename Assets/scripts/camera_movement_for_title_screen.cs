using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement_for_title_screen : MonoBehaviour
{
    float angle;
    [SerializeField] float speed;

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);

    }
}
