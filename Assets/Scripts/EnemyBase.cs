using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

     
public class EnemyBase : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private CapsuleCollider capsuleCollider;

    private EnemyData enemyData;

    void Start()
    {
        enemyData = new EnemyData { no = 0, hp = 2, attackPower = 1, moveSpeed = 3.5f };
        TryGetComponent(out anim);
        TryGetComponent(out capsuleCollider);
        if (TryGetComponent(out rb)){
            MoveEnemy();
        } 
    }

    
    private void MoveEnemy() {
        rb.DOMoveX(transform.position.x + Random.Range(2, 4), enemyData.moveSpeed)
            .SetLink(gameObject)
            .SetEase(Ease.InQuart)
            .OnComplete(() => { if (enemyData.hp > 0) MoveEnemy(); });
        anim.SetBool("Walk Forward", true);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Bullet bullet)) {
            enemyData.hp = Mathf.Max(0, enemyData.hp -= bullet.BulletPower);
            //Debug.Log(enemyData.hp);

            if (enemyData.hp <= 0) {
                capsuleCollider.enabled = false;
                rb.isKinematic = true;
                anim.SetBool("Walk Forward", false);
                anim.SetBool("Down", true);
                Destroy(gameObject, 1.5f);
                //Debug.Log("Destroy");
            }
        }
    }
}
