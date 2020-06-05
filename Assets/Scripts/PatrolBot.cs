using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBot : MonoBehaviour
{
    [SerializeField] Graph.Node target;
    [SerializeField] GameObject player;
    [SerializeField] Color color;
    [SerializeField] float patrolSpeed = 10.0f;
    [SerializeField] float chaseRadius = 5.0f;
    [SerializeField] float chaseSpeed = 15.0f;
    Graph route; 

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> waypointList = new List<GameObject>();
        waypointList.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        route = new Graph(waypointList);
        

        target = route.RandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distToPlayer < chaseRadius)
        {
            transform.LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
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

        foreach (Graph.Node node in route.GetRoute())
        {
            Debug.DrawLine(node.m_position, node.m_nextNode.m_position, color);
        }

    }
    



}
