using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    int bulletPower;

    public void Shoot(Vector3 direction, float speed, int power) {
        bulletPower = power;
        if(TryGetComponent(out Rigidbody rb)) {
            rb.AddForce(direction * speed);
        }
    }
}
