using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveFloor : GimmickBase
{
    [SerializeField]
    private GameObject[] moveObjects;

    [SerializeField]
    private Transform[] targetTrans;

    [SerializeField]
    private float duration;


    public override void TriggerGimmick() {
        GetComponent<Collider>().enabled = false;

        for (int i = 0; i < moveObjects.Length; i++) {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(moveObjects[i].transform.DOMove(targetTrans[i].position, duration).SetEase(Ease.InQuart));
            sequence.Join(moveObjects[i].transform.DOPunchScale(Vector3.one * 0.3f, duration, 30).SetEase(Ease.InQuart));
        }
    }
}
