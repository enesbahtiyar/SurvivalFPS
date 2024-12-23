using System;
using UnityEngine;

public class PlayerMovement : SingletonMonoBehaviour<PlayerMovement>
{
    [Header("Variables")]
    [SerializeField] float mouseSensivity;
    [SerializeField] float movementSpeed;
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] PlayerJump playerJump;

    public Transform cameraTransform;

    public float movementSpeedMultiplier;

    public CharacterController characterController;

    public Vector3 velocity;
    Vector2 look;

    public float Height
    {
        get => characterController.height;
        set => characterController.height = value;
    }
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(!InventorySystem.Instance.isOpen && !CraftingSystem.Instance.isOpen)
        {
            UpdateMovement();
            GetMovementInput();
            UpdateLook();
        }

    }

    private void UpdateLook()
    {
        float lookX = Input.GetAxis("Mouse X");
        float lookY = Input.GetAxis("Mouse Y");

        look.x += lookX * mouseSensivity;
        look.y += lookY * mouseSensivity;

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);

    }

    private Vector3 GetMovementInput()
    {
        Vector3 input = new Vector3();
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        input += transform.forward * inputZ;
        input += transform.right * inputX;
        input = Vector3.ClampMagnitude(input, 1f);
        input *= movementSpeed * movementSpeedMultiplier;



        return input;

    }

    private void UpdateMovement()
    {
        movementSpeedMultiplier = 1f;
        EventsHandler.CallOnBeforeMoveEvent();

        Vector3 input = GetMovementInput();

        var factor = acceleration * Time.deltaTime;

        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);
        
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump.Instance.OnJump();
        }
        */
        

        characterController.Move(velocity * Time.deltaTime);
    }
}
