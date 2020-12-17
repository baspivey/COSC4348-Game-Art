using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_destroy : MonoBehaviour
{
    public Vector2 targetDistance = new Vector2(0, 0);
    private float distanceTraveled = 0;
    private float totalDistance;
    private Vector2 prevPosition = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        totalDistance = Vector2.Distance(transform.position, targetDistance);
        prevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += Vector2.Distance(transform.position, prevPosition);
        if (totalDistance - distanceTraveled < -0.1) {
            Destroy(gameObject);
        }
        prevPosition = transform.position;
    }

}
