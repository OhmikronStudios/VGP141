using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBot : MonoBehaviour, GameManager.NotifySink
{
    //Patrol and chase targets
    [SerializeField] Graph.Node target;
    GameObject player;
    Graph route;
    
    //Unit stats
    [SerializeField] float patrolSpeed = 10.0f;
    [SerializeField] float chaseRadius = 5.0f;
    [SerializeField] float chaseSpeed = 15.0f;
    [SerializeField] int health = 5;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] float projectileSpeed = 20.0f;
    [SerializeField] float fireRange = 3.0f;
    [SerializeField] bool loaded = true;

    //Alert variables
    [SerializeField] GameObject alertObj;
    [SerializeField] bool alert = false;

    //Unique spawn factors
    Color color;
    [SerializeField] GameObject body;
    [SerializeField] GameObject head;

    //Observer & Object Pool link
    GameManager gm;

    

    // Start is called before the first frame update
    void Start()
    {
        //Registers itself with the Gamemanager's subscription list
        gm = FindObjectOfType<GameManager>();
        gm.Register(this);
        player = FindObjectOfType<PlayerL5>().gameObject;

        color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255), 100);
        body.GetComponent<MeshRenderer>().material.color = color;
        head.GetComponent<MeshRenderer>().material.color = color;
        


        //Constructs it's patrol route randomly between all existing waypoints 
        List<GameObject> waypointList = new List<GameObject>();
        waypointList.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        route = new Graph(waypointList);
        
        //Starts with a randomly selected node in the patrol route
        target = route.RandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (alert || distToPlayer < chaseRadius)
        {
            transform.LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
            if (distToPlayer < fireRange && loaded)
            {
                StartCoroutine(Fire());
            }
        }
        else
        {
            transform.LookAt(target.m_position);
            transform.position = Vector3.MoveTowards(transform.position, target.m_position, patrolSpeed*Time.deltaTime);
            float distToTarget = Vector3.Distance(transform.position, target.m_position);
            if (distToTarget < 1.0f)
            {
                target = target.m_nextNode;
            }
        }

        //SceneMode allows you to view each patrolbot's route in it's own specific colour
        foreach (Graph.Node node in route.GetRoute())
        {
            Debug.DrawLine(node.m_position, node.m_nextNode.m_position, color);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            health -= 1;
            gm.Notify();

            if (health <= 0)
            {
                gm.NotifyDead(gameObject);
                //gameObject.SetActive(false);
                //Destroy(gameObject);
                //alive = false;
            }
        }
    }

    void GameManager.NotifySink.OnEvent( )
    {
        if (alert == false)
        {
            Debug.Log (name + " has been notified of an assault, beginning investigation");
            StartCoroutine(AlertMode());
        }
    }

    IEnumerator AlertMode()
    {
        alert = true;
        alertObj.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        alertObj.SetActive(false);
        alert = false;
    }

    public void Revive(Vector3 spawnPoint)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = spawnPoint;
        health = 5;
        alert = false;
        alertObj.SetActive(false);
    }
    IEnumerator Fire()
    {
        loaded = false;
        var forward = projectileSpawnPoint.transform.forward;
        GameObject bullet = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, Quaternion.LookRotation(forward));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = forward*projectileSpeed;
        Destroy(bullet, 3.0f);
        yield return new WaitForSeconds(3.0f);
        loaded = true;

    }

}
