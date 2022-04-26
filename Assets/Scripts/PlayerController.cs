using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.InputSystem;

// �Q�l����B������� NavMesh ���g���Ă��邪�A�W�����v���������̂Ŏg��Ȃ�
// https://www.youtube.com/watch?v=VqS1dTiVLFA&t=2s


// �V�l�}�V���̊p�x�ɂ���ăL�[���͂ɂ��ړ��������Y���邽�߁A��L�̕��@���炱����ɐ؂�ւ���
// https://tech.pjin.jp/blog/2016/11/04/unity_skill_5/


public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private Quaternion targetRotation;
    private float rotSpeed = 600;
    private Vector3 moveDirection;

    [SerializeField]
    private float moveSpeed = 3;

    [SerializeField]
    private AreaCameraManager areaCameraManager;

    private float horizontal;
    private float vertical;
    private Vector2 lookDirection = new Vector2(1, 0);

    [SerializeField]
    private bool useInputSystem;

    /// <summary>
    /// InputSystem �̈ړ�
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context) {
        if (useInputSystem) {
            horizontal = context.ReadValue<Vector2>().x;
        }
    }


    void Start()
    {
        TryGetComponent(out anim);
        TryGetComponent(out rb);

        // ������
        targetRotation = transform.rotation;

        // �L�[����
        this.UpdateAsObservable()
            .Subscribe(_ => 
            {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");

                if (anim) {
                    SyncMoveAnimation();
                }
            });

        // �ړ�
        this.FixedUpdateAsObservable()
            .Subscribe(_ => { MoveDirectionFromCamera(); });

    }

    /// <summary>
    /// ���݂̃J�����̕�������ɂ��Ĉړ�
    /// </summary>
    private void MoveDirectionFromCamera() {

        // ���ݗ��p���Ă���J�����̕�������AX-Z ���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(areaCameraManager.CurrentCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƁA���ݗ��p���Ă���J�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * vertical + areaCameraManager.CurrentCamera.transform.right * horizontal;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    /// <summary>
    /// �ړ��A�j���̓���
    /// </summary>
    private void SyncMoveAnimation() {
        if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f)) {
            lookDirection.Set(horizontal, vertical);
            lookDirection.Normalize();

            //anim.SetFloat("Direction", lookDirection.x);
            anim.SetFloat("LookX", lookDirection.x);
            //anim.SetFloat("LookZ", lookDirection.y);

            anim.SetFloat("Speed", lookDirection.sqrMagnitude);
        } else {
            anim.SetFloat("Speed", 0);
        }
    }

    //void Update()
    //{
        // �J�����̌����ŕ␳�������̓x�N�g���̎擾
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");


        //Quaternion horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);  // Camera.main �ł� VirtualCamera ���Q�Ƃł��Ȃ�����
        //Vector3 velociry = horizontalRotation * new Vector3(horizontal, 0, vertical).normalized;

        //// ���x�̎擾(�_�b�V�������邩�A�ʏ�ړ���)
        //float speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        //float rotationSpeed = rotSpeed * Time.deltaTime;

        //// ���l�ȏ�̃L�[���͂�����ꍇ
        //if (velociry.sqrMagnitude > 0.5f) {
        //    targetRotation = Quaternion.LookRotation(velociry, Vector3.up);
        //}



        // BlendTree ���g���AAnimator �� RootMotion �ňړ�������ꍇ
        //anim.SetFloat("Speed", velociry.sqrMagnitude * speed, 0.1f, Time.deltaTime);



        //// �ړ�����������
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

        //// ���̌v�Z�� FixedUpdate �ōs���ƌv�Z������
        //moveDirection = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * speed;
    //}


    //private void FixedUpdate() {

        //rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    //}
}
