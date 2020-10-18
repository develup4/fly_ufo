using UnityEngine;
using System.Collections;

public class PolymorphCollider : MonoBehaviour
{
    private bool isItemGet = false;
    private GameObject changeItem;
    private GameObject UFO;
    private float changeItemX;
    private float changeItemY;
    private float duration = 0.1f;
    private const float positionY = 6.4f;
	private GameObject m_refEffectManager;

    private bool isCrash = false;

    void Awake()
    {
        UFO = GameObject.Find("UFO");
		m_refEffectManager = GameObject.Find("EffectManager");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Obstacle") && isItemGet)
        {
			if(!col.GetComponent<Obstacle>().getPolymorphObstacle())
			{
		        col.GetComponent<PolymorphEffect>().setChangeItemSize(changeItemX, changeItemY);
		        col.GetComponent<PolymorphEffect>().polymorph(changeItem);

		        m_refEffectManager.GetComponent<EffectManager>().MakeEffect(11, col.transform.position);

		        if (!audio.isPlaying)
		            audio.Play();

		        col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y, -10.0f);
		        col.GetComponent<Obstacle>().setPolymorphObstacle(true);
			}
        }
    }

    void end()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        Vector3 vecUFOPosition = UFO.transform.position;

        if (UFO.GetComponent<UFO>().GetIsXTracking() == false)
        {
            vecUFOPosition.x = this.transform.position.x;
        }

        vecUFOPosition.y = UFO.transform.position.y + 3.6f;
        vecUFOPosition.z = positionY;
        this.transform.position = vecUFOPosition;

        if(isItemGet)
        {
            duration -= Time.deltaTime;

            if(duration < 0f)
            {
                duration = 0.1f;
                isItemGet = false;
                end();
            }
        }
    }

    public void setChangeItem(GameObject item)
    {
        changeItem = item;
    }

    public void setChangeItemSize(float width, float height)
    {
        this.changeItemX = width;
        this.changeItemY = height;
    }

    public float tempX()
    {
        return this.changeItemX;
    }

    public float tempY()
    {
        return this.changeItemY;
    }

    public void setIsItemGet(bool tf)
    {
        isItemGet = tf;
    }
}
