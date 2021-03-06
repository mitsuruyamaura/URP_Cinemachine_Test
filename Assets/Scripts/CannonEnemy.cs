using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonEnemy : EnemyBase
{
    [Header("移動させる場合には true")]
    public bool isMove;

    [Header("斜方投射させる場合には true。false の場合には前方直線")]
    public bool isProjectileMotion;

    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private Transform bulletGenerateTran;

    [SerializeField]
    private float shootInterval;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private int bulletPower;


    protected override void Start() {
        base.Start();

        StartCoroutine(PrepareShoot());
    }

    protected override void MoveEnemy() {
        if (isMove) {
            base.MoveEnemy();
        }
    }

    /// <summary>
    /// 弾の生成準備
    /// </summary>
    /// <returns></returns>
    private IEnumerator PrepareShoot() {

        float timer = 0;
        while (true) {
            timer += Time.deltaTime;
            if (timer >= shootInterval) {
                timer = 0;
                Shoot();
            }
            yield return null;
        }
    }

    /// <summary>
    /// 弾の生成と発射
    /// </summary>
    private void Shoot() {
        Bullet bullet = Instantiate(bulletPrefab, bulletGenerateTran);

        // 斜方投射の場合
        if (isProjectileMotion) {
            bullet.Shoot(new Vector3(1, 1, 0), bulletSpeed, bulletPower);
        } else {
            // 直線の場合
            bullet.Shoot(transform.forward, bulletSpeed, bulletPower);
        }
    }
}
