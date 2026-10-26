using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    //  handle moving the enemy left and right on the screen
    // also handles moving the enemy closer to the player 

    // (observer pattern ) broadcast if the enemy reaches the edge of the screen and which edge that is (Enemy Manager will subscribe to this )

    public event Action Edge_Detected;

    

    [SerializeField] private Rigidbody2D enemy_rb;
    [SerializeField] private bool isAlive;

    [SerializeField] private bool moveLeft = false;


    [SerializeField] private float initial_Move_Speed = 3f;



    [SerializeField] private GameManager gameManager;

    void Start()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
        isAlive = true;
    }

    void Awake()
    {
        if(gameManager != null)
        {
            gameManager.move_Enemy_Down += OnMoveEnemyDown;
        }
    }

    void OnDestroy()
    {
        if(gameManager != null)
        {
            gameManager.move_Enemy_Down -= OnMoveEnemyDown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {

            MoveEnemy();
        }
    }


    private void OnMoveEnemyDown()
    {
        Debug.Log("Moving the enemy down now");
    }


    public void Edge_of_Screen_Reached()
    {
        Edge_Detected?.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D colission)
    {

        Debug.Log(colission.name);

        if (colission.CompareTag("Bullet"))
        {
            Debug.Log("Enemy was hit");
            Destroy(colission.gameObject);

            isAlive = false;
        }

        Destroy(gameObject);
    }


    private void MoveEnemy()
    {
        //check if the enemy has hit the edge of the screen
        // if the enemy has hit the edge of the screen , send that to the game manager (game manager will switch the direction of all enemies)

        // else move the enemy in the set direction

        Vector3 viewPortPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPortPos.x >= 1)
        {
            moveLeft = true;
            Edge_of_Screen_Reached();
        }
        else if (viewPortPos.x <= 0)
        {
            moveLeft = false;
            Edge_of_Screen_Reached();
        }

        if (moveLeft)
        {
            enemy_rb.linearVelocity = new Vector2( - initial_Move_Speed, 0f);
        }
        else
        {
            enemy_rb.linearVelocity = new Vector2(initial_Move_Speed, 0f);
        }
        
    }
}
