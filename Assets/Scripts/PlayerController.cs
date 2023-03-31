using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerMovementState playerMovementState;

    private const float RUNNING_SPEED = 5f;
    private const float JUMP_FORCE = 10f;
    
    private static float playerHorizontalVector;
    private static float playerVerticalVector;

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
    }

    private void FixedUpdate()
    {
        if (playerHorizontalVector == 0 && playerVerticalVector == 0) return;
        
        Run();
        if (IsGrounded())
            Jump();
    }

    private void Run()
    {
        _rbPlayer.velocity = new Vector2(playerHorizontalVector * RUNNING_SPEED, _rbPlayer.velocity.y);
    }

    private void Jump()
    {
        _rbPlayer.AddForce(Vector2.up * JUMP_FORCE * playerVerticalVector, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        var bCBounds = _colliderPlayer.bounds;
        var raycastHit2D = Physics2D.BoxCast(bCBounds.center, bCBounds.size,
            0f, Vector2.down, 0.1f, platformsLayerMask);
        return !ReferenceEquals(raycastHit2D.collider, null); // changed from raycastHit2D.collider != null;
    }
    
    // private bool IsGrounded() 
    // {
    //     var boxSize = _colliderPlayer.size * transform.localScale.x;
    //     var boxCenter = (Vector2)transform.position + _colliderPlayer.offset;
    //
    //     RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, 
    //         0.1f, platformsLayerMask);
    //
    //     return hit.collider != null;
    // }
}

