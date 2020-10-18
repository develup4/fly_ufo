using UnityEngine;
using System.Collections;

public class MovingObstacleMeteor : MonoBehaviour {

    public AudioClip hitSound;
    public AudioClip crashSound;

    public float speed;

    private GameObject ufo;
    private GameObject gameManager;

    private Vector2 dirVec;
    private Vector3 dirVec1;

    private float leftright;

	// Use this for initialization
	void Start () {
        ufo = GameObject.Find("UFO");
        gameManager = GameObject.Find("GameManager");

        dirVec = ufo.transform.position - transform.position;
        dirVec.Normalize();

        leftright = ufo.transform.position.x - transform.position.x;

        if (leftright < 0.0f)
            GetComponentInChildren<MeteorRotate>().setLeftRight(-1);
        else if (leftright > 0.0f)
            GetComponentInChildren<MeteorRotate>().setLeftRight(1);
        else
            GetComponentInChildren<MeteorRotate>().setLeftRight(0);

        GetComponentInChildren<MeteorRotate>().setRotate(dirVec);
	}
	
	// Update is called once per frame
	void Update () {
		if ((gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.CHANGE_STAGE) ||
		    (gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.FEVER_STAGE))
		{
	        if (gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.CHANGE_STAGE)
	        {
	            if (GetComponent<Obstacle>().getIsCrash())
	            {
					if(transform.position.x > -15.0f && transform.position.x < 15.0f)
	                	transform.Translate(dirVec1 * 10.0f * Time.deltaTime);
					else
						Destroy(gameObject);
	            }

	            else
	            {
					if(!GetComponent<Obstacle>().getPolymorphObstacle())
	                	transform.Translate(dirVec * speed * Time.deltaTime);
	            }

				if(Mathf.Abs(ufo.transform.position.y - transform.position.y) > 38.4f)
					Destroy(gameObject);
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

                    col.gameObject.GetComponent<UFO>().CollideObject(true, false);
                    col.gameObject.GetComponent<UFO_Animation>().setAnimationState(UFO_Animation.CRUSH_STATE);
                }

                if (col.gameObject.GetComponent<UFO>().GetIsGiantMode() || col.gameObject.GetComponent<UFO>().GetIsBoosterMode())
                {
                    if (!audio.isPlaying)
                    {
                        audio.clip = crashSound;
                        audio.Play();
                    }

                    dirVec1 = transform.position - col.transform.position;
                    dirVec1.Normalize();
                    GetComponent<Obstacle>().setIsCrash(true);
                    GetComponentInChildren<CrashObstacle>().setIsCrash(true);
                }
            }
        }
    }
}
