using UnityEngine;

public class EnemyShootingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletVelocity = 3f;

    [SerializeField] private float playerIngageThreshold = 0.5f;

    [SerializeField] private float bulletCooldown = 3f;

    [SerializeField] private float nextShootTime = 0f;




    [SerializeField] private Rigidbody2D enemyRb;


    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LocatePlayer();
    }


    private void LocatePlayer()
    {
        //find the players coords and check if the enemy is within a specified range from the players x coordinates
        Vector2 playerCoords = GameManager.instance.ReturnPlayerCoordinates();
        Vector2 enemyCoords = enemyRb.position;


        if (Time.time >= nextShootTime)
        {
            if (playerCoords != new Vector2(0, 0))
            {
                Debug.Log(playerCoords);
                if (enemyCoords.x <= (playerCoords.x + playerIngageThreshold) && enemyCoords.x >= (playerCoords.x - playerIngageThreshold))
                {
                    //Debug.Log("Enemy within the range of the players x coordinates");
                    CheckCurrentEnemyBulletsCount();
                }
            }

            nextShootTime = Time.time + bulletCooldown;
        }

    }


    private void CheckCurrentEnemyBulletsCount()
    {
        // Check how many enemy bullets are in the scene before shooting

        bool isShootingPermitted = GameManager.instance.ReturnBoolAliveEnemyBullets();

        if (isShootingPermitted)
        {
            ShootEnemyBullet();
        }
    }


    private void ShootEnemyBullet()
    {

        // store enemy bullet count in gameManager , return a bool if the current number of enemy bullets is less  than the max enemy bullets        
        Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);


        //Play audio source of bullet firing , (make it a more aggresive sound so the player can differentiate between their bullets and the enemy bullets)

        AudioManager.instance.PlayLaserAudioClip();
        bullet.AddForce(bulletVelocity * -transform.up, ForceMode2D.Impulse);
        GameManager.instance.IncrementAliveEnemyBullets();

    }
}
