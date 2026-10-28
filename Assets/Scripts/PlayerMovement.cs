using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float normalGravity = 3f;

    [Header("Wall Slide & Jump")]
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(7f, 10f);
    [SerializeField] private float wallJumpLockTime = 0.2f;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private Vector3 startScale;

    private float horizontalInput;
    private float wallJumpTimer;
    private bool jumpRequested;

    private float knockbackTimer;

    public void Knockback(Vector2 velocity, float duration = 0.25f)
    {
        knockbackTimer = duration;
        body.linearVelocity = velocity;   // в Unity 6; в старых версиях body.velocity
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        startScale = transform.localScale;
        wallJumpTimer = wallJumpLockTime + 1f; // в начале игры блокировки нет
    }

    private void Update()
    {
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
            return; // пока идёт отброс, управление и перезапись скорости отключены
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            jumpRequested = true;

        // Поворот спрайта
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(startScale.x), startScale.y, startScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(startScale.x), startScale.y, startScale.z);

        // Параметры Animator
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
    }

    private void FixedUpdate()
    {
        wallJumpTimer += Time.fixedDeltaTime;

        // Во время отброса не трогаем скорость
        if (knockbackTimer > 0f)
        {
            body.gravityScale = normalGravity;
            anim.SetBool("wallSlide", false);
            jumpRequested = false;
            return;
        }

        bool sliding = false;

        if (wallJumpTimer > wallJumpLockTime)
        {
            bool grounded = IsGrounded();
            RaycastHit2D wallHit = GetWallHit();
            sliding = wallHit.collider != null && !grounded && body.linearVelocity.y < 0f;

            if (sliding)
            {
                body.linearVelocity = new Vector2(0f, -wallSlideSpeed);

                // Прижим к стене с учётом смещения коллайдера относительно pivot
                float direction = Mathf.Sign(transform.localScale.x);
                float centerOffsetX = boxCollider.bounds.center.x - body.position.x;
                float targetX = wallHit.point.x - boxCollider.bounds.extents.x * direction - centerOffsetX;
                body.position = new Vector2(targetX, body.position.y);
            }
            else
            {
                body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            }

            if (jumpRequested)
            {
                ExecuteJump();
                sliding = false; // любой прыжок заканчивает скольжение
            }
        }

        // Единственное место, где меняются гравитация и анимация скольжения
        body.gravityScale = sliding ? 0f : normalGravity;
        anim.SetBool("wallSlide", sliding);

        jumpRequested = false;
    }

    private void ExecuteJump()
    {
        if (IsGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            anim.SetTrigger("jump");
        }
        else if (OnWall())
        {
            wallJumpTimer = 0f;

            // Отскок от стены в противоположную сторону
            float jumpDirection = -Mathf.Sign(transform.localScale.x);
            body.linearVelocity = new Vector2(jumpDirection * wallJumpForce.x, wallJumpForce.y);

            // Разворот лицом в сторону прыжка
            transform.localScale = new Vector3(jumpDirection * Mathf.Abs(startScale.x), startScale.y, startScale.z);
            anim.SetTrigger("jump");
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    private RaycastHit2D GetWallHit()
    {
        float direction = Mathf.Sign(transform.localScale.x);
        Vector2 rayOrigin = new Vector2(boxCollider.bounds.center.x + boxCollider.bounds.extents.x * direction, boxCollider.bounds.center.y);
        return Physics2D.Raycast(rayOrigin, new Vector2(direction, 0f), 0.1f, wallLayer);
    }

    private bool OnWall()
    {
        return GetWallHit().collider != null;
    }
}


//new email added