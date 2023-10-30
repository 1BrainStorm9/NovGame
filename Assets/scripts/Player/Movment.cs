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

    void Start()        //с запуском игры берет данные о работе анимаций и игрока в целом 
    {
        rigidbody2d = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()       //позволяет задать скорость персонажу
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()      //работа с подключением анимаций и правильного их отображения
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
            gameSession.CompleteQuest("Move"); // complete quest
        }
        else animator.Play("HeroIdle");
	}

    private void Flip()         //просто помогает изменить анимацию бега в нужную сторону
	{
        looksRight = !looksRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));

    }

    



}
