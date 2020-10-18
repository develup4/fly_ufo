using UnityEngine;
using System.Collections;

public class UFO_Animation : MonoBehaviour {

    private int UFOState;

    public const int NORMAL_STATE = 0;
    public const int CRUSH_STATE = 1;
    public const int CRUSHAFTER_STATE = 2;

    private float[] animationTime = new float[3];

    private float realTime = 0.0f;

	// Use this for initialization
	void Start () {

        UFOState = NORMAL_STATE;

        animationTime[NORMAL_STATE] = GetComponent<SkeletonAnimation>().skeleton.data.FindAnimation("normal").Duration;
        animationTime[CRUSH_STATE] = GetComponent<SkeletonAnimation>().skeleton.data.FindAnimation("crash").Duration;
        animationTime[CRUSHAFTER_STATE] = GetComponent<SkeletonAnimation>().skeleton.data.FindAnimation("crash after").Duration;
	}
	
	// Update is called once per frame
	void Update () {
        checkAnimationState();
	}

    private void checkAnimationState()
    {
        switch (UFOState)
        {
            case NORMAL_STATE:
                GetComponent<SkeletonAnimation>().animationName = "normal";
                GetComponent<SkeletonAnimation>().loop = true;
                break;

            case CRUSH_STATE:
                GetComponent<SkeletonAnimation>().animationName = "crash";
                GetComponent<SkeletonAnimation>().loop = true;

                realTime += Time.deltaTime;

                if (realTime > animationTime[CRUSH_STATE])
                {
                    UFOState = CRUSHAFTER_STATE;
                    realTime = 0.0f;
                }
                break;

            case CRUSHAFTER_STATE:
                GetComponent<SkeletonAnimation>().animationName = "crash after";
                GetComponent<SkeletonAnimation>().loop = true;

                realTime += Time.deltaTime;

                if (realTime > animationTime[CRUSHAFTER_STATE] * 3)
                {
                    UFOState = NORMAL_STATE;
                    realTime = 0.0f;
                }
                break;
        }
    }

    public int getAnimationState() { 
        return UFOState; 
    }

    public void setAnimationState(int state) {
        if (GetComponent<UFO>().GetIsBoosterMode() || GetComponent<UFO>().GetIsGiantMode())
        {
            UFOState = NORMAL_STATE;
            return;
        }
        
        UFOState = state;
    }
}
