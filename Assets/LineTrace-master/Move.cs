using UnityEngine;
using LineTrace;

[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour {

    [SerializeField]
    private float speed;

    private DirectionController2d controller;
    private Rigidbody rb;

    private string horizontal = "Horizontal";
    float inputValue;

    void Start() {
        if(!TryGetComponent(out rb)) {
            Debug.Log("Rigidbody 取得出来ませんでした");
        }

        if (!TryGetComponent(out controller)) {
            Debug.Log("DirectionController2d 取得出来ませんでした");
        }
    }

    void Update() {

        // DirectionController2d が取得できていない場合
        if (controller == null) {
            // null エラーが発生するため、処理を行わない
            return;
        }

        // 左右のキー入力の取得
        inputValue = Input.GetAxis(horizontal);

        // キー入力の方向に合わせて向きを設定
        if (inputValue > 0) {
            controller.direction = Direction.front;
        } else if (inputValue < 0){
            controller.direction = Direction.back;
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

    private void FixedUpdate() {

        // Rigidbody が取得できていない場合
        if (!rb) {
            // null エラーが発生するため、処理を行わない
            return; 
        }

        // 移動
        rb.velocity = new Vector3(controller.forward.x * speed, rb.velocity.y, controller.forward.z * speed);

        // キー入力がない場合
        if (inputValue == 0) {
            // 停止
            rb.velocity = Vector3.zero;
        }
    }
}