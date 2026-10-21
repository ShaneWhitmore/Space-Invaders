using System;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private Rigidbody rb;
    public bool isAlive = true;
    [SerializeField] private float shipAcceleration = 5f;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        if (PlayerInput_Manager.instance != null)
        {
            PlayerInput_Manager.instance.OnEnable();
        }
    }

    void OnDisable()
    {
        if (PlayerInput_Manager.instance != null)
        {
            PlayerInput_Manager.instance.OnDisable();
        }
    }



    void Update()
    {
        if (PlayerInput_Manager.instance.WasShootPressedThisFrame())
        {
            Debug.Log("Shoot button was pressed");
        }
        if (isAlive)
        {
            HandleShipMovement();
        }
    }


    private void HandleShipMovement()
    {
        Vector2 input = PlayerInput_Manager.instance.MoveInput();

        float inputX = input.x;

        rb.linearVelocity = new Vector2(inputX * shipAcceleration, 0f);

    }
}
