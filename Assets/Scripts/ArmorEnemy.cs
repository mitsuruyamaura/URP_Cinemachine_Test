using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArmorEnemy : EnemyBase
{
    [Header("�ړ�������ꍇ�ɂ� true")]
    public bool isMove;



    protected override void MoveEnemy() {
        if (isMove) {
            base.MoveEnemy();
        }
    }

    protected override void OnTriggerEnter(Collider other) {
        // �㏑�����Č��̏���������
    }
}
