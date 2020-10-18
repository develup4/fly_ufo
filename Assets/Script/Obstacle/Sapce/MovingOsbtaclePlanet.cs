using UnityEngine;
using System.Collections;

public class MovingOsbtaclePlanet : MonoBehaviour {

    public AudioClip hitSound;
    public AudioClip crashSound;

    private GameObject ufo;
    private GameObject gameManager;

    private Vector2 ufoPosition;
    private Vector2 planetPosition;
    private Vector2 dirVec1;

    private float angle;
    private float length;

    private bool left;

    private bool isCrash;

	// Use this for initialization
	void Start () {
	    ufo = GameObject.Find("UFO");
        gameManager = GameObject.Find("GameManager");

        ufoPosition = ufo.transform.position;

        length = Vector2.Distance(ufo.transform.position, transform.position);

        if (ufoPosition.x - transform.position.x < 0)
            left = false;
        else
            left = true;

        angle = Mathf.Atan((transform.position.x - ufoPosition.x) / (transform.position.y - ufoPosition.y));

        isCrash = false;
	}
	
	// Update is called once per frame
	void Update () {

        if ((gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.CHANGE_STAGE) ||
		    (gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.FEVER_STAGE))
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
				{
					if (!left)
					{
						planetPosition.x = ufoPosition.x + length * Mathf.Cos(angle);
						planetPosition.y = ufoPosition.y + length * Mathf.Sin(angle);
					}
					else
					{
						planetPosition.x = ufoPosition.x + length * Mathf.Sin(angle);
						planetPosition.y = ufoPosition.y + length * Mathf.Cos(angle);
					}

					angle += 0.005f;
					
					transform.position = planetPosition;
				}
			}
			
			if(Mathf.Abs(ufo.transform.position.y - transform.position.y) > 38.4f)
				Destroy(gameObject);
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
