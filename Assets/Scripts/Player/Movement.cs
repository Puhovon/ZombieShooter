using Configs;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Movement : MonoBehaviour
{
    private Vector3 moveVector;
    [SerializeField] private CharacterController _cc;
    [SerializeField] private float jumpHeight = 2;
    [SerializeField] private float gravity = -9.81f;
    private PlayerConfig _config;
    
    private bool isJumping;
    private Vector3 velocity;
    private InputSystem_Actions _input;

    
    [Inject]
    private void Construct(InputSystem_Actions input, PlayerConfig config)
    {
        _config = config;
        _input = input;
        _input.Player.Move.performed += GetMove;
        _input.Player.Move.canceled += CancelMove;
        input.Player.Enable();
    }
    
    private void Update()
    {
        if (_cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (_input.Player.Jump.IsPressed() && _cc.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        _cc.Move(velocity * Time.deltaTime);
        
        if (moveVector == Vector3.zero)
            return;
        Vector3 move = Vector3.zero;
        move = transform.right * moveVector.x + transform.forward * moveVector.z;
        _cc.Move(move * _config.Speed * Time.deltaTime);
    }

    private void GetMove(InputAction.CallbackContext callbackContext)
    {
        var input = callbackContext.ReadValue<Vector2>();
        moveVector = new Vector3(input.x, 0, input.y);
    }

    private void CancelMove(InputAction.CallbackContext callbackContext)
    {
        moveVector = Vector3.zero;
    }
}
