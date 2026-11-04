using System;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private Rigidbody2D ShipRb;
    public bool isAlive = true;
    [SerializeField] private float shipAcceleration = 5f;

    [SerializeField] private GameManager gameManager;



    void Start()
    {
        ShipRb = GetComponent<Rigidbody2D>();

    }

    public void InitializePlayer(GameManager gm)
    {
        gameManager = gm;        
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        

        if(collision.CompareTag("EnemyBullet") && isAlive)
        {
            //Player has been hit with an enemy bullet, inform the game manager so a life can be deducted and the player can be respawned

            isAlive = false;
            GameManager.instance.RespawnPlayer();
            Destroy(gameObject);
            
        }
    }
}
