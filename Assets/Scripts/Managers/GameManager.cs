using System;
using System.Collections;
using UnityEngine.SceneManagement;

using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager instance { get; private set; }

    


    [SerializeField] private bool edge_detection_invocation = false;
    // if edge_detection_invocation = false , the enemy has hit the left wall , if edge_detection_invocation = true , enemy has hit the right wall


    public event Action move_Enemy_Down;



    [Header("Game Variables")]
    [SerializeField] private int round = 0;
    [SerializeField] private bool gameOver;





    [Header("Enemy Variables")]
    [SerializeField] private int enemysToSpawn = 11;

    [SerializeField] private float enemySpawnOffset = 1f;


    [SerializeField] private int spawnedEnemies = 0;

    [SerializeField] private List<Rigidbody2D> liveEnemyBullets = null;


    [SerializeField] private int maxNumberOfEnemyBullets = 7;


    [SerializeField] private GameOverTarget gameOverTarget;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private Transform enemySpawnPoint;





    [Header("Player Variables")]
    [SerializeField] private int playerLives = 3;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform playerSpawnPoint;





    // game manager should spawn a fixed number of enemies, when an enemy dies it should invoke a method to inform the manager it died
    // Game manager should have a method to subtract the number of enemies 
    // if the number of enemies is 0 , move to next level (increment a level variable) which can be used for handling special enemies spawning once per round







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


        if (enemyMovement != null)
        {
            enemyMovement.Edge_Detected += OnScreenEdgeDetection;
            enemyMovement.Enemy_Was_Destroyed += OnEnemyDestroyed;
        }
    }


    void Start()
    {
        gameOverTarget.enemyEnteredTarget += OnEnemyEnteredGameOver;
        gameOver = false;
        playerLives = 3;
    }

    void OnDestroy()
    {
        if (enemyMovement != null)
        {
            enemyMovement.Edge_Detected -= OnScreenEdgeDetection;
            enemyMovement.Enemy_Was_Destroyed -= OnEnemyDestroyed;

        }
        gameOverTarget.enemyEnteredTarget += OnEnemyEnteredGameOver;

    }


    // Update is called once per frame
    void Update()
    {

        if (gameObject && PlayerInput_Manager.instance.WasRestartPressedThisFrame())
        {
            StartCoroutine(Restart());
        }


        if (gameOver == false)
        {
            if (spawnedEnemies == 0)
            {
                edge_detection_invocation = false;
                IncrementRound();
                SpawnNewEnemies();
            }
        }
    }

    public bool getGameOverVariable()
    {
        return gameOver;
    }



    // Observer Pattern Method tied to the edge detection broadcast from the enemy gameObject
    private void OnScreenEdgeDetection(float detected_edge)
    {
        //Debug.Log(detected_edge);
        bool detected_edge_bool_conversion = false;




        if (detected_edge >= 1)
        {
            detected_edge_bool_conversion = true;
        }
        else if (detected_edge <= 0)
        {
            detected_edge_bool_conversion = false;
        }

        if (detected_edge_bool_conversion != edge_detection_invocation)
        {
            edge_detection_invocation = detected_edge_bool_conversion;
            MoveEnemyDown();
        }
    }

    private void OnEnemyDestroyed()
    {
        spawnedEnemies--;
        // Debug.Log(spawnedEnemies);
    }


    // Observer Pattern instructs Enemies to change direction and move down the screen
    public void MoveEnemyDown()
    {
        move_Enemy_Down?.Invoke();
    }

    public void IncrementRound()
    {
        round++;

        RoundManager.instance.ChangeRound(round);


        Debug.Log("NEW ROUND: " + round);
    }


    void SpawnNewEnemies()
    {
        Vector2 lastEnemySpawnPos = new Vector2(enemySpawnPoint.position.x, enemySpawnPoint.position.y);



        // take a linked list approach using the previously spawned enemies position as a reference for spawning the next enemy
        for (int i = 0; i < enemysToSpawn; i++)
        {
            Vector2 newSpawnPos = new Vector2(lastEnemySpawnPos.x + enemySpawnOffset, lastEnemySpawnPos.y);


            EnemyMovement newEnemy = Instantiate(enemyMovement, newSpawnPos, Quaternion.identity);


            newEnemy.Initialize(this);

            newEnemy.Edge_Detected += OnScreenEdgeDetection;
            newEnemy.Enemy_Was_Destroyed += OnEnemyDestroyed;


            lastEnemySpawnPos = newEnemy.GetEnemyLocation();


            spawnedEnemies++;
        }
    }

    private void OnEnemyEnteredGameOver()
    {
        //Disable Player Movement
        // Invoke Game Over method

        Debug.Log("Enemy entered end (called from gameManager)");
        StartCoroutine(GameOver());
    }


    void RespawnPlayer()
    {
        playerLives--;

        if (playerLives == 0)
        {
            GameOver();
        }
        else
        {
            RoundManager.instance.ChangeLivesCounter(playerLives);
        }
    }

    

    public Vector2 ReturnPlayerCoordinates()
    {
        return playerMovement.ReturnPlayerCoordinates();
    }

    public void IncrementAliveEnemyBullets(Rigidbody2D enemyBullet)
    {
        liveEnemyBullets.Add(enemyBullet);
    }


    public bool ReturnBoolAliveEnemyBullets()
    {
        if (liveEnemyBullets.Count < maxNumberOfEnemyBullets)
        {
            return true;
        }
        return false;
    }




    private IEnumerator Restart()
    {
        gameOver = false;
        RoundManager.instance.GameOver(gameOver);

        //Restart Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return null;
    }


    private IEnumerator GameOver()
    {
        // Disable player movement
        // Disable Enemy movement

        PlayerInput_Manager.instance.OnGameOver();
        //set some bool value to true for gameOver , check this against a button press in the Update method to restart the game
        // Display "Game Over"
        //Stop the Game
        gameOver = true;
        RoundManager.instance.GameOver(gameOver);

        yield return null;

    }
}
