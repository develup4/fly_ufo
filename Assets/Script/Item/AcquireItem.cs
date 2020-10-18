using UnityEngine;
using System.Collections;

public class AcquireItem : MonoBehaviour {

    public int 			score;
    public int 			money;

    private Result 		result;
	private GameObject	m_refEffectManager;
    private GameObject  soundManager;
    private GameObject  ufo;

	public int			EffectNumber;

    private bool        isCrash = false;

    private Vector3     originalPosition;
    private bool        polymorph = false;

    private float       realTime = 0.0f;

    void Awake()
    {
        result = GameObject.Find("Result").GetComponent<Result>();
		m_refEffectManager = GameObject.Find ("EffectManager");
        soundManager = GameObject.Find("SoundManager");
        ufo = GameObject.Find("UFO");

        originalPosition = transform.position;
    }

    void Update()
    {
        if (polymorph)
        {
            if (ufo.transform.position.y - transform.position.y > 38.4f)
            {
                Destroy(gameObject);
            }
        }
    }

	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.tag.Equals("Player"))
        {
            Vector3 triggerEnterPosition;

            if (!isCrash)
            {
                result.score += score;
                result.money += money;
                result.renewScore();

                m_refEffectManager.GetComponent<EffectManager>().MakeEffect(EffectNumber, this.transform.position);

                if (!audio.isPlaying)
                {
                    audio.Play();
                }

                triggerEnterPosition = transform.position;
                triggerEnterPosition.z = -10.0f;

                transform.position = triggerEnterPosition;

                isCrash = true;
            }

            if (polymorph)
            {
                realTime = Time.deltaTime;

                if (realTime > audio.clip.length)
                {
                    Destroy(gameObject);
                }
            }
        }
	}

    public void setOriginalPosition()
    {
        transform.position = originalPosition;
    }

    public void setIsCrash(bool crash)
    {
        this.isCrash = crash;
    }

    public bool getIsCrash()
    {
        return this.isCrash;
    }

    public void setPolymorphItem(bool polymorph)
    {
        this.polymorph = polymorph;
    }
}
