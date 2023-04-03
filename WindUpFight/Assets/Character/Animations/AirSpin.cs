using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSpin : StateMachineBehaviour
{
    float kickForce = 5.0f;
    Rigidbody2D rigidBody;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //add to counter
        GameObject.Find("Player").GetComponent<PlayerController>().airSpinsDone++;
        //cancel velocity
        rigidBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0.0f, 0.0f);
        //add new jump force
        rigidBody.AddForce(new Vector2(0.0f, kickForce * 1.25f), ForceMode2D.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
