using UnityEngine;
using LineTrace;
public class Move : MonoBehaviour {

    public DirectionController2d controller;
    public float speed;

    private Rigidbody rb;
    private string horizontal = "Horizontal";
    float inputValue;

    private void Start() {
        if(!TryGetComponent(out rb)) {
            Debug.Log("Rigidbody 取得出来ませんでした");
        }
    }

    void Update() {

        inputValue = Input.GetAxis(horizontal);

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
        if (!rb) {
            return; 
        }
        // 移動
        rb.velocity = new Vector3(controller.forward.x * speed, rb.velocity.y, controller.forward.z * speed);

        if (inputValue == 0) {
            rb.velocity = Vector3.zero;
        }
    }
}