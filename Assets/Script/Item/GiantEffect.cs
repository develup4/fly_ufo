using UnityEngine;
using System.Collections;

public class GiantEffect : MonoBehaviour {
    public int destroyScore;

    private Result result;
    private bool isItemGet = false;
    private float remainTime;
    private float growthSize;
    private Transform UFO_Transform;
    private Vector3 originalScale;
    private Vector3 framePerGrowth;

    void Awake()
    {
        result = GameObject.Find("Result").GetComponent<Result>();
        UFO_Transform = GetComponent<Transform>();
        originalScale = transform.localScale;
        framePerGrowth = new Vector3(0.001f, 0.001f, 0.001f);
    }

    void Update()
    {
        if (isItemGet)
        {
            remainTime -= Time.deltaTime;

            if (remainTime > 4.5f && UFO_Transform.localScale.x < growthSize)
            {
                UFO_Transform.localScale += framePerGrowth;
            }
            else if (remainTime < 0.5f && UFO_Transform.localScale.x > originalScale.x)
            {
                UFO_Transform.localScale -= framePerGrowth;
            }
            else if (remainTime < 0.0f)
            {
                isItemGet = false;
                GetComponent<UFO>().SetIgnoreObjectMode(false);
                GetComponent<UFO>().SetGiantMode(false);
                GetComponent<UFO_Animation>().setAnimationState(UFO_Animation.CRUSHAFTER_STATE);
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
    }

    public void setRemainTime(float time)
    {
        if (isItemGet == false)
        {
            remainTime = time;
        }
    }

    public void setGrowthSize(float size)
    {
        growthSize = size;
    }
}
