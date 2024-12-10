using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackPlayer : MonoBehaviour
{
    private GameInput input;

    public GameObject attackRange;

    //public GameObject attackEffect1;
    //public GameObject attackEffect2;//¹¥»÷ÌØÐ§

    private Animator animator;
    private int Attack01AniID;
    private int Attack02AniID;

    private float playerAttackTime = 0.1f;

    static public int KillNumber;

    public ParticleSystem attackPlay;
    public float attackTime;
    // Start is called before the first frame update
    void Start()
    {
        KillNumber = 0;
        attackTime = 0.0f;
        animator = GetComponent<Animator>();
        Attack01AniID = Animator.StringToHash("Attack01");
        Attack02AniID = Animator.StringToHash("Attack02");

    }

    // Update is called once per frame
    void Update()
    {
        if (attackTime > 0)
        {
            attackTime-=Time.deltaTime;
        }
        else
        {
            attackPlay.Stop();
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void Attacking(bool bFiring)
    {
        int attacknum = -1;
        playerAttackTime -= Time.deltaTime;
        if (playerAttackTime < 0)
        {
            attacknum=Random.Range(0, 2);
        }
        switch (attacknum)
        {
            case -1:
                break;
            case 0:
                animator.SetTrigger(Attack01AniID);
                playerAttackTime = 0.1f;
                break;
            case 1:
                animator.SetTrigger(Attack02AniID);
                playerAttackTime = 0.1f;
                break;
        }
 
    }

    //Animation Event
    public void SetAttacktrue()
    {
        attackRange.SetActive(true);
        
    }
    public void SetAttackfalse()
    {
        attackRange.SetActive(false);
    }
    public void SetAttackEffect()
    {
        attackPlay.Play();
        attackTime = 0.5f;
    }


    public void EnterGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    
}
