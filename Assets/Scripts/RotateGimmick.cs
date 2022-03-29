using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateGimmick : GimmickBase
{
    [SerializeField]
    private GameObject targetObj;

    [SerializeField]
    private bool isLoop;

    [SerializeField]
    private float duration;

    private Vector3 originPos;


    public override void TriggerGimmick() {
        GetComponent<Collider>().enabled = false;
        base.TriggerGimmick();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(targetObj.transform.DORotate(originPos, duration).SetEase(Ease.InQuart));

        if (isLoop) {
            sequence.Append(targetObj.transform.DORotate(originPos, duration).SetEase(Ease.InQuart)).SetLink(gameObject).SetLoops(-1, LoopType.Yoyo);

        }

    }

}
