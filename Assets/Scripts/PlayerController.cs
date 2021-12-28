using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 参考動画。こちらは NavMesh を使っているが、ジャンプさせたいので使わない
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

    [SerializeField]
    private AreaCameraManager areaCameraManager;

    float horizontal;
    float vertical;

    void Start()
    {
        TryGetComponent(out anim);
        TryGetComponent(out rb);

        // 初期化
        targetRotation = transform.rotation;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // カメラの向きで補正した入力ベクトルの取得
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        //Quaternion horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);  // Camera.main では VirtualCamera が参照できないため
        //Vector3 velociry = horizontalRotation * new Vector3(horizontal, 0, vertical).normalized;

        //// 速度の取得(ダッシュさせるか、通常移動か)
        //float speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        //float rotationSpeed = rotSpeed * Time.deltaTime;

        //// 一定値以上のキー入力がある場合
        //if (velociry.sqrMagnitude > 0.5f) {
        //    targetRotation = Quaternion.LookRotation(velociry, Vector3.up);
        //}



        // BlendTree を使い、Animator の RootMotion で移動させる場合
        //anim.SetFloat("Speed", velociry.sqrMagnitude * speed, 0.1f, Time.deltaTime);



        //// 移動方向を向く
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

        //// この計算を FixedUpdate で行うと計算が狂う
        //moveDirection = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * speed;
    }


    private void FixedUpdate() {

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(areaCameraManager.CurrentCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * vertical + areaCameraManager.CurrentCamera.transform.right * horizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);            
        }


        //rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    }
}
