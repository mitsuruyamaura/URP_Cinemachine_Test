using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃のオーナーの種類
/// </summary>
public enum OwnerType
{
    Player,
    Enemy
}

public class HitController : MonoBehaviour
{
    [SerializeField]
    private float knockbackPower = 5.0f;

    [SerializeField] 
    private OwnerType ownerType;

    [SerializeField]
    private float forcePower = 1000.0f;

    private float range = 3.0f;
    private float height = 2.0f;
    

    /// <summary>
    /// 初期設定
    /// </summary>
    /// <param name="ownerType"></param>
    public void SetUpOwner(OwnerType ownerType) {
        this.ownerType = ownerType;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        // ここに書くと先にアイテムを落とす。そのあとに敵がノックバックするので、敵は拾いにくくなる
        JedgeKnockBackItem(other.transform);

        // この攻撃のオーナーがプレイヤーで、攻撃対象が敵なら
        if (ownerType == OwnerType.Player && other.TryGetComponent(out EnemyBase enemy)) {
            
            // 敵をノックバック
            KnockbackTarget(enemy.transform);
            
            // 敵のステート変更。合わせて移動停止させる
            //enemy.PrepareChangeState(ChaseEnemy.ENEMY_STATE_TYPE.STOP, 2.0f);
        } 
        // この攻撃のオーナーが敵で、攻撃対象がプレイヤーなら
        else if (ownerType == OwnerType.Enemy && other.TryGetComponent(out PlayerController player)) {

            // プレイヤーをノックバック
            KnockbackTarget(player.transform);
        }
        
        // ここに書くと、ノックバック後の相手の位置からアイテムが落ちる。相手が拾いやすくなる
        // どちらでも
        //JedgeKnockBackItem(other.transform);

    }

    /// <summary>
    /// アイテムを持っているか判定
    /// </summary>
    /// <param name="target"></param>
    private void JedgeKnockBackItem(Transform target) {
        
        // GetChild メソッドと for 文を組み合わせて Radar を見つける
        // 子オブジェクトの数を固定化しないので、敵でもプレイヤーでも問題なく動く
        for (int i = 0; i < target.childCount; i++) {
            // if (target.GetChild(i).TryGetComponent(out Radar item) == true) {
            //     
            //     // 肉をドロップ
            //     item.Drop();
            //     Debug.Log("肉落とし成功！");
            //
            //     // 肉をノックバック
            //     KnockbackDrumStick(item);
            //     break;
            // }
        }
    }

    /// <summary>
    /// アイテムのノックバック
    /// </summary>
    /// <param name="item"></param>
    private void KnockbackDrumStick(GameObject item)
    {
        // 肉をランダムに吹き飛ばす
        if (item.gameObject.TryGetComponent(out Rigidbody rb)) {
            rb.AddForce(new Vector3(Random.Range(-range, range), height, Random.Range(-range, range)) * forcePower);
        }
    }
    
    /// <summary>
    /// 対象をノックバック(KnockbackEnemy メソッドの代わり)
    /// 敵やプレイヤー、という風に対象を固定化せず、引数でもらった情報をノックバックの対象とするので可変化できる
    /// </summary>
    /// <param name="target"></param>
    private void KnockbackTarget(Transform target)
    {
        // 対象を吹き飛ばす方向(移動してきた反対方向)を設定
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // 高さを調整
        direction = new(direction.x, 0, direction.z);
        
        // 対象を吹き飛ばす
        target.position += direction * knockbackPower;
    }
 }