using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 6.0f; // 이동 속도
    public float jumpSpeed = 8.0f; // 점프 속도
    public float gravity = 20.0f; // 중력 가속도

    public int PDamge = 10;

    private Vector3 moveDirection = Vector3.zero; // 이동 방향

    private CharacterController controller; // 캐릭터 컨트롤러 컴포넌트

    public int Health;

    void Start()
    {
        controller = GetComponent<CharacterController>(); // 캐릭터 컨트롤러 컴포넌트 할당
    }

    void Update()
    {
        if (controller.isGrounded) // 캐릭터가 땅에 닿아 있는 경우
        {
            // 이동 방향 설정
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;

            // 점프
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // 중력 적용
        moveDirection.y -= gravity * Time.deltaTime;

        // 캐릭터 이동
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void TakeDamage()
    {

    }
}
