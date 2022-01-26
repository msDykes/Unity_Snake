using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeController : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    [SerializeField] UnityEvent eatEgg = null;
    [SerializeField] UnityEvent gameOver = null;

    Vector3 startPosition = Vector3.zero;
    SnakeBody body = null;
    float counter = 0;
    Quaternion rotation = new Quaternion();
    
    void Start()
    {
        startPosition = transform.position;
        body = gameObject.GetComponent<SnakeBody>();
        eatEgg.Invoke();
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

    public void setSpeed(float amount)
    {
        speed = amount;
    }

    public void resetPosition()
    {
        transform.position = startPosition;
        foreach (Segment segment in body.getSegments())
        {
            Destroy(segment.gameObject);
        }
        body.getSegments().Clear();
        body.buildSnake();
        transform.GetComponent<Rigidbody>().isKinematic = false;
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
        List<Segment> segments = body.getSegments();

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
            transform.GetComponent<Rigidbody>().isKinematic = true;
            gameOver.Invoke();
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Food")
        {
            body.addSegment();
            eatEgg.Invoke();
            transform.GetComponent<AudioSource>().Play();
        }
    }
}
