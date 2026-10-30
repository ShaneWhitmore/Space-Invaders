using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletVelocity = 5f;

    [SerializeField] private int maxBulletCount = 2;



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (PlayerInput_Manager.instance.WasShootPressedThisFrame())
        {

            //First check if an instance of bullet already exists in the scene
            // if it doesnt then spawn a new bullet
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

            if (bullets.Length == maxBulletCount)
            {
                return;
            }
            else
            {
                Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);


                //Play audio source of bullet firing 

                AudioManager.instance.PlayLaserAudioClip();
                bullet.AddForce(bulletVelocity * transform.up, ForceMode2D.Impulse);

            }




        }
    }
}
