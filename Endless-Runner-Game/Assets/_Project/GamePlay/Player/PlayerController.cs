using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    public OnscreenInputs inputActions;
    [Header("Jump Value")]
    [SerializeField] private float jumpForce = 7f;
    [Header("Player Animation")]
    [SerializeField] Animator animator;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRayLength = 0.15f;

    [Header("Dubble Jump")]
    private int _remainingJumps = 1; 

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private bool _isGrounded;
    AudioSource runnindAudioSource;
    AudioSource jumpAudioSource;

    void OnEnable()
    {
        inputActions.Enable();
    }
    
    void OnDisable()
    {
        inputActions.Disable();

    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        runnindAudioSource = GetComponent<AudioSource>();
        jumpAudioSource = GetComponentInChildren<AudioSource>();
        inputActions = new OnscreenInputs();
    }

 

    private void Update()
    { 
        _rb.simulated = GameManager.Instance.CurrentState == GameState.inGame ? true : false;
        if(GameManager.Instance.CurrentState != GameState.inGame)return;
        HandleInput();
        PlayerAnimationState();
    }

    void FixedUpdate()
    {
        CheckGround();
    }


    private void HandleInput()
    {

        if ( inputActions.MainPlayer.Jump.WasPressedThisFrame())
        {
            TryJump();
        }

    }
   
    private void TryJump()
    {
        if (_remainingJumps <= 0)return;
      //  jumpAudioSource.Play();
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _remainingJumps--;
    }

    void PlayerAnimationState()
    {
        animator.SetBool("isJump", !_isGrounded); 

    }

    private void CheckGround()
    {
        bool wasGrounded = _isGrounded;
        Bounds bounds = _collider.bounds;

        Vector2 origin = new Vector2(bounds.center.x, bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            groundRayLength,
            groundLayer
        );

        _isGrounded = hit.collider != null;
        // rest Dubble jump 
        if (!wasGrounded && _isGrounded) _remainingJumps =SaveService.GetDubbleJumpPower() ? 2:1; 
        // play run audio
        runnindAudioSource.mute =  GameManager.Instance.CurrentState == GameState.inGame && _isGrounded ? false: true;
    }




}
