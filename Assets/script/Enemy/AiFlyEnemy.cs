using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AiFlyEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 400f;
    public float nextWaypoint = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachendofpath = false;


    [SerializeField] Seeker seeker;
    [SerializeField] Rigidbody2D rb2d;

    private void OnEnable()
    {
        target = PlayerManager.Instance.transform;
    }
    public void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb2d.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachendofpath = true;
            return;
        }    
        else
        {
            reachendofpath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb2d.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb2d.AddForce(force);
        float distance = Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypoint)
        {
            currentWaypoint++;
        }

        if(rb2d.velocity.x > 0)
            transform.eulerAngles = Vector3.zero;
        else if(rb2d.velocity.x < 0)
            transform.eulerAngles = Vector3.up * 180;
    }
}
