using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : SingletonMonoBehaviour<PlayerJump>
{
    [SerializeField] float jumpSpeed;
    [SerializeField] float jumpPressBufferTime = .05f;
    [SerializeField] float jumpGroundGraceTime = .3f;

    PlayerMovement player;

    public int maxJumps;

    bool isTryingToJump;
    float lastJumpPressTime;
    float lastGroundTime;
    int jumps;



    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerMovement>();
    }


    private void OnEnable()
    {
        EventsHandler.OnBeforeMove += OnBeforeMove;
        EventsHandler.OnGroundStateChanged += OnGroundStateChanged;
    }

    private void OnDisable()
    {
        EventsHandler.OnBeforeMove -= OnBeforeMove;
        EventsHandler.OnGroundStateChanged -= OnGroundStateChanged;
    }
    public void OnJump()
    {
        isTryingToJump = true;
        lastGroundTime = Time.time;
        Debug.Log(Time.time);
    }
    private void OnBeforeMove()
    {
        if (player.characterController.isGrounded) jumps = 0;


        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;
        bool wasGrounded = Time.time - lastGroundTime < jumpGroundGraceTime;

        bool isOrWasTryingToJump = isTryingToJump || (wasTryingToJump && player.characterController.isGrounded);
        bool isOrWasGrounded = player.characterController.isGrounded || wasGrounded;

        bool jumpAllowed = jumps <= maxJumps;

        Debug.Log("" + wasTryingToJump + wasGrounded + isOrWasTryingToJump + isOrWasGrounded);

        if (jumpAllowed && isOrWasTryingToJump && isOrWasGrounded || jumpAllowed && isTryingToJump)
        {
            EventsHandler.CallOnGroundStateChangedEvent(false);
            player.velocity.y += jumpSpeed;
            jumps++;
        }

        isTryingToJump = false;
    }

    private void OnGroundStateChanged(bool isGrounded)
    {
        if(!isGrounded) lastGroundTime = Time.time;
    }
}
