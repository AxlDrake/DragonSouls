using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    EnemyLocomotion enemyLocomotion;
    EnemyAnimatorManager enemyAnimatorManager;
    EnemyStats enemyStats;
    public NavMeshAgent navMeshAgent;

    public State currentState;
    public CharacterStats currentTarget;
    public Rigidbody enemyRigidBody;

    public bool isPerformingAction;

    public float distanceFromTarget;   
    public float rotationSpeed = 15;
    public float maximumAttackRange = 1.5f;

    [Header("A.I. Settings")]
    public float detectionRadius = 20;
    //Detection Field of View
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public float viewableAngle;

    public float currentRecoveryTime = 0;   

    private void Awake()
    {
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStats>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidBody = GetComponent<Rigidbody>();        
    }

    private void Start()
    {
        enemyRigidBody.isKinematic = false;
        navMeshAgent.enabled = false;
    }

    private void Update()
    {
        HandleRecoveryTime();
    }


    void FixedUpdate()
    {
        HandleStateMachine();
                
    }

    private void HandleStateMachine()
    {
        if(currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

            if(nextState != null)
            {
                SwitchNextState(nextState);
            }

        }


        //if(enemyLocomotion.currentTarget != null )
        //{
        //    enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyLocomotion.currentTarget.transform.position, transform.position);
        //}        

        //if (enemyLocomotion.currentTarget == null)
        //{
        //    enemyLocomotion.HandleDetection();
        //}
        //else if(enemyLocomotion.distanceFromTarget > enemyLocomotion.stoppingDistance)
        //{
        //    enemyLocomotion.HandleMoveToTarget();
        //}
        //else if(enemyLocomotion.distanceFromTarget <= enemyLocomotion.stoppingDistance)
        //{
        //    AttackTarget();
        //}
    }
    

    private void SwitchNextState(State state)
    {
        currentState = state;
    }


    private void HandleRecoveryTime()
    {
        if(currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if(isPerformingAction)
        {
            if(currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }
    
}
