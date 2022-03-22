using UnityEngine;
using LineTrace;

[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour {

    [SerializeField]
    private float speed;

    private DirectionController2d controller;
    private Rigidbody rb;
    private Animator anim;

    private string horizontal = "Horizontal";
    private float inputValue;
    private Vector2 lookDirection = new Vector2(1, 0);
    private bool isDash;


    void Start() {
        if(!TryGetComponent(out rb)) {
            Debug.Log("Rigidbody �擾�o���܂���ł���");
        }

        if (!TryGetComponent(out controller)) {
            Debug.Log("DirectionController2d �擾�o���܂���ł���");
        }

        if (!TryGetComponent(out anim)) {
            Debug.Log("Animator �擾�o���܂���ł���");
        }
    }

    void Update() {

        // DirectionController2d ���擾�ł��Ă��Ȃ��ꍇ
        if (controller == null) {
            // null �G���[���������邽�߁A�������s��Ȃ�
            return;
        }

        // ���E�̃L�[���͂̎擾
        inputValue = Input.GetAxis(horizontal);

        // �L�[���͂̕����ɍ��킹�Č�����ݒ�
        if (inputValue > 0) {
            controller.direction = Direction.front;
        } else if (inputValue < 0){
            controller.direction = Direction.back;
        }

        SyncMoveAnimation();

        //if (Input.GetKey(KeyCode.LeftArrow)) {
        //    // ������ݒ肷��
        //    controller.direction = Direction.back;
        //    //transform.position += controller.forward * speed * Time.deltaTime;
        //} else if (Input.GetKey(KeyCode.RightArrow)) {
        //    // ������ݒ肷��
        //    controller.direction = Direction.front;
        //    //transform.position += controller.forward * speed * Time.deltaTime;
        //} 
    }

    /// <summary>
    /// �ړ���������ƈړ��A�j���̓���
    /// </summary>
    private void SyncMoveAnimation() {
        if (!Mathf.Approximately(inputValue, 0.0f)) {
            lookDirection.Set(inputValue, 0);
            lookDirection.Normalize();

            anim.SetFloat("Look X", lookDirection.x);
            anim.SetFloat("Look Y", lookDirection.y);

            float moveSpeed = isDash ? lookDirection.sqrMagnitude * 2.0f : lookDirection.sqrMagnitude;

            // �_�b�V���L���ɉ����ăA�j���̍Đ����x�𒲐�
            anim.SetFloat("Speed", moveSpeed);
        } else {
            anim.SetFloat("Speed", 0);
        }
    }

    private void FixedUpdate() {

        // Rigidbody ���擾�ł��Ă��Ȃ��ꍇ
        if (!rb) {
            // null �G���[���������邽�߁A�������s��Ȃ�
            return; 
        }

        // �ړ�
        rb.velocity = new Vector3(controller.forward.x * speed, rb.velocity.y, controller.forward.z * speed);

        // �L�[���͂��Ȃ��ꍇ
        if (inputValue == 0) {
            // ��~
            rb.velocity = Vector3.zero;
        }
    }
}