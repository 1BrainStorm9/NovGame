using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameSession;

public class Movment : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Vector2 movement;
    public float moveSpeed;

    private Animator animator;
    private bool looksRight = true;

    private GameSession gameSession;
    private QuestManager questManager;

    void Start()        //� �������� ���� ����� ������ � ������ �������� � ������ � ����� 
    {
        rigidbody2d = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameSession = FindObjectOfType<GameSession>();
        questManager = FindObjectOfType<QuestManager>();
    }

    void Update()       //��������� ������ �������� ���������
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space)) { questManager.CompleteQuest("Move"); }
       
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
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));

    }

    



}
