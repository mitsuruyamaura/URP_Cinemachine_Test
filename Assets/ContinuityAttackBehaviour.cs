using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unityのアクションゲームで連続攻撃を実現する方法
// https://gametukurikata.com/program/continuityattack

// 【Unity】AnimationControllerのAnyStateを使用してる際、現在のStateへ何度も移動しないようにする
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

        // AnyState を利用している場合は、Can Transition To Self にチェックが入っていると常に最初に戻ってしまうのでチェックを外す
        // AnyState を利用していない場合には特に問題なし
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
