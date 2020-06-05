using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerL5 : MonoBehaviour
{
    [SerializeField] float thrust = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();    
    }

    private void Move()
    {
        float moveSpeed = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * thrust * moveSpeed * Time.deltaTime);
        float sideSpeed = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * thrust * sideSpeed * Time.deltaTime);
    }
}
