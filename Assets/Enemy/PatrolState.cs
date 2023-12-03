using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;

    void BaseState.EnterState(Enemy enemy)
    {
        Debug.Log("Start Patrolling");
        _isMoving = false;
    }

    void BaseState.UpdateState(Enemy enemy)
    {
        //Debug.Log("Patrolling");

        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }

        if (!_isMoving) {
            _isMoving = true;
            int index = Random.Range(0, enemy.Waypoints.Count);
            _destination = enemy.Waypoints[index].position;
            enemy.NavMeshAgent.destination = _destination;
        }
        else if(Vector3.Distance(_destination, enemy.transform.position) <= 0.1f){
            _isMoving = false;
        }
    }

    void BaseState.ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrolling");
    }
}
