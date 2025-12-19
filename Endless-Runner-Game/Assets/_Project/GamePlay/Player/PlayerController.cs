using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Jump Value")]
    [SerializeField] private float jumpForce = 7f;
    [Header("Player Animation")]
    [SerializeField] Animator animator;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRayLength = 0.15f;

    private Rigidbody2D _rb;
    private Collider2D _collider;

    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        CheckGround();
        HandleInput();
        PlayerAnimationState();
    }

    private void HandleInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            TryJump();
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TryJump();
        }
#endif
    }

    private void TryJump()
    {
        if (!_isGrounded)
            return;

        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void PlayerAnimationState()
    {
        animator.SetBool("isJump", !_isGrounded);
    }

    private void CheckGround()
    {
        Bounds bounds = _collider.bounds;

        Vector2 origin = new Vector2(bounds.center.x, bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            groundRayLength,
            groundLayer
        );

        _isGrounded = hit.collider != null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_collider == null) return;

        Gizmos.color = Color.green;
        Bounds bounds = _collider.bounds;
        Vector2 origin = new Vector2(bounds.center.x, bounds.min.y);

        Gizmos.DrawLine(
            origin,
            origin + Vector2.down * groundRayLength
        );
    }
#endif


}
