using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    //
    private Rigidbody2D rb;
    public float jumpForce = 10.0f;
    public bool isGround;
    public float rayDistance = 2f;
    private GameObject player;
    public float speed = 5f;

    private bool isMoving = false;
    private SpriteRenderer spriteRenderer; // ссылка на компонент SpriteRenderer

    public bool WithSword = false;

    public Transform attac1;
    public float attac1Radius;
    public Transform attac2;
    public float attac2Radius;

    private bool isAttacking = false;


    private void Start()
    {
        // Получение компонента Animator из текущего объекта или другого объекта
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        Sword();
        JumpController();
        Muve();
        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {
            Attack();
        }
        if (player == null)
        {
            EndGame();
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Получаем компонент Animator из объекта
        animator = GetComponent<Animator>();
    }

    void Sword()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            WithSword = !WithSword;
            Debug.Log("Sword equipped: " + WithSword);

            // Добавьте здесь ваш код для активации или деактивации меча
            if (WithSword)
            {

                animator.SetBool("WithSword", true);
            }
            else
            {

                animator.SetBool("WithSword", false);
            }
        }
    }

    void JumpController()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            isGround = true;

        }
        else
        {
            isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            // Добавляем условие для изменения значения параметра isJumping в аниматоре
            if (animator != null)
            {
                animator.SetBool("isJumping", true);
            }
        }

        // Получаем значение параметра isJumping из аниматора
        bool isJumping = animator.GetBool("isJumping");

        // Добавляем условие для изменения значения параметра isJumping в аниматоре
        if (!isJumping && !isGround)
        {
            animator.SetBool("isJumping", true);
        }
        else if (isJumping && isGround)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void Muve()
    {
        // Получаем входные данные по горизонтальной оси (A и D или стрелки влево и вправо)
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Вычисляем новую позицию игрока
        Vector3 position = transform.position;
        position.x += horizontalInput * speed * Time.deltaTime;

        // Проверяем, двигается ли игрок
        if (horizontalInput != 0)
        {
            isMoving = true;

            // Определяем направление движения и разворачиваем спрайт
            if (horizontalInput > 0)
            {
                spriteRenderer.flipX = false; // не разворачиваем спрайт
            }
            else if (horizontalInput < 0)
            {
                spriteRenderer.flipX = true; // разворачиваем спрайт
            }
        }
        else
        {
            isMoving = false;
        }

        // Присваиваем новую позицию игроку
        transform.position = position;

        // Устанавливаем значение параметра "IsMoving" в аниматоре
        animator.SetBool("IsMoving", isMoving);
    }


    private void Attack()
    {
        if (WithSword)
        {
            isAttacking = true;

            // Включаем анимацию для атаки
            animator.SetBool("Attack1", true);

            // Здесь вы можете добавить логику обработки удара или взаимодействия с другими объектами
            Fight2D.Action(attac1.position, attac1Radius, 6, 25, false);

            // Восстановление состояния атаки после завершения анимации
            Invoke(nameof(ResetAttack), 0.2f);
        }
        else
        {
            isAttacking = true;

            // Включаем анимацию для атаки
            animator.SetBool("Attack2", true);

            // Здесь вы можете добавить логику обработки удара или взаимодействия с другими объектами
            Fight2D.Action(attac2.position, attac2Radius, 6, 20, true);

            // Восстановление состояния атаки после завершения анимации
            Invoke(nameof(ResetAttack), 0.2f);
        }


    }

    private void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
    }

    private void EndGame()
    {
        // Здесь можно выполнить действия для завершения игры
        // Например, остановить время, отключить управление игроком, показать экран завершения и т.д.

        // Просто пример для завершения игры:
        Debug.Log("Игра завершена!");
        Application.Quit(); // Завершение приложения (работает только для сборки на платформе, не в редакторе Unity)
    }

}
