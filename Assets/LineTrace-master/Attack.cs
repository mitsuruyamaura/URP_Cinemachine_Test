using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using UnityEngine.InputSystem;


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

    [SerializeField]
    private bool useInputSystem;


    void Start()
    {
        TryGetComponent(out anim);
        attackEffect.SetActive(false);

        if (!useInputSystem) {
            this.UpdateAsObservable()
                .Where(_ => Input.GetButtonDown("Fire1"))
                .Where(_ => !anim.IsInTransition(0))  // アニメーションの遷移中ではない
                .ThrottleFirst(System.TimeSpan.FromSeconds(1.5f))
                .Subscribe(_ => PrepareAttack())
                .AddTo(gameObject);
        }          
    }

    /// <summary>
    /// 攻撃準備
    /// </summary>
    private void PrepareAttack() {
        Debug.Log("Attack");
        anim.SetTrigger("Attack");
        SoundManager.instance.PlaySE(SeType.AttackEffect);
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

    /// <summary>
    /// InputSystem 使用時の攻撃処理
    /// </summary>
    /// <param name="context"></param>
    public void OnFire(InputAction.CallbackContext context) {
        if (useInputSystem) {
            // 連続ででるので、UniRx で制御する方法で考える
            //if (context.phase == InputActionPhase.Performed) {  // Perfromed は有効である場合を差す。入力有無での判断ではない
            PrepareAttack();
            //}
        }
    }
}
