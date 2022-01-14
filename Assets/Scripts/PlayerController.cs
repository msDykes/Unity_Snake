using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1;

    SegmentSpawner spawner = null;
    float counter = 0;
    Quaternion rotation = new Quaternion();
    
    void Start()
    {
        spawner = this.gameObject.GetComponent<SegmentSpawner>();
    }
    
    void Update()
    {
        counter += Time.deltaTime;
        if (speed > 0)
        {
            processInput();
            if (counter > 1/speed)
            {
                counter = 0;
                transform.rotation = rotation;
                moveSnake();
            }
        }
    }

    void processInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && transform.rotation.eulerAngles.y != 270)
        {
            rotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.rotation.eulerAngles.y != 90)
        {
            rotation = Quaternion.Euler(0, -90, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.rotation.eulerAngles.y != 180)
        {
            rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && transform.rotation.eulerAngles.y != 0)
        {
            rotation = Quaternion.Euler(0, 180, 0);
        }    
    }

    void moveSnake()
    {
        List<Segment> segments = spawner.getSegments();

        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].transform.rotation = segments[i - 1].transform.rotation;
            segments[i].transform.position = segments[i - 1].transform.position;
        }
        segments[0].transform.rotation = transform.rotation;
        segments[0].transform.position = transform.position;

        transform.Translate(Vector3.forward);
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Snake")
        {
            speed = 0;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Food")
        {
            spawner.addSegment();
            Destroy(other.gameObject);
        }
        
    }
}
