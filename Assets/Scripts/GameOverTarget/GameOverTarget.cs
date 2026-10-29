using System;
using UnityEngine;

public class GameOverTarget : MonoBehaviour
{

    public event Action enemyEnteredTarget;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy entered collider");
            enemyEnteredTarget?.Invoke();
        }
    }
}
