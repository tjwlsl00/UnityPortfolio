using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    #region 싱글턴 및 기본 설정
    public static PlayerMovement Instance;
    public static string nextSceneSpawnPointID = null;

    void Awake()
    {
        // 플레이어 오브젝트 파괴 방지 
        // if (Instance == null)
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb.gravityScale = 3f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SetupGroundAndBounds();
    }


    #endregion

    #region 플레이어 이동 
    [Header("이동 설정")]
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject groundObject;

    private Vector2 moveInput;
    private bool isGrounded = false;
    private bool isMoving = false;
    private bool isJumping = false;
    private float minX, maxX;

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // 스프라이트 방향 전환
        if (horizontalInput != 0)
        {
            spriteRenderer.flipX = horizontalInput < 0;
        }

        moveInput = new Vector2(horizontalInput, 0).normalized;

        // 이동 상태 업데이트
        if (!isJumping && !isAttacking)
        {
            bool wasMoving = isMoving;
            isMoving = Mathf.Abs(moveInput.x) > 0.1f;

            if (isMoving != wasMoving)
            {
                animator.SetBool(moveAnimParam, isMoving);
            }
        }
    }

    private void ApplyMovement()
    {
        if (!isAttacking)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    private void SetupGroundAndBounds()
    {
        if (groundObject == null) return;

        Collider2D collider = groundObject.GetComponent<Collider2D>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            minX = bounds.min.x;
            maxX = bounds.max.x;
        }
    }

    private void ClampPosition()
    {
        if (groundObject != null && (minX != 0 || maxX != 0))
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minX, maxX),
                transform.position.y,
                transform.position.z
            );
        }
    }
    #endregion

    #region 점프 관련
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && isGrounded && !isAttacking)
        {
            isJumping = true;
            isMoving = false;
            animator.SetBool(moveAnimParam, false);
            animator.SetTrigger(jumpAnimTrigger);
        }
    }

    private void ApplyJump()
    {
        if (isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = false;
        }
    }
    #endregion

    #region 플레이어 공격 
    [Header("공격 설정")]
    public float attackCooldown = 0.5f;
    private float lastAttackTime;
    private bool isAttacking = false;
    public int attackForce = 10;
    public float attackRange = 4f;

    //공격 시작
    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) &&
            Time.time >= lastAttackTime + attackCooldown &&
            !isAttacking)
        {
            lastAttackTime = Time.time;
            isAttacking = true;
            animator.SetTrigger(attackAnimTrigger);

            // 이동 중지
            isMoving = false;
            animator.SetBool(moveAnimParam, false);
        }
    }

    // 공격 종료
    public void EndAttack()
    {
        isAttacking = false;
        Debug.Log("공격 종료 - 이동 가능"); // (디버깅용)
    }

    // 공격 범위 내 적 체크 (태그 사용)
    public void OnAttackAnimationEvent()
    {
        // 1. 모든 적 오브젝트 찾기 (태그로 필터링)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 2. 범위 내 적이 있는지 확인
        bool hitEnemy = false;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= attackRange)
            {
                ApplyDamage(enemy);
                hitEnemy = true;
            }
        }

        if (!hitEnemy)
        {
            Debug.Log("공격 범위 내 적 없음");
        }
    }

    // 적에게 데미지 
    public void ApplyDamage(GameObject enemy)
    {
        if (enemy != null)
        {
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.TakeDamage(attackForce);
                Debug.Log($"{enemy.name}에게 {attackForce} 데미지!");
            }
        }
    }
    #endregion

    #region 애니메이션 관련
    [Header("애니메이션 파라미터")]
    public string moveAnimParam = "isMoving";
    public string jumpAnimTrigger = "JumpTrigger";
    public string attackAnimTrigger = "AttackTrigger";
    #endregion

    #region 물리 및 충돌 처리
    private Rigidbody2D rb;
    private Animator animator;

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        ApplyJump();
        ClampPosition();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (isJumping)
            {
                isJumping = false;
                isMoving = Mathf.Abs(moveInput.x) > 0.1f;
                animator.SetBool(moveAnimParam, isMoving);
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    #endregion

    #region 디버그 시각화(Gizmo)
    void OnDrawGizmosSelected()
    {
        DrawAttackRange();
    }

    void DrawAttackRange()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    #endregion
}