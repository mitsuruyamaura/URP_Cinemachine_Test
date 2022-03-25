using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArmorEnemy : EnemyBase
{
    [Header("移動させる場合には true")]
    public bool isMove;



    protected override void MoveEnemy() {
        if (isMove) {
            base.MoveEnemy();
        }
    }

    protected override void OnTriggerEnter(Collider other) {
        // 上書きして元の処理を消す
    }
}
