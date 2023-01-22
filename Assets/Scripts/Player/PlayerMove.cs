using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool runDown;
    [SerializeField] float groundYOffset;
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    [SerializeField] LayerMask groundMask;
    bool jumpDown;
    // bool isJump;
    bool dodgeDown;
    bool isDodge;

    Animator animator;
    CharacterController controller;
    Vector3 moveVec;
    Vector3 spherePos;
    Vector3 dodgeVec;
    Rigidbody rigid;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        move();
        Gravity();
        turn();
        // jump();
        // dodge();
    }

    void getInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        runDown = Input.GetButton("Run");
        jumpDown = Input.GetButtonDown("Jump");
        dodgeDown = Input.GetButtonDown("Dodge");
    }

    void move()
    {
        moveVec = transform.forward * vAxis + transform.right * hAxis;
        // moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;
        if (runDown)
            controller.Move(moveVec * speed * Time.deltaTime);
        else
            controller.Move(moveVec * speed * 0.6f * Time.deltaTime);
        // transform.position += moveVec * speed * 0.6f * Time.deltaTime;

        animator.SetBool("isWalk", moveVec != Vector3.zero);
        animator.SetBool("isRun", runDown);
    }

    bool isGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        else return false;
    }

    void Gravity()
    {
        if (!isGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = 2;
        controller.Move(velocity * Time.deltaTime);
    }


    // void jump()
    // {
    //     if (jumpDown && !isJump && !isDodge)
    //     {
    //         rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
    //         animator.SetBool("isJump", true);
    //         animator.SetTrigger("doJump");
    //         isJump = true;
    //     }

    // }

    // void dodge()
    // {
    //     if (dodgeDown && !isDodge && !isJump)
    //     {
    //         dodgeVec = moveVec;
    //         speed *= 2;
    //         animator.SetTrigger("doDodge");
    //         isDodge = true;

    //         Invoke("endDodge", 0.4f);
    //         //쿨타임 만들고 싶으면 endDodge를 두개로 쪼개서 invoke 시간 따로
    //     }

    // }

    // void endDodge()
    // {
    //     speed *= 0.5f;
    //     isDodge = false;
    // }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.tag == "Floor")
    //     {
    //         animator.SetBool("isJump", false);
    //         isJump = false;
    //     }
    // }

    void turn()
    {
        transform.LookAt(transform.position + moveVec);
    }
}