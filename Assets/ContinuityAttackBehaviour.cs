using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unity�̃A�N�V�����Q�[���ŘA���U��������������@
// https://gametukurikata.com/program/continuityattack

// �yUnity�zAnimationController��AnyState���g�p���Ă�ہA���݂�State�։��x���ړ����Ȃ��悤�ɂ���
// https://tsubakit1.hateblo.jp/entry/2017/01/13/233000

public class ContinuityAttackBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.ResetTrigger("Attack");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("OnStateUpdate" + layerIndex);

        // AnyState �𗘗p���Ă���ꍇ�́ACan Transition To Self �Ƀ`�F�b�N�������Ă���Ə�ɍŏ��ɖ߂��Ă��܂��̂Ń`�F�b�N���O��
        // AnyState �𗘗p���Ă��Ȃ��ꍇ�ɂ͓��ɖ��Ȃ�
        if (Input.GetButtonDown("Fire1")) {
            animator.SetTrigger("Attack");
            Debug.Log("ContinuousAttack : " + layerIndex);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("OnStateExit" + layerIndex);
        if (stateInfo.IsName("ContinuousAttack3")) {
            animator.ResetTrigger("Attack");
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
