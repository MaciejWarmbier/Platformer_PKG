using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float distance = 0.5f;
    [SerializeField] bool isMovingHorizontaly = true;
    Vector3 startingPosition;
    
    private void Start() {
        startingPosition = transform.position;
    }
    void Update()
    {
        if (isMovingHorizontaly)
        {
            Vector3 mov = new Vector3( Mathf.Sin(speed * Time.time) * distance,0, 0);
            transform.position  = startingPosition + mov;
        }
        else
        {
            Vector3 mov = new Vector3(0, Mathf.Sin(speed * Time.time) * distance, 0);
            transform.position  = startingPosition + mov;
        }
    }
}