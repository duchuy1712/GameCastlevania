using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_WalkingState : StateMachineBehaviour
{
    public float speed;
    private Zombie_Update zombie;
    private int Dir;
    private int Flip;
    protected Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        zombie = animator.gameObject.GetComponent<Zombie_Update>();
        zombie.TurnOnHitBox();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 Direction = animator.transform.position - zombie.Target.position;
        if (Direction.x > 1)
        {
            Dir = -1;
        }
        else if (Direction.x < -1)
        {
            Dir = 1;
        }
        rb.velocity = new Vector2(speed * Dir * Time.deltaTime, rb.velocity.y);
        if (Flip.Equals(Dir))
            return;
        if (Dir < 0)
        {
            animator.gameObject.transform.eulerAngles = Vector3.up * 180f;
        }
        else if (Dir > 0)
        {
            animator.gameObject.transform.eulerAngles = Vector3.zero;
        }
        Flip = Dir;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
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
