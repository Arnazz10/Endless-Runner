using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float runSpeed = 10f;
    public float jumpForce = 10f;
    public float slideDuration = 0.5f;
    public float slideHeight = 0.5f;
    
    [Header("Ground Check")]
    public LayerMask groundLayer = 1;
    public float groundCheckDistance = 0.1f;
    
    // Private variables
    private Rigidbody rb;
    private BoxCollider playerCollider;
    private Vector3 originalColliderSize;
    private Vector3 originalColliderCenter;
    private bool isGrounded;
    private bool isSliding = false;
    private float slideTimer = 0f;
    private bool isGameOver = false;
    
    // Events
    public System.Action OnPlayerDeath;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();
        
        // Store original collider dimensions
        originalColliderSize = playerCollider.size;
        originalColliderCenter = playerCollider.center;
        
        // Subscribe to game over event
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.OnGameOver += HandleGameOver;
        }
    }
    
    void Update()
    {
        if (isGameOver) return;
        
        CheckGrounded();
        HandleInput();
        UpdateSliding();
    }
    
    void FixedUpdate()
    {
        if (isGameOver) return;
        
        // Auto-run forward
        Vector3 forwardMovement = Vector3.forward * runSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }
    
    void CheckGrounded()
    {
        // Cast a ray downward to check if player is on ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
    
    void HandleInput()
    {
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding)
        {
            Jump();
        }
        
        // Slide input
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && isGrounded && !isSliding)
        {
            StartSlide();
        }
    }
    
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        
        // Reduce collider height for sliding
        Vector3 newSize = originalColliderSize;
        newSize.y = slideHeight;
        playerCollider.size = newSize;
        
        // Adjust collider center
        Vector3 newCenter = originalColliderCenter;
        newCenter.y = -0.25f;
        playerCollider.center = newCenter;
    }
    
    void UpdateSliding()
    {
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            
            if (slideTimer <= 0f)
            {
                EndSlide();
            }
        }
    }
    
    void EndSlide()
    {
        isSliding = false;
        
        // Restore original collider dimensions
        playerCollider.size = originalColliderSize;
        playerCollider.center = originalColliderCenter;
    }
    
    void HandleGameOver()
    {
        isGameOver = true;
        rb.velocity = Vector3.zero;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Check if collision is with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if trigger is with an obstacle
        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
    }
    
    void Die()
    {
        if (isGameOver) return;
        
        isGameOver = true;
        OnPlayerDeath?.Invoke();
    }
    
    void OnDestroy()
    {
        // Unsubscribe from events
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.OnGameOver -= HandleGameOver;
        }
    }
}
