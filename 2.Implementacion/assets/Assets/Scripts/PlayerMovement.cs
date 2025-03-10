using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator animator;

    // Variebles de movimiento
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpSpeed = 12f;
    [SerializeField] private float doubleJumpSpeed = 10f;
    private bool canDoubleJump = false;
    private bool hasDoubleJumped = false;
    private bool isWallSliding;
    [SerializeField] private float wallSlidingSpeed = 2f;

    // Variables para manejar el salto  mejorado
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 3f;

    // Detección de pared o suelo
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;

    // Coyote time
    private float coyoteTime = 0.12f;
    private float coyoteTimeCounter;


    private bool isFacingRight = true;

    [SerializeField] GameObject optionsPanel;

    // Inicializamos RigidBody y animator
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ir al menu cuando pulsamos escape
        if (Input.GetKey(KeyCode.Escape))
        {
            optionsPanel.SetActive(true);
            Time.timeScale = 0;
        }

        HandleCoyoteTime();
        HandleJump();
        HandleAnimations();
        WallSlide();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJumpPhysics();
    }

    private void HandleCoyoteTime()
    {

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime; //reiniciamos coyoteTime
            canDoubleJump = true;
            hasDoubleJumped = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void HandleJump()
    {
        // Si pulsa una tecla de salto comprobamos si puede saltar o hacer doble salto
        if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (coyoteTimeCounter > 0f && !isWallSliding)
            {
                Jump(jumpSpeed);
                coyoteTimeCounter = 0f; //deshabilitamos coyoteTime
            }
            else if (canDoubleJump && !hasDoubleJumped && !isWallSliding)
            {
                Jump(doubleJumpSpeed);
                hasDoubleJumped = true;
            }
        }
    }

    // Manejo de salto y salto doble
    private void Jump(float jumpForce)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetBool("Jump", true);
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        // Damos la vuelta al personaje según donde esté andando
        if ((horizontalInput > 0f && !isFacingRight) || (horizontalInput < 0f && isFacingRight))
        {
            Flip();
        }
        // Cuando no estamos pulsando tecla de movimiento paramos el personaje
        if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            rb.linearVelocity = new Vector2(Mathf.MoveTowards(rb.linearVelocity.x, 0, 40 * Time.deltaTime), rb.linearVelocity.y);
        }
    }

    private void HandleJumpPhysics()
    {
        // si no está pulsando la tecla de salto y está cayendo subimos la gravedad para que caiga más rápido
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // Si está en el aire y seguimos pulsando espacio se aplica una gravedad menor para que suba hasta el máximo que al velocidad de salto le permita.
        else if (rb.linearVelocity.y > 0 && !Input.GetKey("space") && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void HandleAnimations()
    {
        animator.SetBool("Run", Mathf.Abs(rb.linearVelocity.x) > 0.1f && IsGrounded()); // Animación de correr al movernos en x mientras estamos en el suelo.
        animator.SetBool("Jump", !IsGrounded() && rb.linearVelocity.y > 0.1f); // Animación de salto cuando no estamos en el suelo y subimos.
        animator.SetBool("Falling", !IsGrounded() && rb.linearVelocity.y < -0.1f); // Animación de caer cuando no estamos en el suelo y bajamos.
        animator.SetBool("DoubleJump", hasDoubleJumped); // Animación de salto doble
    }

    // Damos de vuelta al personaje manejando Scale en x (-1 izquierda, 1 derecha).
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    // Crea un circulo alrededor de la posición del collider (groundCheck) de jugador 
    // que comprueba si toca el suelo (groundLayer), con un radio de 0.2f.
    // Devuelve true si el circulo toca el groundLayer y false en caso contrario.
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    // Crea un circulo alrededor de la posición del collider (wallCheck) de jugador 
    // que comprueba si toca la pared (wallLayer), con un radio de 0.2f.
    // Devuelve true si el circulo toca el wallLayer y false en caso contrario.
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    // Cuando toca la pared le asigna la animación de slide
    // asigna una velocidad distinta (que debde ser pequeña) para hacer como que te deslizas.
    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlidingSpeed);
            animator.SetBool("Slide", true);
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("Slide", false);
        }
    }
}