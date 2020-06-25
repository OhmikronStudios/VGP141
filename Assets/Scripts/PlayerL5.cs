using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerL5 : MonoBehaviour
{
    [SerializeField] float thrust = 15.0f;
    [SerializeField] float rotSpeed = 55.0f;
    GameManager gm;


    [SerializeField] GameObject bSpawner;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 20.0f;
    
    
    [SerializeField] int health = 10;



    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Notify();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }


    }

    private void Move()
    {
        float moveSpeed = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * thrust * moveSpeed * Time.deltaTime); 
        float strafeSpeed = Input.GetAxisRaw("Strafe");
        transform.Translate(Vector3.right * thrust * strafeSpeed * Time.deltaTime);
        float sideSpeed = Input.GetAxisRaw("Horizontal");
        transform.Rotate(Vector3.up * rotSpeed * sideSpeed * Time.deltaTime);
 
    }

    private void Fire()
    {   
        var forward = bSpawner.transform.forward;
        GameObject bullet = Instantiate(bulletPrefab, bSpawner.transform.position, Quaternion.LookRotation(forward));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(forward * bulletSpeed);
        Destroy(bullet, 3.0f);
    }

    public void Notify()
    {
        gm.Notify();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            health -= 1;
            
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

}
