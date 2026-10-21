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

        Debug.Log(viewPortPos);

        if(viewPortPos.y > 1)
        {
            Destroy(gameObject);
        }

    }
}
