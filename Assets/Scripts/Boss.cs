using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    BossController boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BossController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // The boss will turn his face to look at Ruby
        boss.LookAtPLayer();
        animator.SetFloat("Look X", -1.0f);
        Vector2 target = new Vector2(player.position.x, player.position.y);

        // If the boss HP is greater than 50%
        if (boss.health >= 10)
        {
            animator.SetInteger("BossHealth", 20);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, 2.0f * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        // If the boss HP is less than 50%
        else
        {
            animator.SetInteger("BossHealth", 9);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, 3.0f * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }
}
