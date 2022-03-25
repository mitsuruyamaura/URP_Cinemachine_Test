using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpEnemy : EnemyBase
{
    protected override void MoveEnemy() {
        StartCoroutine(Jump());
        //transform.position.x, transform.position.y + 1.5f, transform.position.z
        //rb.DOJump(new Vector3(0, 1.5f, 0), 1.5f, 1, 1.5f).SetLink(gameObject).SetEase(Ease.InQuart).SetRelative().OnComplete(() => { if (enemyData.hp > 0) MoveEnemy(); });

    }


    private IEnumerator Jump() {
        while (true) {
            rb.AddForce(transform.up * 300f);

            yield return new WaitForSeconds(1.5f);
        }
    }
}
