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
            Debug.Log("Rigidbody 取得出来ませんでした");
        }

        if (!TryGetComponent(out controller)) {
            Debug.Log("DirectionController2d 取得出来ませんでした");
        }

        if (!TryGetComponent(out anim)) {
            Debug.Log("Animator 取得出来ませんでした");
        }
    }

    void Update() {

        // DirectionController2d が取得できていない場合
        if (controller == null) {
            // null エラーが発生するため、処理を行わない
            return;
        }

        if (!useInputSystem) {
            // 左右のキー入力の取得
            inputValue = Input.GetAxis(horizontal);
        }

        // キー入力の方向に合わせて向きを設定
        if (inputValue > 0) {
            controller.direction = Direction.front;
        } else if (inputValue < 0){
            controller.direction = Direction.back;
        }

        // 移動アニメの同期
        SyncMoveAnimation();

        // ジャンプ
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
        //    // 向きを設定する
        //    controller.direction = Direction.back;
        //    //transform.position += controller.forward * speed * Time.deltaTime;
        //} else if (Input.GetKey(KeyCode.RightArrow)) {
        //    // 向きを設定する
        //    controller.direction = Direction.front;
        //    //transform.position += controller.forward * speed * Time.deltaTime;
        //} 
    }

    /// <summary>
    /// 移動する方向と移動アニメの同期
    /// </summary>
    private void SyncMoveAnimation() {
        if (!Mathf.Approximately(inputValue, 0.0f)) {
            lookDirection.Set(inputValue, 0);
            lookDirection.Normalize();

            anim.SetFloat("LookX", lookDirection.x);
            anim.SetFloat("LookZ", lookDirection.y);

            float animSpeed = isDash ? lookDirection.sqrMagnitude * 2.0f : lookDirection.sqrMagnitude;

            // ダッシュ有無に応じてアニメの再生速度を調整
            anim.SetFloat("Speed", animSpeed);
        } else {
            anim.SetFloat("Speed", 0);
        }
    }

    private void FixedUpdate() {

        // Rigidbody が取得できていない場合
        if (!rb) {
            // null エラーが発生するため、処理を行わない
            return; 
        }

        float moveSpeed = isDash ? speed * 2.0f : speed;

        // 移動
        rb.velocity = new Vector3(controller.forward.x * moveSpeed, rb.velocity.y, controller.forward.z * moveSpeed);

        // キー入力がない場合
        if (inputValue == 0) {
            // 停止
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

    // ジャンプのアニメ切り替え記事
    // https://issekinichou.wordpress.com/2020/01/27/unity-%E3%82%B8%E3%83%A3%E3%83%B3%E3%83%97/
    private void Jump() {
        anim.SetTrigger("JumpTrigger");
        rb.AddForce(rb.velocity.x, jumpPower, rb.velocity.z);
    }
}