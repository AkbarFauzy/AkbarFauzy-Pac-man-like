using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private BaseState _currentState;
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();

    [SerializeField]
    public List<Transform> Waypoints = new List<Transform>();
    public float ChaseDistance;
    public Player Player;

    [HideInInspector]
    public NavMeshAgent NavMeshAgent;
    public Animator Animator;

    private void Awake()
    {
        _currentState = PatrolState;
        _currentState.EnterState(this);
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (Player != null) {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if (_currentState != null) {
            _currentState.UpdateState(this);
        }   
    }

    public void SwitchState(BaseState state) {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void StartRetreating() {
        SwitchState(RetreatState);
    }

    public void StopRetreating() {
        SwitchState(PatrolState);
    }

    public void Dead() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentState != RetreatState) {
            if (collision.gameObject.CompareTag("Player"))
            {
               collision.gameObject.GetComponent<Player>().Dead();
            }
        }
    }

}
