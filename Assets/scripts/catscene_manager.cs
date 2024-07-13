using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class catscene_manager : MonoBehaviour
{
    [SerializeField] GameObject chick;
    [SerializeField] Vector3 off_screen_start_pos;

    chickentecktive_animation_script chick_anim_script;

    private void Start()
    {
        chick_anim_script = chick.GetComponent<chickentecktive_animation_script>();
        //chick_anim_script.jump();
    }

    public void tutorial()
    {
        chick.transform.position = off_screen_start_pos;
        chick.SetActive(true);
        chick_anim_script.tutorial_first_jumps();
    }
}
