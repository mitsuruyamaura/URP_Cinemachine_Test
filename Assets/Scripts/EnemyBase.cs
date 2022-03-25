using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

     
public class EnemyBase : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator anim;
    protected CapsuleCollider capsuleCollider;

    protected EnemyData enemyData;

    protected virtual void Start()
    {
        enemyData = new EnemyData { no = 0, hp = 2, attackPower = 1, moveSpeed = 3.5f };
        TryGetComponent(out anim);
        TryGetComponent(out capsuleCollider);
        if (TryGetComponent(out rb)){
            MoveEnemy();
        } 
    }

    
    protected virtual void MoveEnemy() {
        rb.DOMoveX(transform.position.x + Random.Range(2, 4), enemyData.moveSpeed)
            .SetLink(gameObject)
            .SetEase(Ease.InQuart)
            .OnComplete(() => { if (enemyData.hp > 0) MoveEnemy(); });
        if(anim) anim.SetBool("Walk Forward", true);
    }


    protected virtual void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Bullet bullet)) {
            CalcDamage(bullet.BulletPower);
        }
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    /// <param name="attackPower"></param>
    public virtual void CalcDamage(int attackPower) {
        enemyData.hp = Mathf.Max(0, enemyData.hp -= attackPower);
        //Debug.Log(enemyData.hp);

        if (enemyData.hp <= 0) {
            capsuleCollider.enabled = false;
            rb.isKinematic = true;
            if (anim) {
                anim.SetBool("Walk Forward", false);
                anim.SetBool("Down", true);
            }
            Destroy(gameObject, 1.5f);
            //Debug.Log("Destroy");
        }
    }
}
