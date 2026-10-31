using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static PlayerInput_Manager instance { get; private set; }

    public InputSystem_Actions inputActions;

    void Start()
    {

    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        inputActions = new InputSystem_Actions();
    }

    void Update()
    {

    }

    public void OnEnable()
    {
        inputActions.Player.Enable();

    }

    public void OnDisable()
    {
        inputActions.Player.Disable();
    }


    public Vector2 MoveInput()
    {
        return inputActions.Player.Move.ReadValue<Vector2>().normalized;
    }

    public bool WasShootPressedThisFrame()
    {
        return inputActions.Player.Attack.WasPressedThisFrame();
    }

    public bool WasRestartPressedThisFrame()
    {
        return inputActions.Player.Jump.WasPressedThisFrame();
    }


    public void OnGameOver()
    {
        inputActions.Player.Attack.Disable();
        inputActions.Player.Move.Disable();
    }

}
