using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public int BulletPower { get; set; }

    /// <summary>
    /// ’e‚ÌˆÚ“®
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    /// <param name="power"></param>
    public void Shoot(Vector3 direction, float speed, int power) {
        BulletPower = power;
        if(TryGetComponent(out Rigidbody rb)) {
            rb.AddForce(direction * speed);
        }
        Destroy(gameObject, 3.0f);
    }
}
