using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_animation : MonoBehaviour
{
    [SerializeField] Transform target_obj;

    void Start()
    {
        transform.DORotate(transform.eulerAngles - new Vector3(9, 0, 0), 0.7f).SetEase(Ease.InOutSine).SetLoops(-1, loopType: LoopType.Yoyo); //добавить кучу перемкнных
    }

}
