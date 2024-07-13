using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickentecktive_animation_script : MonoBehaviour
{
    [SerializeField] private float bounsing_hight = 1.2f;
    [SerializeField] private float bounsing_dur = 0.5f;

    [SerializeField] float speaking_anim_dur;

    [SerializeField] private float jump_hight = 1f;
    [SerializeField] private float jump_duration = 2f;
    [SerializeField] private float screen_side = 1.5f;

    [SerializeField] private float rot_dur = 1.4f;
    [SerializeField] private int rot_step_amound = 4;
    [SerializeField] private Vector3 rot_left;
    private Vector3 rot_right;

    [SerializeField] private AnimationCurve curve;


    public bool is_speaking_anim = false;
    public bool do_idle = false;
    bool is_whatch_right = true;

    Transform conteiner;
    [SerializeField] GameObject beak_up;
    [SerializeField] GameObject beak_down;
    [SerializeField] GameObject wing_right;
    [SerializeField] GameObject wing_left;
    [SerializeField] GameObject target_right;
    [SerializeField] GameObject target_left;

    Tween idle_animation;

    public Sequence speaking_seq;

    Vector3 parts_original_original_rot;


    void Start()
    {
        parts_original_original_rot = new Vector3(-90, 0, 0);
        rot_right = transform.eulerAngles;
        conteiner = transform.parent;

        transform.position -= new Vector3(0f, 0.3f, 0f);
        //idle_animation = transform.DOLocalMove(transform.localPosition + new Vector3(Random.Range(-bounsing_sides, bounsing_sides), -bounsing_hight, Random.Range(-bounsing_sides, bounsing_sides)), bounsing_dur).SetEase(Ease.InOutSine).SetLoops(-1, loopType: LoopType.Yoyo); 
    }

    private void Update()
    {
        if (!do_idle && idle_animation != null)
        {
            idle_animation.Kill();
        }
        else if (do_idle && idle_animation == null)
        {
            idle_animation = transform.DOLocalMoveY(transform.localPosition.y - bounsing_hight, bounsing_dur).SetEase(Ease.InOutSine).SetLoops(-1, loopType: LoopType.Yoyo);
        }

    }

    public void speaking_animation()
    {
        speaking_seq = DOTween.Sequence();

        speaking_seq.OnStart(() => is_speaking_anim = true);

        speaking_seq.SetLoops(2, LoopType.Yoyo);

        speaking_seq.Append(beak_down.transform.DOLocalMove(Vector3.zero, speaking_anim_dur).SetEase(Ease.InOutCirc));
        speaking_seq.Join(beak_down.transform.DOLocalRotate(parts_original_original_rot, speaking_anim_dur).SetEase(Ease.InOutCirc));
        speaking_seq.Join(beak_up.transform.DOLocalMove(Vector3.zero, speaking_anim_dur).SetEase(Ease.InOutCirc));
        speaking_seq.Join(beak_up.transform.DOLocalRotate(parts_original_original_rot, speaking_anim_dur).SetEase(Ease.InOutCirc));
        speaking_seq.Join(wing_right.transform.DOLocalMove(Vector3.zero, speaking_anim_dur).SetEase(Ease.InOutCirc));
        //speaking_seq.Join(wing_right.transform.DOLocalRotate(parts_original_original_rot, speaking_anim_dur).SetEase(Ease.InOutCirc));
        speaking_seq.Join(wing_left.transform.DOLocalMove(Vector3.zero, speaking_anim_dur).SetEase(Ease.InOutCirc));
        //speaking_seq.Join(wing_left.transform.DOLocalRotate(parts_original_original_rot, speaking_anim_dur).SetEase(Ease.InOutCirc));

        speaking_seq.OnComplete(() => is_speaking_anim = false);
    }

    [ContextMenu("jump")]
    public void jump() //(float jump_dist, int num_jumps)
    {
        /*
        jump_screen_side = transform.parent.transform.localScale.x * screen_side_ratio;

        Vector3 jump_dest = new Vector3();
        jump_dest.x += Mathf.Clamp(((jump_screen_side * 2) / screen_fraction) + transform.localPosition.x, -jump_screen_side, jump_screen_side);
        */
        float jump_dist = 1f;
        int num_jumps = 2;


        Sequence jumping_seq = DOTween.Sequence();

        Vector3 jump_dest = conteiner.transform.localPosition;
        jump_dest.x = Mathf.Clamp(jump_dest.x + jump_dist, -screen_side, screen_side);


        jumping_seq.Append(conteiner.transform.DOLocalJump(jump_dest, jump_hight, num_jumps, jump_duration).SetEase(Ease.InOutSine));
        jumping_seq.Join(target_left.transform.DOLocalJump(target_left.transform.localPosition, jump_hight, num_jumps, jump_duration).SetEase(Ease.InOutSine));
        jumping_seq.Join(target_right.transform.DOLocalJump(target_right.transform.localPosition, jump_hight, num_jumps, jump_duration).SetEase(Ease.InOutSine));

    }

    [ContextMenu("rotate")]
    void rotate() //(Vector3 end_rot)
    {
        Sequence rotate_seq = DOTween.Sequence();

        // /*
        Vector3 end_rot;

        if (is_whatch_right)
        {
            end_rot = rot_left;
            is_whatch_right = false;
        }
        else
        {
            end_rot = rot_right;
            is_whatch_right = true;
        }
        //*/

        rotate_seq.Append(transform.DORotate(end_rot, rot_dur).SetEase(Ease.InOutSine));
        rotate_seq.Join(target_left.transform.DOLocalJump(target_left.transform.localPosition, jump_hight, rot_step_amound, rot_dur).SetEase(curve));//.SetEase(Ease.InOutQuart));
        rotate_seq.Join(target_right.transform.DOLocalJump(target_right.transform.localPosition, jump_hight, rot_step_amound, rot_dur).SetEase(curve).SetDelay((rot_dur/ rot_step_amound)/2));//.SetEase(Ease.InOutQuart)) ;
    }

    public void tutorial_first_jumps()
    {

    }



}
