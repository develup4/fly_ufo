using UnityEngine;
using System.Collections;

public class WarningObstacle : MonoBehaviour {

    public GameObject planeObstacleRight;
    public GameObject planeObstacleLeft;
    public GameObject meteorObstacle;
    public GameObject planetObstacle;

    private GameObject gameManager;

    private Vector3 xyPosition;

    private int obstacleIndex;

    private float xDistance;
    private float yDistance;
    private float realTime;

    private int xPositionSelect;

	// Use this for initialization
	void Start () {
        realTime = 0.0f;

        gameManager = GameObject.Find("GameManager");

        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {

        realTime += Time.deltaTime;

        xyPosition.x = gameManager.transform.position.x + xDistance;
        xyPosition.y = gameManager.transform.position.y + yDistance;
        xyPosition.z = -2.0f;

        transform.position = xyPosition;

        if (realTime > 1.5f)
        {
            Destroy(gameObject);

            if (obstacleIndex == 1)
                makePlaneObstacle();
            else if (obstacleIndex == 2)
                makeMeteorObstacle();
            else if (obstacleIndex == 3)
                makePlanetObstacle();
        }
	}

    private void makePlaneObstacle()
    {
        if (xPositionSelect == -1)
            Instantiate(planeObstacleLeft, new Vector3(transform.position.x - 1.5f, transform.position.y, 0.0f), Quaternion.identity);
        else if(xPositionSelect == 1)
            Instantiate(planeObstacleRight, new Vector3(transform.position.x + 1.5f, transform.position.y, 0.0f), Quaternion.identity);
    }

    private void makeMeteorObstacle()
    {
        Instantiate(meteorObstacle, new Vector3(transform.position.x, transform.position.y + 1.0f, 0.0f), Quaternion.identity);
    }

    private void makePlanetObstacle()
    {
        if (xPositionSelect == -1)
            Instantiate(planetObstacle, new Vector3(transform.position.x - 1.0f, transform.position.y, 0.0f), Quaternion.identity);
        else if (xPositionSelect == 1)
            Instantiate(planetObstacle, new Vector3(transform.position.x + 1.0f, transform.position.y, 0.0f), Quaternion.identity);
    }

    public void setObstacleIndex(int obstacle)
    {
        this.obstacleIndex = obstacle;
    }

    public void setObstacleLeftRight(int leftRight)
    {
        this.xPositionSelect = leftRight;
    }

    public void setXYDistance(float xDistance, float yDistance)
    {
        this.xDistance = xDistance;
        this.yDistance = yDistance;
    }
}