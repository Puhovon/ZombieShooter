using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class TestInput : MonoBehaviour
{
    [SerializeField] private CharacterController _cc;

    [SerializeField] private float _speed;
    private InputSystem_Actions input;
    float xrotation;
    float yrotation;
    Vector2 mousemovement;
    float camera_y;
    Vector3 pos;
    public float mouse_sensitivity=10f;
    public Vector3 moveVector;

    #region rotation try 2
    
    public float _verticalRotationLowerBound;
    public float _verticalRotationHigherBound;
    public float _rotateSpeedX;
    public float _rotateSpeedY;
    public float jumpHight;
    public float gravity = -9.81f;
    public bool isJumping;
    public Vector3 velocity;
    
    #endregion
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Look.performed += GetLook;
        input.Player.Move.performed += GetMove;
        input.Player.Move.canceled += context => moveVector = Vector2.zero;
        input.Player.Jump.performed += context => isJumping = true;
        input.Player.Jump.canceled += context => isJumping = false;
        input.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (_cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        Vector3 move = Vector3.zero;
        if (moveVector != Vector3.zero)
        {
            move = transform.right * moveVector.x + transform.forward * moveVector.z;
        }
        
        _cc.Move(move * _speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        _cc.Move(velocity * Time.deltaTime);
    }

    private void GetMove(InputAction.CallbackContext obj)
    {
        var movement = obj.ReadValue<Vector2>();
        moveVector = new Vector3(movement.x, moveVector.y, movement.y);
    }

    private void GetLook(InputAction.CallbackContext callbackContext)
    {
        var _currentLookDelta = callbackContext.ReadValue<Vector2>(); 
        float horizontalRotationDelta = _currentLookDelta.x * _rotateSpeedX * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(horizontalRotationDelta, Vector3.up);

        float verticalRotationDelta = -_currentLookDelta.y * _rotateSpeedY * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(verticalRotationDelta, Vector3.right);

        var eulerAngles = transform.localEulerAngles;
        eulerAngles.z = 0;

        var verticalAngle = transform.localEulerAngles.x;

        if (verticalAngle > 180 && verticalAngle < 360 - _verticalRotationHigherBound)
        {
            eulerAngles.x = 360 - _verticalRotationHigherBound;
        }
        else if(verticalAngle < 180 && verticalAngle > -_verticalRotationLowerBound)
        {
            eulerAngles.x = -_verticalRotationLowerBound;
        }
        transform.localEulerAngles = eulerAngles;
    }
}
