using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject snakePrefab = null;
    [SerializeField] GameObject eggPrefab = null;
    [SerializeField] float xLeftBoundary = -15;
    [SerializeField] float xRightBoundary = 15;
    [SerializeField] float zTopBoundary = 15;
    [SerializeField] float zBottomBoundary = -15;

    GameObject currentSnake = null;
    GameObject currentEgg = null;

    void Update() 
    {
        if (currentSnake != null)
        {
            PlayerController snake = currentSnake.GetComponentInChildren<PlayerController>();
            if (snake.haveEaten())
            {
                snake.hungry();
                spawnEgg();
            }
        }
    }

    public void spawnSnake()
    {
        if (currentSnake != null)
        {
            Destroy(currentSnake);
        }

        currentSnake = Instantiate(snakePrefab, transform);
        spawnEgg();
    }

    public void spawnEgg()
    {
        if (currentEgg != null)
        {
            print("Destroying egg...");
            Destroy(currentEgg);
        }

        bool occupied = true;
        float xPosition = 0f;
        float zPosition = 0f;

        while (occupied)
        {
            xPosition = UnityEngine.Random.Range(xLeftBoundary, xRightBoundary);
            zPosition = UnityEngine.Random.Range(zBottomBoundary, zTopBoundary);

            xPosition = Mathf.Round(xPosition);
            zPosition = Mathf.Round(zPosition);

            foreach (Segment segment in currentSnake.GetComponentInChildren<SegmentSpawner>().getSegments())
            {
                if (segment.transform.position.x != xPosition || segment.transform.position.z != zPosition)
                {
                    occupied = false;
                }
            }
        }

        Vector3 position = new Vector3(xPosition, transform.position.y, zPosition);
        currentEgg = Instantiate(eggPrefab, transform.parent);
    }

}
