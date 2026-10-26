using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    //  handle moving the enemy left and right on the screen
    // also handles moving the enemy closer to the player 

    // (observer pattern ) broadcast if the enemy reaches the edge of the screen and which edge that is (Enemy Manager will subscribe to this )

    [SerializeField] private Rigidbody2D enemy_rb;
    [SerializeField] private bool isAlive;

    void Start()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            
        }
    }


    private void OnTriggerEnter2D(Collider2D colission)
    {

        Debug.Log(colission.name);

        if(colission.CompareTag("Bullet"))
        {
            Debug.Log("Enemy was hit");
            Destroy(colission.gameObject);

            isAlive = false;
        }

        Destroy(gameObject);
    }
}
