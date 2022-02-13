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
            Debug.Log("Rigidbody Žæ“¾o—ˆ‚Ü‚¹‚ñ‚Å‚µ‚½");
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
        //    // Œü‚«‚ðÝ’è‚·‚é
        //    controller.direction = Direction.back;
        //    //transform.position += controller.forward * speed * Time.deltaTime;
        //} else if (Input.GetKey(KeyCode.RightArrow)) {
        //    // Œü‚«‚ðÝ’è‚·‚é
        //    controller.direction = Direction.front;
        //    //transform.position += controller.forward * speed * Time.deltaTime;
        //} 
    }

    private void FixedUpdate() {
        if (!rb) {
            return; 
        }
        // ˆÚ“®
        rb.velocity = new Vector3(controller.forward.x * speed, rb.velocity.y, controller.forward.z * speed);

        if (inputValue == 0) {
            rb.velocity = Vector3.zero;
        }
    }
}