using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitParts{
    Armor,
    Back,
}

public class EnemyHitPart : MonoBehaviour
{
    private CapsuleCollider capsuleCol;

    public HitParts hitParts;


    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");
        if (hitParts == HitParts.Armor) {
            Destroy(other.gameObject);
            return;
        }

        if (other.TryGetComponent(out Bullet bullet)) {
            if(transform.parent.TryGetComponent(out EnemyBase enemyBase)){
                enemyBase.CalcDamage(bullet.BulletPower);
            }
        }
    }
}
