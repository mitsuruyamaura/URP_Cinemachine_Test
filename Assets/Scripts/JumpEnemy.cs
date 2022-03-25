using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpEnemy : EnemyBase
{
    protected override void MoveEnemy() {
        StartCoroutine(Jump());
        
        //rb.DOJump(new Vector3(transform.position.x, transform.position.y, transform.position.z), 1.0f, 1, 1.25f).SetLink(gameObject).SetEase(Ease.InQuart).SetLoops(-1, LoopType.Yoyo);

    }


    private IEnumerator Jump() {
        while (true) {
            rb.AddForce(transform.up * 300f);

            yield return new WaitForSeconds(1.5f);
        }
    }
}
