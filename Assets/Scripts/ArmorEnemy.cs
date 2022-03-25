using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArmorEnemy : EnemyBase
{
    [Header("ˆÚ“®‚³‚¹‚éê‡‚É‚Í true")]
    public bool isMove;



    protected override void MoveEnemy() {
        if (isMove) {
            base.MoveEnemy();
        }
    }

    protected override void OnTriggerEnter(Collider other) {
        // ã‘‚«‚µ‚ÄŒ³‚Ìˆ—‚ğÁ‚·
    }
}
