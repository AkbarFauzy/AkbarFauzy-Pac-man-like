using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    void BaseState.EnterState(Enemy enemy)
    {
        Debug.Log("Start Chasing");
        enemy.Animator.SetTrigger("ChaseState");
    }

    void BaseState.UpdateState(Enemy enemy)
    {
        //Debug.Log("Chasing");
        if (enemy.Player != null)
        {
            enemy.NavMeshAgent.destination = enemy.Player.transform.position;
            if (Vector3.Distance(enemy.transform.position,  enemy.Player.transform.position) > enemy.ChaseDistance) {
                enemy.SwitchState(enemy.PatrolState);
            }
        }

    }

    void BaseState.ExitState(Enemy enemy)
    {
        Debug.Log("Stop Chasing");
    }
}
