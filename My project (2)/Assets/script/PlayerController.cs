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
    private SpriteRenderer spriteRenderer; // ������ �� ��������� SpriteRenderer

    public bool WithSword = false;

    public Transform attac1;
    public float attac1Radius;
    public Transform attac2;
    public float attac2Radius;

    private bool isAttacking = false;


    private void Start()
    {
        // ��������� ���������� Animator �� �������� ������� ��� ������� �������
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

        // �������� ��������� Animator �� �������
        animator = GetComponent<Animator>();
    }

    void Sword()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            WithSword = !WithSword;
            Debug.Log("Sword equipped: " + WithSword);

            // �������� ����� ��� ��� ��� ��������� ��� ����������� ����
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

            // ��������� ������� ��� ��������� �������� ��������� isJumping � ���������
            if (animator != null)
            {
                animator.SetBool("isJumping", true);
            }
        }

        // �������� �������� ��������� isJumping �� ���������
        bool isJumping = animator.GetBool("isJumping");

        // ��������� ������� ��� ��������� �������� ��������� isJumping � ���������
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
        // �������� ������� ������ �� �������������� ��� (A � D ��� ������� ����� � ������)
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // ��������� ����� ������� ������
        Vector3 position = transform.position;
        position.x += horizontalInput * speed * Time.deltaTime;

        // ���������, ��������� �� �����
        if (horizontalInput != 0)
        {
            isMoving = true;

            // ���������� ����������� �������� � ������������� ������
            if (horizontalInput > 0)
            {
                spriteRenderer.flipX = false; // �� ������������� ������
            }
            else if (horizontalInput < 0)
            {
                spriteRenderer.flipX = true; // ������������� ������
            }
        }
        else
        {
            isMoving = false;
        }

        // ����������� ����� ������� ������
        transform.position = position;

        // ������������� �������� ��������� "IsMoving" � ���������
        animator.SetBool("IsMoving", isMoving);
    }


    private void Attack()
    {
        if (WithSword)
        {
            isAttacking = true;

            // �������� �������� ��� �����
            animator.SetBool("Attack1", true);

            // ����� �� ������ �������� ������ ��������� ����� ��� �������������� � ������� ���������
            Fight2D.Action(attac1.position, attac1Radius, 6, 25, false);

            // �������������� ��������� ����� ����� ���������� ��������
            Invoke(nameof(ResetAttack), 0.2f);
        }
        else
        {
            isAttacking = true;

            // �������� �������� ��� �����
            animator.SetBool("Attack2", true);

            // ����� �� ������ �������� ������ ��������� ����� ��� �������������� � ������� ���������
            Fight2D.Action(attac2.position, attac2Radius, 6, 20, true);

            // �������������� ��������� ����� ����� ���������� ��������
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
        // ����� ����� ��������� �������� ��� ���������� ����
        // ��������, ���������� �����, ��������� ���������� �������, �������� ����� ���������� � �.�.

        // ������ ������ ��� ���������� ����:
        Debug.Log("���� ���������!");
        Application.Quit(); // ���������� ���������� (�������� ������ ��� ������ �� ���������, �� � ��������� Unity)
    }

}
