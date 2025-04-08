using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public Transform cam; // ← 카메라 연결 필요!
    public float mouseSensitivity = 3f;

    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;

    bool isJump;
    
    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;

    float yaw; // 마우스 회전값 누적용

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        yaw = cam.eulerAngles.y; // 초기 카메라 방향값 저장
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
    }

    void GetInput()
    {
        // 2. 이동 입력 처리
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }
    void Move()
    {
        // 4. 이동
        if (wDown)
            transform.position += moveVec * speed * Time.deltaTime;
        else
            transform.position += moveVec * speed * 0.4f * Time.deltaTime;

        // 5. 애니메이션
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }
    void Turn()
    {
        // 1. 마우스 입력으로 카메라 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        yaw += mouseX;
        cam.rotation = Quaternion.Euler(0, yaw, 0);


        // 3. 카메라 방향 기준으로 이동 방향 설정
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        moveVec = camForward * vAxis + camRight * hAxis;
        moveVec.Normalize();

        // 6. 바라보는 방향
        if (moveVec != Vector3.zero)
            transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }
}
