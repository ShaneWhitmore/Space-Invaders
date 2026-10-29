using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager instance { get; private set; }

    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private bool edge_detection_invocation = false;
    // if edge_detection_invocation = false , the enemy has hit the left wall , if edge_detection_invocation = true , enemy has hit the right wall


    public event Action move_Enemy_Down;



    [Header("Game Variables")]
    [SerializeField] private int round = 0;



    [Header("Enemy Variables")]
    [SerializeField] private int enemysToSpawn = 11;

    [SerializeField] private float enemySpawnOffset = 1f;


    [SerializeField] private int spawnedEnemies = 0;




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

    }

    void OnDestroy()
    {
        if (enemyMovement != null)
        {
            enemyMovement.Edge_Detected -= OnScreenEdgeDetection;
            enemyMovement.Enemy_Was_Destroyed -= OnEnemyDestroyed;

        }
    }


    // Update is called once per frame
    void Update()
    {


        if (spawnedEnemies == 0)
        {
            SpawnNewEnemies();
            edge_detection_invocation = false; 
            round++;
        }

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
        Debug.Log(spawnedEnemies);
    }


    // Observer Pattern instructs Enemies to change direction and move down the screen
    public void MoveEnemyDown()
    {
        move_Enemy_Down?.Invoke();
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

            Debug.Log(spawnedEnemies);


        }
    }


    void RespawnPlayer()
    {

    }


    void GameOver()
    {

    }

}
