using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    int bulletPower;

    /// <summary>
    /// ’e‚ÌˆÚ“®
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    /// <param name="power"></param>
    public void Shoot(Vector3 direction, float speed, int power) {
        bulletPower = power;
        if(TryGetComponent(out Rigidbody rb)) {
            rb.AddForce(direction * speed);
        }
    }
}
