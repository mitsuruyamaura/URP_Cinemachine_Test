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

        for (int i = 0; i < moveObjects.Length; i++) {
            moveObjects[i].transform.DOMove(targetTrans[i].position, duration).SetEase(Ease.InQuart);
        }
    }
}
