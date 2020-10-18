using UnityEngine;
using System.Collections;

public class MovingObstaclePlane : MonoBehaviour
{
    public AudioClip hitSound;
    public AudioClip crashSound;

    public float speed;

    private GameObject ufo;
    private GameObject gameManager;

    private Vector3 ufoPosition;
    private Vector2 dirVec;
    private Vector3 dirVec1;

	private GameObject m_refResult;

	void Start()
    {
		m_refResult = GameObject.Find("Result");
        ufo = GameObject.Find("UFO");
        gameManager = GameObject.Find("GameManager");

        ufoPosition = ufo.transform.position;
        
        if (ufoPosition.x - transform.position.x < 0)
            dirVec = Vector2.right * -1;
        else
            dirVec = Vector2.right * 1;
        
	}

    void Update()
    {
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
                // 업적 체크
                if (m_refResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.FIRST_COLLIDE_AIRPLANE_ACHIEVEMENT_NUMBER) == false)
                {
                    m_refResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.FIRST_COLLIDE_AIRPLANE_ACHIEVEMENT_NUMBER);
                }

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

                    dirVec1 = transform.position - col.transform.position;
                    dirVec.Normalize();
                    GetComponent<Obstacle>().setIsCrash(true);
                    GetComponentInChildren<CrashObstacle>().setIsCrash(true);
                }
            }
        }
    }
}
