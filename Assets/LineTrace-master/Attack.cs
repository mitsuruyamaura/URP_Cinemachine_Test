using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class Attack : MonoBehaviour
{
    private Animator anim;

    // 以下はクラスにして外部からもらうようにする
    [SerializeField]
    private GameObject attackEffect;

    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private int bulletPower;


    void Start()
    {
        TryGetComponent(out anim);
        attackEffect.SetActive(false);

        this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("Fire1"))
            .ThrottleFirst(System.TimeSpan.FromSeconds(1.5f))
            .Subscribe(_ => PrepareAttack())
            .AddTo(gameObject);
    }

    /// <summary>
    /// 攻撃準備
    /// </summary>
    private void PrepareAttack() {
        anim.SetTrigger("Attack");
    }

    /// <summary>
    /// Attack 時のアニメーションイベントのコールバック１
    /// </summary>
    private void Hit() {
        attackEffect.SetActive(true);

        // パーティクルの ScalingMode を Local に変更すると Scale でのサイズ変更が可能になる
        attackEffect.transform.DOShakeScale(0.15f).SetEase(Ease.InBack);

        // 弾の生成
        Instantiate(bulletPrefab, attackEffect.transform.position, Quaternion.identity).Shoot(transform.forward, bulletSpeed, bulletPower);
    }

    /// <summary>
    /// Attack 時のアニメーションイベントのコールバック２
    /// </summary>
    private void HitEnd() {
        attackEffect.SetActive(false);
    }
}
