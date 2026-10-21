using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletVelocity = 5f;



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
        if(PlayerInput_Manager.instance.WasShootPressedThisFrame())
        {
            Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawner.position , Quaternion.identity);

            bullet.AddForce(bulletVelocity *transform.up, ForceMode2D.Impulse);
            
        }
    }
}
