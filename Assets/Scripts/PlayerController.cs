using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�l����B������� NavMesh ���g���Ă��邪�A�W�����v���������̂Ŏg��Ȃ�
// https://www.youtube.com/watch?v=VqS1dTiVLFA&t=2s

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private Quaternion targetRotation;
    private float rotSpeed = 600;
    private Vector3 moveDirection;

    [SerializeField]
    private float moveSpeed = 3;


    void Start()
    {
        TryGetComponent(out anim);
        TryGetComponent(out rb);

        // ������
        targetRotation = transform.rotation;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // �J�����̌����ŕ␳�������̓x�N�g���̎擾
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        Quaternion horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        Vector3 velociry = horizontalRotation * new Vector3(horizontal, 0, vertical).normalized;

        // ���x�̎擾(�_�b�V�������邩�A�ʏ�ړ���)
        float speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        float rotationSpeed = rotSpeed * Time.deltaTime;

        // ���l�ȏ�̃L�[���͂�����ꍇ
        if (velociry.sqrMagnitude > 0.5f) {
            targetRotation = Quaternion.LookRotation(velociry, Vector3.up);
        }

        // BlendTree ���g���AAnimator �� RootMotion �ňړ�������ꍇ
        //anim.SetFloat("Speed", velociry.sqrMagnitude * speed, 0.1f, Time.deltaTime);

        // �ړ�����������
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

        // ���̌v�Z�� FixedUpdate �ōs���ƌv�Z������
        moveDirection = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * speed;
    }


    private void FixedUpdate() {
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    }
}
