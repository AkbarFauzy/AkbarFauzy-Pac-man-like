using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    void BaseState.EnterState(Enemy enemy)
    {
        Debug.Log("Start Retreating");
    }

    void BaseState.UpdateState(Enemy enemy)
    {
        //Debug.Log("Retreating");
        if (enemy.Player != null) {
            enemy.NavMeshAgent.destination = enemy.transform.position - enemy.Player.transform.position;
        }


    }

    void BaseState.ExitState(Enemy enemy)
    {
        Debug.Log("Stop Retreating");
    }
}
