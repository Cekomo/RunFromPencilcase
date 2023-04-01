using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    
    private static PlayerMovementState playerMovementState;
    
    // animator parameters may not get adjusted properly for jumping
    private static readonly int SpeedX = Animator.StringToHash("SpeedX");
    private static readonly int SpeedY = Animator.StringToHash("SpeedY");
    private static readonly int TakeOff = Animator.StringToHash("takeOff");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");

    private const float RUNNING_SPEED = 5f;
    private const float JUMP_FORCE = 10f;
    
    private static float playerHorizontalVector;
    private static float playerVerticalVector;
    
    private float previousMoveX;

    private Rigidbody2D _rbPlayer;
    private BoxCollider2D _colliderPlayer;

    [SerializeField] private LayerMask platformsLayerMask;
    
    private void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        _colliderPlayer = GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        playerHorizontalVector = Input.GetAxisRaw("Horizontal");
        playerVerticalVector = Input.GetAxisRaw("Vertical");
        playerAnimator.SetFloat(SpeedX, Mathf.Abs(playerHorizontalVector));
    }

    private void FixedUpdate()
    {
        if (playerHorizontalVector == 0 && playerVerticalVector == 0) return;
        
        Run();
        FaceTowards();
        
        if (CheckIfGrounded())
            Jump();
    }

    private void Run()
    {
        _rbPlayer.velocity = new Vector2(playerHorizontalVector * RUNNING_SPEED, _rbPlayer.velocity.y);
    }

    private void Jump()
    {
        SetParametersToAnimateJump();
        
        _rbPlayer.AddForce(Vector2.up * JUMP_FORCE * playerVerticalVector, ForceMode2D.Impulse);
        playerAnimator.SetTrigger(TakeOff);
    }

    private bool CheckIfGrounded()
    {
        var bCBounds = _colliderPlayer.bounds;
        var raycastHit2D = Physics2D.BoxCast(bCBounds.center, bCBounds.size,
            0f, Vector2.down, 0.1f, platformsLayerMask);
        return !ReferenceEquals(raycastHit2D.collider, null); // changed from raycastHit2D.collider != null;
    }
    
    private void SetParametersToAnimateJump()
    {
        playerAnimator.SetBool(IsJumping, !CheckIfGrounded());
        playerAnimator.SetBool(IsGrounded, CheckIfGrounded());
        playerAnimator.SetFloat(SpeedY, _rbPlayer.velocity.y);
    }
    
    private void FaceTowards()
    {
        if ((int)previousMoveX == (int)playerHorizontalVector) return;

        var localScale = transform.localScale;
        if (playerHorizontalVector != 0)
            transform.localScale = new Vector3(1 * playerHorizontalVector, localScale.y, localScale.z);

        previousMoveX = playerHorizontalVector;
    }
}

