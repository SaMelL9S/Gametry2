using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public Transform player;
    public Transform attacE;
    public float attacERadius;
    public float attackDelay = 1f;
    public Animator animator;

    private int currentPatrolPointIndex = 0;
    private bool isAttacking = false;
    private float attackTimer = 0f;

    // Добавляем переменную SpriteRenderer и facingDirection, которая определяет направление движения
    private SpriteRenderer sr;
    private int facingDirection = 1;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Перемещение между точками патрулирования
        if (!isAttacking)
        {
            Vector3 targetPosition = patrolPoints[currentPatrolPointIndex].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Поворот спрайта в зависимости от направления движения
            if (transform.position.x > targetPosition.x && facingDirection == 1)
            {
                Flip();
            }
            else if (transform.position.x < targetPosition.x && facingDirection == -1)
            {
                Flip();
            }

            // Если достигнута текущая точка патрулирования, переключиться на следующую
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
            }
        }

        // Атака игрока в заданной области
        if (Vector2.Distance(transform.position, player.position) < attacERadius)
        {
            isAttacking = true;
            animator.SetBool("AttackE", true);
            // Отложенное выполнение атаки с задержкой attackDelay
            if (attackTimer >= attackDelay)
            {
                Fight2D.Action(attacE.position, attacERadius, 9, 20, true);
                attackTimer = 0f;
                
            }
            else
            {
                attackTimer += Time.deltaTime;
            }
        }
        else
        {
            isAttacking = false;
            attackTimer = 0f;
            animator.SetBool("AttackE", false);
        }
    }

    // Метод для поворота спрайта
    private void Flip()
    {
        facingDirection *= -1;
        sr.flipX = !sr.flipX;
    }
}
