using System;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private Rigidbody ShipRb;
    public bool isAlive = true;
    [SerializeField] private float shipAcceleration = 5f;



    void Start()
    {
        ShipRb = GetComponent<Rigidbody>();
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
        if (isAlive)
        {
            HandleShipMovement();
        }
    }


    private void HandleShipMovement()
    {
        Vector2 input = PlayerInput_Manager.instance.MoveInput();

        float inputX = input.x;


        Vector3 viewPortPos = Camera.main.WorldToViewportPoint(transform.position);


        if ((viewPortPos.x > 1 && inputX > 0) || (viewPortPos.x < 0 && inputX < 0))
        {
            ShipRb.linearVelocity = new Vector2(0f,0f);
        }
        else
        {
            ShipRb.linearVelocity = new Vector2(inputX * shipAcceleration, 0f);

        }
    }

    public Vector2 ReturnPlayerCoordinates()
    {
        return ShipRb.position;
    }
}
