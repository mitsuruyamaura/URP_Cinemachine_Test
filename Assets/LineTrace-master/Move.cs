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
            Debug.Log("Rigidbody �擾�o���܂���ł���");
        }

        if (!TryGetComponent(out controller)) {
            Debug.Log("DirectionController2d �擾�o���܂���ł���");
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