using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    [SerializeField] Segment segmentPrefab = null;

    List<Segment> segments = new List<Segment>();

    void Awake() 
    {
        buildSnake();        
    }

    public void addSegment()
    {
        Segment lastSegment = segments[segments.Count - 1];
        Vector3 position = lastSegment.transform.position;

        if ((int)lastSegment.transform.rotation.y == 0)
        {            
            position.z = position.z - 1;
        }
        else if ((int)lastSegment.transform.rotation.y == 180)
        {
            position.z = position.z + 1;
        }
        else if ((int)lastSegment.transform.rotation.y == 90)
        {
            position.x = position.x + 1;
        }
        else if ((int)lastSegment.transform.rotation.y == -90)
        {
            position.z = position.x - 1;
        }

        Segment newSegment = Instantiate(segmentPrefab, Vector3.zero, Quaternion.identity);
        newSegment.transform.parent = transform.parent;
        newSegment.transform.rotation = lastSegment.transform.rotation;
        newSegment.transform.position = lastSegment.transform.position;
        segments.Add(newSegment);

        for (int i = 4; i > 0; i--)
        {
            float scale = (float)i / 4;
            segments[segments.Count - i].transform.localScale = new Vector3(scale, scale, scale);            
        }
    }

    public List<Segment> getSegments()
    {
        return segments;
    }

    public void buildSnake()
    {
        Vector3 startPosition = transform.position;
        startPosition.z -= 1;

        for (int i = 4; i > 0; i--)
        {
            Segment segment = Instantiate(segmentPrefab, startPosition, transform.rotation);
            float scale = (float)i / 4;
            segment.transform.localScale = new Vector3(scale, scale, scale);
            segment.transform.parent = transform.parent;
            segments.Add(segment);                
            startPosition.z -= 1;              
        }
    }
}
