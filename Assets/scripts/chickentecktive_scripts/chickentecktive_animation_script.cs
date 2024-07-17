using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickentecktive_animation_script : MonoBehaviour
{
    [SerializeField] private float bounsing_hight = 1.2f;
    [SerializeField] private float bounsing_dur = 0.5f;
    [SerializeField] private float bounsing_rot = 10f;

    [SerializeField] float speaking_anim_dur;

    [SerializeField] private float jump_hight = 1f;
    [SerializeField] private float screen_side = 1.5f;

    //[SerializeField] private int rot_step_amound = 4;
    [SerializeField] private float rot_left;
    

    [SerializeField] private AnimationCurve curve;


    public bool is_speaking_anim = false;
    //public bool do_idle = false;
    //bool is_whatch_right = true;
    public bool is_jumping = false;
    public bool is_rot = false;

    Transform conteiner;
    [SerializeField] GameObject beak_up;
    [SerializeField] GameObject beak_down;
    [SerializeField] GameObject wing_right;
    [SerializeField] GameObject wing_left;
    [SerializeField] GameObject target_right;
    [SerializeField] GameObject target_left;

    Sequence idle_animation;

    public Sequence speaking_seq;
    Sequence rotate_seq;
    Sequence jumping_seq;

    Vector3 parts_original_original_rot;
    Vector3 original_pos_y;

    chickentecktive_legs chickentecktive_legs_right;
    chickentecktive_legs chickentecktive_legs_left;

    void Start()
    {
        parts_original_original_rot = new Vector3(-90, 0, 0);
        //rot_right = transform.eulerAngles;
        conteiner = transform.parent;

        original_pos_y = Vector3.zero;
        original_pos_y.y = transform.localPosition.y;

        chickentecktive_legs_right = target_right.GetComponent<chickentecktive_legs>();
        chickentecktive_legs_left = target_left.GetComponent<chickentecktive_legs>();
    }
/*
    private void Update()
    {
        //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        
        if (!do_idle && idle_animation != null)
        {
            chickentecktive_legs_right.do_fix_pos = false;
            chickentecktive_legs_left.do_fix_pos = false;
            idle_animation.Kill();
        }
        else if (do_idle && idle_animation == null)
        {
            target_right.GetComponent<chickentecktive_legs>().fix_pos();
            target_left.GetComponent<chickentecktive_legs>().fix_pos();

            //transform.position -= new Vector3(0f, 0.3f, 0f);

            idle_animation = DOTween.Sequence();
            idle_animation.SetLoops(-1, loopType: LoopType.Yoyo);

            Transform first_trans = transform;
            idle_animation.Append(transform.DOLocalMoveY(transform.localPosition.y - bounsing_hight, bounsing_dur).SetEase(Ease.InOutSine));
            idle_animation.Join(transform.DOLocalRotate(first_trans.localEulerAngles + new Vector3(0, bounsing_rot, 0), bounsing_dur).SetEase(Ease.InOutSine));
            idle_animation.Append(transform.DOLocalMoveY(first_trans.localPosition.y, bounsing_dur).SetEase(Ease.InOutSine));
            idle_animation.Join(transform.DOLocalRotate(first_trans.localEulerAngles - new Vector3(0, bounsing_rot, 0), bounsing_dur).SetEase(Ease.InOutSine));
        }
        
    }
*/
    public void stop_idle_ani()
    {
        

        idle_animation.Kill();

        //transform.localPosition = original_pos_y; WTF???
        //transform.DOLocalMoveY(original_pos_y, 0.09f);

        chickentecktive_legs_right.do_fix_pos = false;
        chickentecktive_legs_left.do_fix_pos = false;
    }

    public void idle_ani()
    {
        chickentecktive_legs_right.fix_pos();
        chickentecktive_legs_left.fix_pos();

        //transform.localPosition -= new Vector3(0f, 0.05f, 0f);
        //transform.DOLocalMoveY(transform.localPosition.y - 0.05f, 0.11f);

        idle_animation = DOTween.Sequence();
        idle_animation.SetLoops(-1, loopType: LoopType.Yoyo);

        Transform first_trans = transform;
        idle_animation.Append(transform.DOLocalMoveY(transform.localPosition.y - bounsing_hight, bounsing_dur).SetEase(Ease.InOutSine));
        idle_animation.Join(transform.DOLocalRotate(first_trans.localEulerAngles + new Vector3(0, bounsing_rot, 0), bounsing_dur).SetEase(Ease.InOutSine));
        idle_animation.Append(transform.DOLocalMoveY(first_trans.localPosition.y, bounsing_dur).SetEase(Ease.InOutSine));
        idle_animation.Join(transform.DOLocalRotate(first_trans.localEulerAngles - new Vector3(0, bounsing_rot, 0), bounsing_dur).SetEase(Ease.InOutSine));
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
    public void jump(float jump_dest_x, int num_jumps, float jump_dUR)
    {
        conteiner = transform.parent;
        is_jumping = true;
        /*
        jump_screen_side = transform.parent.transform.localScale.x * screen_side_ratio;

        Vector3 jump_dest = new Vector3();
        jump_dest.x += Mathf.Clamp(((jump_screen_side * 2) / screen_fraction) + transform.localPosition.x, -jump_screen_side, jump_screen_side);
        */

        Vector3 jump_dest = conteiner.transform.localPosition;
        //jump_dest.x = Mathf.Clamp(jump_dest.x + jump_dist, -screen_side, screen_side);
        jump_dest.x = jump_dest_x;

        jumping_seq = DOTween.Sequence();


        jumping_seq.Append(conteiner.transform.DOLocalJump(jump_dest, jump_hight, num_jumps, jump_dUR).SetEase(curve));
        jumping_seq.Join(target_left.transform.DOLocalJump(target_left.transform.localPosition, jump_hight, num_jumps, jump_dUR).SetEase(curve));
        jumping_seq.Join(target_right.transform.DOLocalJump(target_right.transform.localPosition, jump_hight, num_jumps, jump_dUR).SetEase(curve));

        jumping_seq.OnComplete(() => is_jumping = false);
    }

    [ContextMenu("rotate")]
    public void rotate(float rot_angle, float rot_dur, int rot_step_amound)
    {
        is_rot = true;

        rotate_seq = DOTween.Sequence();

        /*
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
        
        Transform end_rot = transform;
        end_rot.Rotate(Vector3.up, rot_angle);
        */

        Vector3 end_rot = Vector3.zero;
        end_rot.y = rot_angle; // + transform.eulerAngles.y;

        //rotate_seq.SetLoops(loops_am, LoopType.Incremental);

        rotate_seq.Append(transform.DOLocalRotate(end_rot + transform.localEulerAngles, rot_dur).SetEase(Ease.InOutQuad));
        rotate_seq.Join(target_left.transform.DOLocalJump(target_left.transform.localPosition, jump_hight, rot_step_amound, rot_dur).SetEase(Ease.InOutQuad));//.SetEase(Ease.InOutQuart));
        rotate_seq.Join(target_right.transform.DOLocalJump(target_right.transform.localPosition, jump_hight, rot_step_amound, rot_dur).SetEase(Ease.InOutQuad).SetDelay((rot_dur/ rot_step_amound)/2));//.SetEase(Ease.InOutQuart)) ;

        rotate_seq.OnComplete(() => is_rot = false);
    }





}
