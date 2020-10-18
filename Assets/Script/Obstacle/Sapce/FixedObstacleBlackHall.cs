using UnityEngine;
using System.Collections;

public class FixedObstacleBlackHall : MonoBehaviour {

    private GameObject ufo;
    private GameObject gameManager;

    private bool isInHale;

    private float initInHaleDistance;
    private float inHaleDistance;

    private Vector2 inHaleDirVec;

	// Use this for initialization
	void Start () {

        ufo = GameObject.Find("UFO");
        gameManager = GameObject.Find("GameManager");

        isInHale = false;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.CHANGE_STAGE)
        {
            if (col.tag == "Player")
            {
                if (col.gameObject.GetComponent<UFO_Animation>().getAnimationState() != UFO_Animation.CRUSHAFTER_STATE)
                {
                    if (!isInHale)
                    {
                        initInHaleDistance = Vector2.Distance(transform.position, ufo.transform.position);
                        isInHale = true;
                    }

                    inHaleDirVec = transform.position - ufo.transform.position;

                    inHaleDirVec.Normalize();

                    inHaleDistance = Vector2.Distance(transform.position, ufo.transform.position);

                    ufo.rigidbody2D.AddForce(inHaleDirVec * (initInHaleDistance - inHaleDistance) * 10.0f);
                }
            }
        }
    }
}
