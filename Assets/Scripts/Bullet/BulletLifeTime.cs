using System;
using UnityEngine;

public class BulletLifeTime : MonoBehaviour
{

    //Handles life time of bullet



    void Start()
    {

    }

    //if the bullet exits from the top of the screen, destroy the game object
    void Update()
    {
        Vector3 viewPortPos = Camera.main.WorldToViewportPoint(transform.position);

        Debug.Log(gameObject.tag + " " + viewPortPos.y);
        if (viewPortPos.y > 1 || viewPortPos.y < 0)
        {
            DestroyGameObject();
        }

    }


    private void DestroyGameObject()
    {
        if (gameObject.CompareTag("EnemyBullet"))
        {
            GameManager.instance.DecrementAliveEnemyBullets();
        }
        Destroy(gameObject);
    }
}
