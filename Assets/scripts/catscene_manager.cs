using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class catscene_manager : MonoBehaviour
{
    [SerializeField] GameObject chick;
    [SerializeField] GameObject dude_vis;
    [SerializeField] timer_script timer_script;
    GameObject chick_cont;
    [SerializeField] float off_screen_start_pos_x;

    chickentecktive_animation_script chick_anim_script;
    speeking_manager speeking_manager;
    home_geniration home_geniration;
    choose_dude choose_dude;
    manadger_script manadger_script;
    Camera cam;

    public bool had_finded_dude = false;

    //[SerializeField] string[] funcs;

    [SerializeField] int tutorial_intro_jump_amount;
    [SerializeField] float tutorial_intro_dur;
    [SerializeField] float tutorial_intro_dest;
    [SerializeField] float tutorial_intro_rot_angle;
    [SerializeField] float tutorial_intro_rot_dur;

    [SerializeField] string[] tutorial_intro_sents;
    [SerializeField] string[] tutorial_intro_sents2;
    [SerializeField] string[] mech_intro_sents;
    //[SerializeField] string[] mech_sents;
    [SerializeField] string[] mech_sents_cam_rot;
    [SerializeField] string[] mech_sents_zoom;
    [SerializeField] string[] mech_sents_cam_changepos;
    [SerializeField] string[] mech_sents_cam_changepos2;
    [SerializeField] string[] mech_sents_dude_exp;
    [SerializeField] string[] mech_sents_dudes;
    [SerializeField] string[] mech_sents_timer_intro;
    [SerializeField] string[] mech_sents_timer;
    [SerializeField] string[] mech_sents_ending;



    public void tutorial()
    {
        chick_anim_script = chick.GetComponent<chickentecktive_animation_script>();
        manadger_script = GetComponent<manadger_script>();
        home_geniration = manadger_script.home_geniration;
        speeking_manager = GetComponent<speeking_manager>();
        choose_dude = GameObject.FindGameObjectWithTag("spawner").GetComponent<choose_dude>();
        chick_cont = chick.transform.parent.gameObject;
        dude_vis.SetActive(false);
        cam = Camera.main;

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

        yield return new WaitForSeconds(0.03f);

        chick_anim_script.rotate(15f, tutorial_intro_rot_dur, 2);

        while (chick_anim_script.is_rot)
        {
            yield return null;
        }

        //yield return new WaitForSeconds(0.1f);

        chick_anim_script.idle_ani();

        speeking_manager.start_speaking(tutorial_intro_sents);

        while (speeking_manager.is_speaking)
        {
            yield return null;
        }

        chick_anim_script.stop_idle_ani();

        chick_anim_script.rotate(90f, 0.5f, 3);

        while (chick_anim_script.is_rot)
        {
            yield return null;
        }

        chick_anim_script.jump(-1.4f, 2, tutorial_intro_dur);

        while (chick_anim_script.is_jumping)
        {
            yield return null;
        }

        chick_anim_script.rotate(-166f, 0.5f, 3);

        while (chick_anim_script.is_rot)
        {
            yield return null;
        }

        chick_anim_script.idle_ani();
        speeking_manager.start_speaking(tutorial_intro_sents2);

        while (speeking_manager.is_speaking)
        {
            yield return null;
        }

        PlayerPrefs.SetInt("amount_of_floors", 2);
        PlayerPrefs.SetInt("amount_spawned_plates", 22);
        PlayerPrefs.SetInt("amound_dudes_to_spawn", 4);

        PlayerPrefs.SetInt("nomber_of_iteration", 0);

        home_geniration.genirate_level();
        StartCoroutine(tutorial_mechanics());
    }

    IEnumerator tutorial_mechanics()
    {
        speeking_manager.hide_bubble = false;
        speeking_manager.start_speaking(mech_intro_sents);

        while (speeking_manager.is_speaking)
        {
            yield return null;
        }

        speeking_manager.is_unskip = true;

        Vector3 cam_root = cam.transform.eulerAngles;

        speeking_manager.start_speaking(mech_sents_cam_rot);

        while (Vector3.Distance(cam_root, cam.transform.eulerAngles) < 55f)
        {
            yield return null;
        }

        float cam_zooom = cam.orthographicSize;
        speeking_manager.start_speaking(mech_sents_zoom);

        while (Mathf.Abs(cam_zooom - cam.orthographicSize) < 6f)
        {
            yield return null;
        }

        speeking_manager.is_unskip = false;
        speeking_manager.start_speaking(mech_sents_cam_changepos);

        while (speeking_manager.is_speaking)
        {
            yield return null;
        }

        speeking_manager.is_unskip = true;

        camera_whatch cam_script = cam.GetComponent<camera_whatch>();
        Transform cam_taarget = cam_script.target;
        speeking_manager.start_speaking(mech_sents_cam_changepos2);

        while (cam_taarget == cam_script.target)
        {
            yield return null;
        }

        speeking_manager.is_unskip = false;
        speeking_manager.start_speaking(mech_sents_dude_exp);

        while (speeking_manager.is_speaking)
        {
            yield return null;
        }

        speeking_manager.is_unskip = true;
        dude_vis.SetActive(true);
        speeking_manager.start_speaking(mech_sents_dudes);

        while (!had_finded_dude)
        {
            yield return null;
        }

        speeking_manager.is_unskip = false;
        speeking_manager.start_speaking(mech_sents_timer_intro);

        while (speeking_manager.is_speaking)
        {
            yield return null;
        }

        timer_script.start_timer = false;
        timer_script.enabled = true;
        speeking_manager.start_speaking(mech_sents_timer);

        while (speeking_manager.is_speaking)
        {
            yield return null;
        }

        speeking_manager.hide_bubble = true;
        speeking_manager.start_speaking(mech_sents_ending);

        timer_script.start_timer = true;

        chick_anim_script.stop_idle_ani();
        chick_anim_script.jump(-3f, 6, 3f);

        while (chick_anim_script.is_jumping)
        {
            yield return null;
        }

        //PlayerPrefs.SetInt("was_tutorial", 1);
        chick_cont.SetActive(false);
        manadger_script.do_tutorial = false;
        choose_dude.Dude_to_Find();
    }

}
