using DG.Tweening;
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
    [SerializeField] float catch_animation_hight = 0.4f;
    [SerializeField] AnimationCurve catch_animation_curve;

    /*
    public void start_catch_animation()
    {
        wolking_manadger.enabled = false;
        movement_boids_script.enabled = false;
    }
    */

    public void catch_animation()
    {
        rb = GetComponent<Rigidbody>();

        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        wolking_manadger.enabled = false;
        movement_boids_script.enabled = false;
        //coll.enabled = false;

        manadger_script = GameObject.FindWithTag("manager").GetComponent<manadger_script>();


        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveY(transform.position.y - catch_animation_hight, catch_animation_douwning_duration).SetEase(Ease.OutCubic));
        sequence.Append(transform.DOShakePosition(catch_animation_duration, catch_animation_strength, fadeOut: false));//.SetEase(Ease.InCubic));

        sequence.onComplete = manadger_script.new_level;
        //transform.DOKill();
        //manadger_script.new_level();
    }
}
