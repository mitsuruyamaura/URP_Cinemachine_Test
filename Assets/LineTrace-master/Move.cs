using UnityEngine;
using LineTrace;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private CameraEffect cameraEffect;

    //[SerializeField]
    //private bool isGrounded;

    private DirectionController2d controller;
    private Rigidbody rb;
    private Animator anim;

    private string horizontal = "Horizontal";
    private float inputValue;
    private Vector2 lookDirection = new Vector2(1, 0);

    [SerializeField]
    private bool isDash;

    private bool isVignette;

    [SerializeField]
    private bool useInputSystem;


    public void OnMove(InputAction.CallbackContext context) {
        if (useInputSystem) {
            inputValue = context.ReadValue<Vector2>().x;
        }
    }


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

        if (!useInputSystem) {
            // ���E�̃L�[���͂̎擾
            inputValue = Input.GetAxis(horizontal);
        }

        // �L�[���͂̕����ɍ��킹�Č�����ݒ�
        if (inputValue > 0) {
            controller.direction = Direction.front;
        } else if (inputValue < 0){
            controller.direction = Direction.back;
        }

        // �ړ��A�j���̓���
        SyncMoveAnimation();

        // �W�����v
        if (Input.GetButtonDown("Jump") && CheckGround()) {
            Jump();
        }

        isDash = Input.GetKey(KeyCode.LeftShift) ? true : false;

        if (isDash) {
            if (!isVignette) {
                isVignette = true;
                cameraEffect.ChangeVignetteIntensity(0.45f, 0.25f);
            }
        } else {
            if (isVignette) {
                isVignette = false;
                cameraEffect.ChangeVignetteIntensity(0, 0.25f);
            }
        }

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

            anim.SetFloat("LookX", lookDirection.x);
            anim.SetFloat("LookZ", lookDirection.y);

            float animSpeed = isDash ? lookDirection.sqrMagnitude * 2.0f : lookDirection.sqrMagnitude;

            // �_�b�V���L���ɉ����ăA�j���̍Đ����x�𒲐�
            anim.SetFloat("Speed", animSpeed);
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

        float moveSpeed = isDash ? speed * 2.0f : speed;

        // �ړ�
        rb.velocity = new Vector3(controller.forward.x * moveSpeed, rb.velocity.y, controller.forward.z * moveSpeed);

        // �L�[���͂��Ȃ��ꍇ
        if (inputValue == 0) {
            // ��~
            rb.velocity = Vector3.zero;
        }
    }


    private bool CheckGround() {
        return Physics.Linecast(
            transform.position + transform.up * 1.0f,
            transform.position - transform.up * 0.3f,
            groundLayer
            );
    }

    // �W�����v�̃A�j���؂�ւ��L��
    // https://issekinichou.wordpress.com/2020/01/27/unity-%E3%82%B8%E3%83%A3%E3%83%B3%E3%83%97/
    private void Jump() {
        anim.SetTrigger("JumpTrigger");
        rb.AddForce(rb.velocity.x, jumpPower, rb.velocity.z);
    }
}