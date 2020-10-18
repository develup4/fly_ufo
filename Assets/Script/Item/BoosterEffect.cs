using UnityEngine;
using System.Collections;

public class BoosterEffect : MonoBehaviour {

    public int destroyScore;

    private bool isItemGet = false;
    private float remainTime;
    private Result result;

    void Awake()
    {
        result = GameObject.Find("Result").GetComponent<Result>();
    }

    void Update()
    {
        if (isItemGet)
        {
            remainTime -= Time.deltaTime;
            if (remainTime < 0.0f)
            {
                GetComponent<UFO>().SetBoosterMode(false);
                GetComponent<UFO>().SetIgnoreObjectMode(true);
                GetComponent<UFO_Animation>().setAnimationState(UFO_Animation.CRUSHAFTER_STATE);
                
                Vector2 vecVelocity = this.rigidbody2D.velocity;
                vecVelocity.y = GetComponent<UFO_Attribute>().maxMoveSpeed;
                this.rigidbody2D.velocity = vecVelocity;

                isItemGet = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Obstacle") && isItemGet == true)
        {
            // 여기에 이펙트 소스 코드 추가
            result.score += destroyScore;
        }
    }

    public void setIsItemGet(bool tf)
    {
        isItemGet = tf;

        if (isItemGet)
        {
            GetComponent<UFO>().SetBoosterMode(true);
        }	
    }

    public void setRemainTime(float time)
    {
        if (isItemGet == false)
        {
            remainTime = time;
        }
    }
}
