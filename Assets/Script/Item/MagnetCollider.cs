using UnityEngine;
using System.Collections;

public class MagnetCollider : MonoBehaviour {
    private float remainTime = 0.0f;
    private float basicMagnetForce;
    private float itemMagnetForce = 0.0f;
    private GameObject UFO;
    private const float UFO_position_Z = -1.0f;
    private bool isItemGet = false;

    void Awake()
    {
        UFO = GameObject.Find("UFO");
        basicMagnetForce = UFO.GetComponent<UFO_Attribute>().magnetForce;
    }

	void Update()
    {
        // 카메라 따라다니도록 처리
        Vector3 vecUFOPosition = UFO.transform.position;

        vecUFOPosition.x = UFO.transform.position.x;
        vecUFOPosition.y = UFO.transform.position.y;
        vecUFOPosition.z = UFO_position_Z;

        this.transform.position = vecUFOPosition;

        // 지속시간 감소 시키기
        if (isItemGet)
        {
            remainTime -= Time.deltaTime;
            if (remainTime < 0.0f)
            {
                isItemGet = false;
                end();
            }
        }
	}

    public void end()
    {
        GetComponent<CircleCollider2D>().radius = basicMagnetForce;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    public void setRemainTime(float remainTime)
    {
        this.remainTime = remainTime;
        isItemGet = true;
    }

    public void setItemMagnetForce(float magnetForce)
    {
        this.itemMagnetForce = magnetForce;
        GetComponent<CircleCollider2D>().radius = basicMagnetForce + itemMagnetForce;
    }
}
