using UnityEngine;
using System.Collections;

public class FixedObstacleAirBalloon : MonoBehaviour {

    public AudioClip hitSound;
    public AudioClip crashSound;

    private GameObject ufo;
    private GameObject gameManager;

    public float speed;

    private Vector2 dirVec;

    // Use this for initialization
    void Start()
    {
        ufo = GameObject.Find("UFO");
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
		if ((gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.CHANGE_STAGE) ||
		    (gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.FEVER_STAGE))
		{
	        // 충돌했을때
	        if (GetComponent<Obstacle> ().getIsCrash ()) 
			{
				if (transform.position.x < 13.0f && transform.position.x > -13.0f)
						transform.Translate (dirVec * 10.0f * Time.deltaTime);
			}

			else {
				if(!GetComponent<Obstacle>().getPolymorphObstacle())
				{
					if (Mathf.Abs (ufo.transform.position.y - transform.position.y) < 12.8f)
						transform.Translate (Vector2.up * speed * Time.deltaTime);
				}
			}
		}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (!GetComponent<Obstacle>().getPolymorphObstacle())
            {
                if (col.gameObject.GetComponent<UFO_Animation>().getAnimationState() == UFO_Animation.NORMAL_STATE)
                {
                    if (!audio.isPlaying)
                    {
                        audio.clip = hitSound;
                        audio.Play();
                    }

                    col.gameObject.GetComponent<UFO>().CollideObject(false, false);
                    col.gameObject.GetComponent<UFO_Animation>().setAnimationState(UFO_Animation.CRUSH_STATE);
                }

                if (col.gameObject.GetComponent<UFO>().GetIsGiantMode() || col.gameObject.GetComponent<UFO>().GetIsBoosterMode())
                {
                    if (!audio.isPlaying)
                    {
                        audio.clip = crashSound;
                        audio.Play();
                    }

                    dirVec = transform.position - col.transform.position;
                    dirVec.Normalize();
                    GetComponent<Obstacle>().setIsCrash(true);
                    GetComponentInChildren<CrashObstacle>().setIsCrash(true);
                }
            }
        }
    }
}
