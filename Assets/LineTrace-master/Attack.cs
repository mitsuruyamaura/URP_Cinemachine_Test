using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class Attack : MonoBehaviour
{
    private Animator anim;

    // �ȉ��̓N���X�ɂ��ĊO��������炤�悤�ɂ���
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
    /// �U������
    /// </summary>
    private void PrepareAttack() {
        anim.SetTrigger("Attack");
    }

    /// <summary>
    /// Attack ���̃A�j���[�V�����C�x���g�̃R�[���o�b�N�P
    /// </summary>
    private void Hit() {
        attackEffect.SetActive(true);

        // �p�[�e�B�N���� ScalingMode �� Local �ɕύX����� Scale �ł̃T�C�Y�ύX���\�ɂȂ�
        attackEffect.transform.DOShakeScale(0.15f).SetEase(Ease.InBack);

        // �e�̐���
        Instantiate(bulletPrefab, attackEffect.transform.position, Quaternion.identity).Shoot(transform.forward, bulletSpeed, bulletPower);
    }

    /// <summary>
    /// Attack ���̃A�j���[�V�����C�x���g�̃R�[���o�b�N�Q
    /// </summary>
    private void HitEnd() {
        attackEffect.SetActive(false);
    }
}
