using DG.Tweening;
using System;
using UnityEngine;


public class animation_script : MonoBehaviour
{
    manadger_script manadger_script;
    [SerializeField] wolking_manadger wolking_manadger;
    [SerializeField] movement_boids_script movement_boids_script;
    //[SerializeField] Collider coll;
    Rigidbody rb;

    [SerializeField] float catch_animation_duration = 100f;
    [SerializeField] float catch_animation_strength = 10f;
    [SerializeField] float catch_animation_douwning_duration = 0.7f;
    [SerializeField] float catch_animation_look_duration = 0.3f;
    [SerializeField] float catch_animation_hight = 0.4f;
    [SerializeField] AnimationCurve catch_animation_curve;

    bool activated = false;

    /*
    private void Update()
    {
        if (activated)
        {
            transform.LookAt(-Camera.main.transform.position);
        }
    }
    */

    public void catch_animation()
    {
        activated = true;
        rb = GetComponent<Rigidbody>();

        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;

        prosigual_walking left_prosigual_walking = wolking_manadger.left_prosigual_walking;
        prosigual_walking right_prosigual_walking = wolking_manadger.right_prosigual_walking;

        rb.isKinematic = true;
        movement_boids_script.enabled = false;

        //transform.LookAt(-Camera.main.transform.position);

        wolking_manadger.math_method_influence = 0f;
        wolking_manadger.DesideAndMakeStep();
        wolking_manadger.DesideAndMakeStep();
        wolking_manadger.timer = 99999999f;
        wolking_manadger.long_timer = 999999999f;

        //wolking_manadger.enabled = false;

        //coll.enabled = false;

        manadger_script = GameObject.FindWithTag("manager").GetComponent<manadger_script>();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOLookAt(-Camera.main.transform.position, catch_animation_look_duration).SetEase(Ease.InOutCubic));
        sequence.Append(transform.DOMoveY(transform.position.y - catch_animation_hight, catch_animation_douwning_duration).SetEase(Ease.OutCubic));
        sequence.Append(transform.DOShakePosition(catch_animation_duration, catch_animation_strength, fadeOut: false));//.SetEase(Ease.InCubic));

        sequence.onComplete = manadger_script.new_level;
        //transform.DOKill();
        //manadger_script.new_level();
    }

}
