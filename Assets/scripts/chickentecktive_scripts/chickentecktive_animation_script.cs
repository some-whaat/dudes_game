using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickentecktive_animation_script : MonoBehaviour
{
    [SerializeField] private float bounsing_hight = 1.2f;
    [SerializeField] private float bounsing_sides = 0.5f;
    [SerializeField] private float bounsing_dur = 0.5f;


    void Start()
    {
        transform.position -= new Vector3(0f, 0.3f, 0f);
        transform.DOLocalMove(transform.localPosition + new Vector3(Random.Range(-bounsing_sides, bounsing_sides), -bounsing_hight, Random.Range(-bounsing_sides, bounsing_sides)), bounsing_dur).SetEase(Ease.InOutSine).SetLoops(-1, loopType: LoopType.Yoyo);
    }

}
