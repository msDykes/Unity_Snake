using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] SnakeController snake = null;
    [SerializeField] GameObject egg = null;
    [SerializeField] float xLeftBoundary = -14;
    [SerializeField] float xRightBoundary = 14;
    [SerializeField] float zTopBoundary = 14;
    [SerializeField] float zBottomBoundary = -14;

    public void moveEgg()
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

            occupied = false;
            foreach (Segment segment in snake.GetComponent<SnakeBody>().getSegments())
            {
                if (segment.transform.position.x == xPosition && segment.transform.position.z == zPosition)
                {
                    occupied = true;
                }
            }
        }

        egg.transform.position = new Vector3(xPosition, egg.transform.position.y, zPosition);
    }

}
