using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    [SerializeField] float xLeftBoundary = -15;
    [SerializeField] float xRightBoundary = 15;
    [SerializeField] float zTopBoundary = 15;
    [SerializeField] float zBottomBoundary = -15;

    public void spawnRandom(List<Segment> segments)
    {
        bool occupied = true;
        float xPosition = 0f;
        float zPosition = 0f;

        while (occupied)
        {
            xPosition = Random.Range(xLeftBoundary, xRightBoundary);
            zPosition = Random.Range(zBottomBoundary, zTopBoundary);

            xPosition = Mathf.Round(xPosition);
            zPosition = Mathf.Round(zPosition);

            foreach (Segment segment in segments)
            {
                if (segment.transform.position.x != xPosition || segment.transform.position.z != zPosition)
                {
                    occupied = false;
                }
            }
        }

        transform.position = new Vector3(xPosition, transform.position.y, zPosition);
    }

}
