using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    
    private static PlayerMovementState playerMovementState;
    
    // animator parameters may not get adjusted properly for jumping
    private static readonly int SpeedX = Animator.StringToHash("SpeedX");
    private static readonly int SpeedY = Animator.StringToHash("SpeedY");
    private static readonly int TakeOff = Animator.StringToHash("TakingOff");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    private const float RUNNING_SPEED = 7.5f;
    private const float JUMP_FORCE = 16f;
    
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
        playerAnimator.SetFloat(SpeedY, _rbPlayer.velocity.y);

        if (CheckIfGrounded() && playerAnimator.GetBool(IsJumping))
            playerAnimator.SetBool(IsJumping, false);
    }

    private void FixedUpdate()
    {
        if (playerHorizontalVector != 0)
        {
            Run();
            FaceTowards();
        }

        if (playerVerticalVector > 0.1f && CheckIfGrounded())
        {
            Jump();
        }
    }

    private void Run()
    {
        _rbPlayer.velocity = new Vector2(playerHorizontalVector * RUNNING_SPEED, _rbPlayer.velocity.y);
    }

    private void Jump()
    {
        _rbPlayer.AddForce(Vector2.up * (JUMP_FORCE * playerVerticalVector), ForceMode2D.Impulse);
        playerAnimator.SetTrigger(TakeOff);
        playerAnimator.SetBool(IsJumping, true);
    }

    private bool CheckIfGrounded()
    {
        var bCBounds = _colliderPlayer.bounds;
        var raycastHit2D = Physics2D.BoxCast(bCBounds.center, bCBounds.size,
            0f, Vector2.down, 0.1f, platformsLayerMask);
        var isGrounded = !ReferenceEquals(raycastHit2D.collider, null);
        playerAnimator.SetBool(IsGrounded, isGrounded);
        return isGrounded; // changed from raycastHit2D.collider != null;
    }

    private void FaceTowards()
    {
        transform.rotation = Quaternion.Euler(0, 90 - playerHorizontalVector * 90, 0);
    }


}

