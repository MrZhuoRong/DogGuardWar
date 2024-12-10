using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates
{
    CHASE,
    ATTACK,
    DIE
}
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyState : MonoBehaviour
{
    private EnemyStates enemyStates;
    private NavMeshAgent agent;

    private CharacterStats EnemycharacterStats;//ÊýÖµ


    private Animator animator;
    private int StabAttackAniID;//¹¥»÷Id
    private int SmashAttackAnimID;

    public GameObject attackTarget;
    private float enemySpeed;

    private float attackTime = 0.5f;

    public bool IsRun { get; private set; }//ÊÇ·ñ×·Öð
    public bool IsDie { get; private set; }

    public GameObject enemyAttackRange;

    private void Awake()
    {
        EnemycharacterStats = GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackTarget = GameObject.Find("Player");
        enemySpeed = agent.speed;
        EnemycharacterStats.CurrentHealth = EnemycharacterStats.MaxHealth;


        StabAttackAniID = Animator.StringToHash("StabAttack");
        SmashAttackAnimID = Animator.StringToHash("SmashAttack");

        IsDie = false;
    }
    private void Update()
    {
        SwitchStates();
        animator.SetBool("IsRun", IsRun);
        attackTime -= Time.deltaTime;
        
    }


    private void OnTriggerEnter(Collider other)
    {

            if (other.tag == "PlayerAttack")
            {
                EnemycharacterStats.GetDamage(EnemycharacterStats);
                Debug.Log(EnemycharacterStats.CurrentHealth);
                if (EnemycharacterStats.CurrentHealth <= 0)
                {
                    enemyStates = EnemyStates.DIE;
                }
            }
    }

    private void SwitchStates()
    {
        switch (enemyStates)
        {
            case EnemyStates.CHASE://×·»÷ÈËÎï
                MovetoPlayer();
                break;
            case EnemyStates.ATTACK: //¹¥»÷
                EnemyAttack();
                break;
            case EnemyStates.DIE:
                EnemyDie();
                break;
        }
    }

    void EnemyDie()
    {
        agent.isStopped = true;
        IsRun = false;
        IsDie = true;
        animator.SetBool("IsDie", IsDie);
    }

    void MovetoPlayer()
    {
        agent.isStopped = false;
        if (Vector3.Distance(attackTarget.transform.position,this.transform.position)> EnemycharacterStats.AttackRange)
        {
            IsRun = true;  
            agent.destination=attackTarget.transform.position;
        }
        else
        {
            IsRun = false;
            
            enemyStates = EnemyStates.ATTACK;
        }
    }

    void EnemyAttack()
    {
        agent.isStopped = true;
 
        if (attackTime < 0)
        {
            
            int attacknum = Random.Range(0, 2);
            switch (attacknum)
            {
                case 0:
                    animator.SetTrigger(StabAttackAniID);//ÆÕÍ¨¹¥»÷
                    break;
                case 1:
                    animator.SetTrigger(SmashAttackAnimID);//±©»÷
                    break;
            }
            attackTime = 0.5f;
        }

    }

    public void EnterAttack()
    {
        enemyAttackRange.SetActive(true);
    }

    public void OverAttack()
    {
        enemyAttackRange.SetActive(false);
        enemyStates = EnemyStates.CHASE;
    }

    public void OverDie()
    {
        AttackPlayer.KillNumber += 1;
        Destroy(gameObject);
    }
}
