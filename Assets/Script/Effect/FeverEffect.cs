using UnityEngine;
using System.Collections;

public class FeverEffect : MonoBehaviour {

    private GameObject ufo;
    private GameObject gameManager;

    private float animationDuration;
    private float realTime;

    private Vector3 effectPosition;

    private bool isFeverEffect;

	// Use this for initialization
	void Start () {

        ufo = GameObject.Find("UFO");
        gameManager = GameObject.Find("GameManager");

        animationDuration = GetComponent<SkeletonAnimation>().skeleton.data.FindAnimation("animation").Duration;

        isFeverEffect = false;
	}
	
	// Update is called once per frame
	void Update () {

        effectPosition = ufo.transform.position;
        effectPosition.z = -3.0f;

        transform.position = effectPosition;

        realTime += Time.deltaTime;

        if (!isFeverEffect)
        {
            if (realTime > animationDuration + 0.1f)
            {
                if (gameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.CHANGE_STAGE)
                {
                    Vector3 feverPosition = ufo.transform.position;

                    feverPosition.x = gameManager.transform.position.x;
                    feverPosition.y = -5000.0f;

                    gameManager.GetComponent<MapControlManager>().saveFeverXPosition(feverPosition.x);

                    ufo.transform.position = feverPosition;
                    ufo.rigidbody2D.gravityScale = 1.0f;
                    ufo.GetComponent<UFO>().SetControllable(true);

                    gameManager.GetComponent<StageChange>().feverChange();
                }

                else if(gameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.FEVER_STAGE)
                {
                    ufo.GetComponent<UFO_Animation>().setAnimationState(UFO_Animation.CRUSHAFTER_STATE);
                    ufo.GetComponent<UFO>().SetIgnoreObjectMode(true);
                    ufo.transform.position = gameManager.GetComponent<MapControlManager>().getUfoPrevPosition();
                    ufo.rigidbody2D.velocity = new Vector2(0.0f, 25.0f);

                    ufo.rigidbody2D.gravityScale = 1.0f;
                    ufo.GetComponent<UFO>().SetControllable(true);

                    gameManager.GetComponent<StageChange>().prevStage(gameManager.GetComponent<MapControlManager>().getPrevStage());
                }

                isFeverEffect = true;
            }
        }
        else
        {
            if (realTime > animationDuration + 0.2f)
            {
                Destroy(gameObject);
            }
        }
	}
}
