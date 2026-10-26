using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager instance { get; private set; }

    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private bool edge_detection_invocation = false;


    public event Action move_Enemy_Down;



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

    }



    // Observer Pattern Method tied to the edge detection broadcast from the enemy gameObject
    private void OnScreenEdgeDetection()
    {
        Debug.Log("Edge of Screen Reached");

        MoveEnemyDown();
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
