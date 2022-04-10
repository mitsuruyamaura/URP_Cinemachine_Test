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

    // �ȉ��̓N���X�ɂ��ĊO��������炤�悤�ɂ���
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
                .Where(_ => !anim.IsInTransition(0))  // �A�j���[�V�����̑J�ڒ��ł͂Ȃ�
                .ThrottleFirst(System.TimeSpan.FromSeconds(1.5f))
                .Subscribe(_ => PrepareAttack())
                .AddTo(gameObject);
        }          
    }

    /// <summary>
    /// �U������
    /// </summary>
    private void PrepareAttack() {
        Debug.Log("Attack");
        anim.SetTrigger("Attack");
        SoundManager.instance.PlaySE(SeType.AttackEffect);
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

    /// <summary>
    /// InputSystem �g�p���̍U������
    /// </summary>
    /// <param name="context"></param>
    public void OnFire(InputAction.CallbackContext context) {
        if (useInputSystem) {
            // �A���łł�̂ŁAUniRx �Ő��䂷����@�ōl����
            //if (context.phase == InputActionPhase.Performed) {  // Perfromed �͗L���ł���ꍇ�������B���͗L���ł̔��f�ł͂Ȃ�
            PrepareAttack();
            //}
        }
    }
}
