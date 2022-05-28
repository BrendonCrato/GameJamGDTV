using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    CHASE,
    ATTACK
}
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Transform playerTarget;
    public float move_Speed = 3.5f;
    public float attack_Distance = 1f;
    public float chase_Player_After_Attack_Distance = 1f;
    private float wait_Before_Attack_Time = 3f;
    private float attack_Timer;

    private EnemyState enemy_State;

    // Start is called before the first frame update
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Start()
    {
        enemy_State = EnemyState.CHASE;

        attack_Timer = wait_Before_Attack_Time;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_State == EnemyState.CHASE)
        {
            ChasePlayer();
        }   

        if(enemy_State == EnemyState.ATTACK)
        {
            AttackPlayer();
        }
    }


    void ChasePlayer()
    {
        navAgent.SetDestination(playerTarget.position);
        navAgent.speed = move_Speed;

        if(navAgent.velocity.sqrMagnitude == 0)
        {
            //seta animação parado
        }
        else
        {
            //seta animação andando
        }
        Debug.Log(Vector3.Distance(transform.position, playerTarget.position));
        if(Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance)
        {
            enemy_State = EnemyState.ATTACK;
        } 
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        //seta animacao parado

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_Before_Attack_Time)
        {
            if(Random.Range(0, 2) > 0)
            {
                //seta anim ataque
            }
            else
            {
                //anim atack 2
            }

            attack_Timer = 0f;
        } 

        if(Vector3.Distance(transform.position, playerTarget.position) > attack_Distance + chase_Player_After_Attack_Distance)
        {
            navAgent.isStopped = false;
            enemy_State = EnemyState.CHASE;
        }
    }
}
