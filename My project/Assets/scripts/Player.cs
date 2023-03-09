using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 6.0f; // �̵� �ӵ�
    public float jumpSpeed = 8.0f; // ���� �ӵ�
    public float gravity = 20.0f; // �߷� ���ӵ�

    public int PDamge = 10;

    private Vector3 moveDirection = Vector3.zero; // �̵� ����

    private CharacterController controller; // ĳ���� ��Ʈ�ѷ� ������Ʈ

    public int Health;

    void Start()
    {
        controller = GetComponent<CharacterController>(); // ĳ���� ��Ʈ�ѷ� ������Ʈ �Ҵ�
    }

    void Update()
    {
        if (controller.isGrounded) // ĳ���Ͱ� ���� ��� �ִ� ���
        {
            // �̵� ���� ����
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;

            // ����
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // �߷� ����
        moveDirection.y -= gravity * Time.deltaTime;

        // ĳ���� �̵�
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void TakeDamage()
    {

    }
}
