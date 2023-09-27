using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Vector2 movement;
    public float moveSpeed;

    private Animator animator;
    private bool looksRight = true;

    void Start()        //� �������� ���� ����� ������ � ������ �������� � ������ � ����� 
    {
        rigidbody2d = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()       //��������� ������ �������� ���������
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()      //������ � ������������ �������� � ����������� �� �����������
	{
        rigidbody2d.MovePosition(rigidbody2d.position + movement * moveSpeed * Time.fixedDeltaTime);

        if((movement.x > 0) && !looksRight)
		{
            Flip();
		} else if((movement.x < 0) && looksRight)
		{
            Flip();
		}

        if (movement.x != 0 || movement.y != 0)
        {
            animator.Play("HeroRun");
        }
        else animator.Play("HeroIdle");
	}

    private void Flip()         //������ �������� �������� �������� ���� � ������ �������
	{
        looksRight = !looksRight;
        transform.Rotate(0f, 180f, 0f);
	}
}
