using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsetCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, Speed * Time.deltaTime);
    }
}
