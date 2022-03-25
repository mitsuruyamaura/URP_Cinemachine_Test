using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonEnemy : EnemyBase
{
    [Header("ˆÚ“®‚³‚¹‚éê‡‚É‚Í true")]
    public bool isMove;

    [Header("Î•û“ŠË‚³‚¹‚éê‡‚É‚Í trueBfalse ‚Ìê‡‚É‚Í‘O•û’¼ü")]
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
    /// ’e‚Ì¶¬€”õ
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
    /// ’e‚Ì¶¬‚Æ”­Ë
    /// </summary>
    private void Shoot() {
        Bullet bullet = Instantiate(bulletPrefab, bulletGenerateTran);

        // Î•û“ŠË‚Ìê‡
        if (isProjectileMotion) {
            bullet.Shoot(new Vector3(1, 1, 0), bulletSpeed, bulletPower);
        } else {
            // ’¼ü‚Ìê‡
            bullet.Shoot(transform.forward, bulletSpeed, bulletPower);
        }
    }
}
