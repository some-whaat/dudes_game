using DG.Tweening;
using UnityEngine;


public class animation_script : MonoBehaviour
{
    manadger_script manadger_script;
    [SerializeField] wolking_manadger wolking_manadger;
    [SerializeField] movement_boids_script movement_boids_script;
    Rigidbody rb;

    [SerializeField] float catch_animation_duration = 100f;
    [SerializeField] float catch_animation_strength = 10f;
    [SerializeField] float catch_animation_douwning_duration = 0.7f;
    [SerializeField] float catch_animation_look_duration = 0.3f;
    [SerializeField] float catch_animation_hight = 0.4f;
    [SerializeField] AnimationCurve catch_animation_curve;

    public void catch_animation(bool do_restart = true)
    {
        rb = GetComponent<Rigidbody>();

        prosigual_walking left_prosigual_walking = wolking_manadger.left_prosigual_walking;
        prosigual_walking right_prosigual_walking = wolking_manadger.right_prosigual_walking;

        rb.isKinematic = true;
        movement_boids_script.enabled = false;

        wolking_manadger.math_method_influence = 0f;
        wolking_manadger.DesideAndMakeStep();
        wolking_manadger.DesideAndMakeStep();
        wolking_manadger.timer = 99999999f;
        wolking_manadger.long_timer = 999999999f;

        manadger_script = GameObject.FindWithTag("manager").GetComponent<manadger_script>();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOLookAt(-Camera.main.transform.position, catch_animation_look_duration).SetEase(Ease.InOutCubic));
        sequence.Append(transform.DOMoveY(transform.position.y - catch_animation_hight, catch_animation_douwning_duration).SetEase(Ease.OutCubic));
        sequence.Append(transform.DOShakePosition(catch_animation_duration, catch_animation_strength, fadeOut: false));

        if (do_restart)
        {
            sequence.onComplete = manadger_script.new_level;
        }
    }
}
