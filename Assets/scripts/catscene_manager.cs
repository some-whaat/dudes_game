using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class catscene_manager : MonoBehaviour
{
    [SerializeField] GameObject chick;
    GameObject chick_cont;
    [SerializeField] float off_screen_start_pos_x;

    chickentecktive_animation_script chick_anim_script;
    speeking_manager speeking_manager;
    home_geniration home_geniration;

    //[SerializeField] string[] funcs;

    [SerializeField] int tutorial_intro_jump_amount;
    [SerializeField] float tutorial_intro_dur;
    [SerializeField] float tutorial_intro_dest;
    [SerializeField] float tutorial_intro_rot_angle;
    [SerializeField] float tutorial_intro_rot_dur;

    [SerializeField] string[] tutorial_intro_sents;
    [SerializeField] string[] tutorial_intro_sents2;

    /*
    private void Start()
    {
        speeking_manager = GetComponent<speeking_manager>();
    }
    */

    public void tutorial()
    {
        chick_anim_script = chick.GetComponent<chickentecktive_animation_script>();
        home_geniration = GetComponent<home_geniration>();
        speeking_manager = GetComponent<speeking_manager>();
        chick_cont = chick.transform.parent.gameObject;

        chick_cont.transform.localPosition = new Vector3(off_screen_start_pos_x, chick_cont.transform.localPosition.y, chick_cont.transform.localPosition.z);
        chick.SetActive(true);
        StartCoroutine(tutorial_intro());
    }

    /*
    private IEnumerator tutorial_pipline()
    {

        foreach(string func_sting  in funcs)
        {
            string[] func = func_sting.Split(' ');

            if (func.Length == 2 )
            {
                Invoke(func[0], float.Parse(func[1]));
            }
        }

    }
    */


    public IEnumerator tutorial_intro()
    {
        chick_anim_script.jump(tutorial_intro_dest, 3, tutorial_intro_dur);

        while (chick_anim_script.is_jumping)
        {
            yield return null;
        }

        chick_anim_script.rotate(-10f, tutorial_intro_rot_dur,1);

        while (chick_anim_script.is_rot)
        {
            yield return null;
        }

        chick_anim_script.do_idle = true;

        speeking_manager.start_speaking(tutorial_intro_sents);

        while (speeking_manager.is_speaking)
        {
            yield return null;
        }

        chick_anim_script.do_idle = false;

        chick_anim_script.rotate(77f, 0.5f, 3);

        while (chick_anim_script.is_rot)
        {
            yield return null;
        }

        chick_anim_script.jump(-1.4f, 2, tutorial_intro_dur);

        while (chick_anim_script.is_jumping)
        {
            yield return null;
        }

        chick_anim_script.rotate(-165f, 0.5f, 3);

        while (chick_anim_script.is_rot)
        {
            yield return null;
        }

        chick_anim_script.do_idle = true;
        speeking_manager.start_speaking(tutorial_intro_sents2);


        tutorial_mechanics();
    }

    void tutorial_mechanics()
    {
        home_geniration.genirate_level();
    }

}
