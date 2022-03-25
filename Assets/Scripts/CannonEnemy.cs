using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonEnemy : EnemyBase
{
    [Header("�ړ�������ꍇ�ɂ� true")]
    public bool isMove;

    [Header("�Ε����˂�����ꍇ�ɂ� true�Bfalse �̏ꍇ�ɂ͑O������")]
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
    /// �e�̐�������
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
    /// �e�̐����Ɣ���
    /// </summary>
    private void Shoot() {
        Bullet bullet = Instantiate(bulletPrefab, bulletGenerateTran);

        // �Ε����˂̏ꍇ
        if (isProjectileMotion) {
            bullet.Shoot(new Vector3(1, 1, 0), bulletSpeed, bulletPower);
        } else {
            // �����̏ꍇ
            bullet.Shoot(transform.forward, bulletSpeed, bulletPower);
        }
    }
}
