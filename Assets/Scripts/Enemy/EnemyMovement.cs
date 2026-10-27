using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    //  handle moving the enemy left and right on the screen
    // also handles moving the enemy closer to the player 

    // (observer pattern ) broadcast if the enemy reaches the edge of the screen and which edge that is (Enemy Manager will subscribe to this )

    public event Action<float> Edge_Detected;

    

    [SerializeField] private Rigidbody2D enemy_rb;
    [SerializeField] private bool isAlive;

    [SerializeField] private bool moveLeft = false;


    [SerializeField] private float initial_Move_Speed = 3f;
    [SerializeField] private float enemyDropDownDistance = 0.5f;



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
        //Debug.Log("Moving the enemy down now");

        moveLeft = !moveLeft;

        Vector2 enemyCurrentPos = enemy_rb.position;

        enemy_rb.position = new Vector2(enemyCurrentPos.x , enemyCurrentPos.y - enemyDropDownDistance);


    }


    public void Edge_of_Screen_Reached(float edge)
    {
        Edge_Detected?.Invoke(edge);
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

        //Debug.Log(enemy_rb.position);


        // broadcast to the gameManager that the enemy reached the edge of the screen
        // this will then broadcast to all enemies to change direction and move down the screen
        if (viewPortPos.x >= 1 || viewPortPos.x <= 0)
        {
            Edge_of_Screen_Reached(viewPortPos.x);
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
