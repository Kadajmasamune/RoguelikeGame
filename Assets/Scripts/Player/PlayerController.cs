using UnityEngine;
using Common;
using System.Collections.Generic;
using UnityEditor.Animations;

public class PlayerController : MonoBehaviour, IHasDirection
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Animation")]
    [SerializeField] private AnimatorController PlayerAnimatorController;

    private Rigidbody2D playerRb;
    private Vector2 movementInput;

    private AnimHashGenerator animHashGenerator = new AnimHashGenerator(); 
    public Dictionary<string, int> AnimationClipHashes { get; private set; } = new Dictionary<string, int>();

    public Direction CurrentDirection { get; private set; }

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animHashGenerator.GenerateAnimHash(AnimationClipHashes, PlayerAnimatorController);
    }

    private void Update()
    {
        movementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            Vector2 newPosition = playerRb.position + movementInput * moveSpeed * Time.fixedDeltaTime;
            playerRb.MovePosition(newPosition);
            CurrentDirection = GetDirectionFromInput(movementInput.x, movementInput.y);
        }
        else
        {
            playerRb.linearVelocity = Vector2.zero;
        }
    }

    private Direction GetDirectionFromInput(float x, float y)
    {
        if (x == 0 && y == 0) return Direction.None;
        if (x > 0 && y == 0) return Direction.Right;
        if (x < 0 && y == 0) return Direction.Left;
        if (x == 0 && y > 0) return Direction.Up;
        if (x == 0 && y < 0) return Direction.Down;
        if (x > 0 && y > 0) return Direction.UpRight;
        if (x < 0 && y > 0) return Direction.UpLeft;
        if (x > 0 && y < 0) return Direction.BottomRight;
        if (x < 0 && y < 0) return Direction.BottomLeft;

        return Direction.None;
    }
}