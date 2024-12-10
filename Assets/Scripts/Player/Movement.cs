using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(CharacterController))]

public class Movement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 0.15f;
    [SerializeField] private float speedOffset = 0.05f;
    [SerializeField] private float speedChangeRate = 10f;

    private float Gravity = -10f;
    
    [SerializeField] private float GroundedOffset = 0.1f;
    [SerializeField] private float GroundedRadius = 0.2f;
    [SerializeField] private LayerMask GroundLayers;

    public bool IsGrounded { get; private set; }//判断是否在地面

    private float verticalSpeed = 0f;

    public Vector3 MoveDirection { get; protected set; } 

    public float ForwardSpeed { get; protected set; }
    public float RightSpeed { get; protected set; }

    public float TurnValue { get => turnValue; set => turnValue = value * turnSpeed; }
    public float ForwardValue { get; set; } = 0f;
    public float RightValue { get; set; } = 0f;

    private float turnValue = 0f;

    private Animator animator;
    

    private int forwardMoveAniID;
    private int rightMoveAniID;//横向移动
    private int groundedAniID;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        SetupAnimationIDs();
        MoveDirection = transform.forward+transform.right;

    }

    
    // Update is called once per frame
    void Update()
    {

        GroundedCheck();
        CalVerticalSpeed();
        MoveForward();
        MoveRight();
    }
  

    void SetupAnimationIDs()
    {
        forwardMoveAniID = Animator.StringToHash("forwardSpeed");
        rightMoveAniID = Animator.StringToHash("rightSpeed");
        groundedAniID = Animator.StringToHash("Grounded");
    }


    void MoveForward()
    {

        float targetSpeed = ForwardValue * moveSpeed;    
        if (IsGrounded)
        {
            if (ForwardSpeed < targetSpeed - speedOffset || ForwardSpeed > targetSpeed + speedOffset)
            {
                ForwardSpeed = Mathf.Lerp(ForwardSpeed, targetSpeed , speedChangeRate * Time.deltaTime);
                ForwardSpeed = Mathf.Round(ForwardSpeed * 1000f) / 1000f;
            }
            else
            {
                ForwardSpeed = targetSpeed;
            }
        }

        controller.Move(ForwardSpeed * Vector3.forward * Time.deltaTime);
        animator.SetFloat(forwardMoveAniID, ForwardSpeed);
    }
    void MoveRight()
    {
        
        float targetSpeed = RightValue * moveSpeed;
        if (IsGrounded)
        {
            if (RightSpeed < targetSpeed - speedOffset || RightSpeed > targetSpeed + speedOffset)
            {
                RightSpeed = Mathf.Lerp(RightSpeed, targetSpeed, speedChangeRate * Time.deltaTime);
                RightSpeed = Mathf.Round(RightSpeed * 1000f) / 1000f;
            }
            else
            {
                RightSpeed = targetSpeed;
            }
        }

        controller.Move(RightSpeed * Vector3.right * Time.deltaTime);
        animator.SetFloat(rightMoveAniID, RightSpeed);
    }

    
    /// <summary>
    /// GroundedCheck 通过脚底下的球形碰撞是否与地面发生碰撞，返回值表示是否站在地面上
    /// </summary>
    void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        ;
        if(Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore))
        {
            IsGrounded = true;
        }
        animator?.SetBool(groundedAniID, IsGrounded);
    }

   

    void CalVerticalSpeed()
    {
        if (IsGrounded)
        {    
            //如果在地面，阻止无限下落
            if (verticalSpeed < 0f)
            {
                verticalSpeed = -2f;                
            }
        }
        else//如果不在地面
        {

            verticalSpeed += Gravity * Time.deltaTime;
        } 
    }

    
}
