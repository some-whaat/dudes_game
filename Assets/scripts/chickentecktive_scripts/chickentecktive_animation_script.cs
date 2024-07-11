using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickentecktive_animation_script : MonoBehaviour
{
    [SerializeField] private float bounsing_hight = 1.2f;
    [SerializeField] private float bounsing_sides = 0.5f;
    [SerializeField] private float bounsing_dur = 0.5f;

    public bool do_idle = false;

    [SerializeField] GameObject beak_up;
    [SerializeField] GameObject beak_down;
    [SerializeField] GameObject wing_right;
    [SerializeField] GameObject wing_left;


    Tween idle_animation;

    Vector3 beak_up_original_pos;
    Vector3 beak_up_original_rot;
    Vector3 beak_down_original_pos;
    Vector3 beak_down_original_rot;
    [SerializeField] Vector3 beak_original_original_rot;

    [SerializeField] float speaking_anim_dur;


    void Start()
    {
        beak_up_original_pos = beak_up.transform.localPosition;
        beak_up_original_rot = beak_up.transform.eulerAngles;
        beak_down_original_pos = beak_down.transform.localPosition;
        beak_down_original_rot = beak_down.transform.eulerAngles;
        

        transform.position -= new Vector3(0f, 0.3f, 0f);
        //idle_animation = transform.DOLocalMove(transform.localPosition + new Vector3(Random.Range(-bounsing_sides, bounsing_sides), -bounsing_hight, Random.Range(-bounsing_sides, bounsing_sides)), bounsing_dur).SetEase(Ease.InOutSine).SetLoops(-1, loopType: LoopType.Yoyo);
    }

    private void Update()
    {
        if (!do_idle)
        {
            idle_animation.Kill();
        }
        else if (idle_animation == null)
        {
            idle_animation = transform.DOLocalMove(transform.localPosition + new Vector3(Random.Range(-bounsing_sides, bounsing_sides), -bounsing_hight, Random.Range(-bounsing_sides, bounsing_sides)), bounsing_dur).SetEase(Ease.InOutSine).SetLoops(-1, loopType: LoopType.Yoyo);
        }
    }

    [ContextMenu("aaaaaaaaaaaaaaaaaaaaaaaa")]
    public void speaking_animation()
    {
        Sequence speaking_seq = DOTween.Sequence();

        speaking_seq.SetLoops(99, LoopType.Yoyo);

        speaking_seq.Append(beak_down.transform.DOLocalMove(Vector3.zero, speaking_anim_dur).SetEase(Ease.InOutCirc));
        speaking_seq.Join(beak_down.transform.DOLocalRotate(beak_original_original_rot, speaking_anim_dur).SetEase(Ease.InOutCirc));
        speaking_seq.Join(beak_up.transform.DOLocalMove(Vector3.zero, speaking_anim_dur).SetEase(Ease.InOutCirc));
        speaking_seq.Join(beak_up.transform.DOLocalRotate(beak_original_original_rot, speaking_anim_dur).SetEase(Ease.InOutCirc));
    }

}
