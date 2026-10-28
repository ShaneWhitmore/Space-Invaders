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



    int spawnEnemy = 0;

    


    // game manager should spawn a fixed number of enemies, when an enemy dies it should invoke a method to inform the manager it died
    // Game manager should have a method to subtract the number of enemies 
    // if the number of enemies is 0 , move to next level (increment a level variable) which can be used for handling special enemies spawning once per round



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


        if (enemyMovement != null)
        {
            enemyMovement.Edge_Detected += OnScreenEdgeDetection;
        }
    }

    void OnDestroy()
    {
        if (enemyMovement != null)
        {
            enemyMovement.Edge_Detected -= OnScreenEdgeDetection;
        }
    }


    // Update is called once per frame
    void Update()
    {
        

        if(spawnEnemy == 0)
        {
            EnemyMovement newEnemy = Instantiate(enemyMovement, enemySpawnPoint.position , Quaternion.identity);
            newEnemy.gameManager = this;
            

            newEnemy.Initialize(this);

            newEnemy.Edge_Detected += OnScreenEdgeDetection;

            spawnEnemy = 1;
        }

    }



    // Observer Pattern Method tied to the edge detection broadcast from the enemy gameObject
    private void OnScreenEdgeDetection(float detected_edge)
    {
        Debug.Log(detected_edge);
        bool detected_edge_bool_conversion = false;


        

        if(detected_edge >= 1)
        {
            detected_edge_bool_conversion = true;
        }
        else if(detected_edge <= 0)
        {
            detected_edge_bool_conversion = false;
        }

        if(detected_edge_bool_conversion != edge_detection_invocation)
        {
            edge_detection_invocation = detected_edge_bool_conversion;
            MoveEnemyDown();
        }        
    }


    // Observer Pattern instructs Enemies to change direction and move down the screen
    public void MoveEnemyDown()
    {
        move_Enemy_Down?.Invoke();
    }



    void ChangeEnemyDirection()
    {


    }


    void SpawnNewEnemies()
    {

    }


    void RespawnPlayer()
    {

    }


    void GameOver()
    {

    }

}
