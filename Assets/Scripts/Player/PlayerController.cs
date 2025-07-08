using UnityEngine;
using Common;
using System.Collections.Generic;
using UnityEditor.Animations;

public class PlayerController : MonoBehaviour, IHasDirection, IHasVelocity, IHasBooleans
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float RollSpeed;
    private EntityMovement PlayerMovementData = new EntityMovement();



    [SerializeField] AnimationClip RollingClip;

    float rollDuration;
    float rollTimer = 0f;

    [Header("Booleans")]
    public bool IsRolling { get; private set; }


    [Header("Animation")]
    [SerializeField] AnimatorController PlayerAnimatorController;




    private Rigidbody2D playerRb;
    private Vector2 movementInput;
    private Vector2 rollDirection;



    private AnimHashGenerator animHashGenerator = new AnimHashGenerator();
    public Dictionary<string, int> AnimationClipHashes { get; set; } = new Dictionary<string, int>();



    public Direction CurrentDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        RollSpeed = moveSpeed + 2;

        animHashGenerator.GenerateAnimHash(AnimationClipHashes, PlayerAnimatorController);
    }

    private void Update()
    {
        movementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        if (!IsRolling && Input.GetKeyDown(KeyCode.V) && movementInput != Vector2.zero)
        {
            IsRolling = true;
            rollTimer = RollingClip.length;
            rollDirection = movementInput;
        }
    }

    private void FixedUpdate()
    {
        if (IsRolling)
        {
            rollTimer -= Time.fixedDeltaTime;

            if (rollTimer <= 0f)
            {
                IsRolling = false;
            }

            Vector2 newPosition = playerRb.position + rollDirection * RollSpeed * Time.fixedDeltaTime;
            playerRb.MovePosition(newPosition);
            CurrentDirection = PlayerMovementData.GetDirectionFromInput(rollDirection.x, rollDirection.y);
            CurrentVelocity = rollDirection * RollSpeed;
        }
        else if (movementInput != Vector2.zero)
        {
            // Normal movement
            Vector2 newPosition = playerRb.position + movementInput * moveSpeed * Time.fixedDeltaTime;
            playerRb.MovePosition(newPosition);
            CurrentDirection = PlayerMovementData.GetDirectionFromInput(movementInput.x, movementInput.y);
            CurrentVelocity = movementInput * moveSpeed;
        }
        else
        {
            // Idle
            CurrentVelocity = Vector2.zero;
        }
    }

}