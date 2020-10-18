using UnityEngine;
using System.Collections;

public class EngineAnimation : MonoBehaviour
{
    private int         m_nEngineAnimationState;

    public const int    ENGINE_OFF          = 0;
    public const int    ENGINE_ON           = 1;
    public const int    ENGINE_TURNON       = 2;
    public const int    ENGINE_TURNOFF      = 3;
    public const int    ANIMATION_STATE_NUM = 4;

    private float[]     m_arrAnimationTime = new float[ANIMATION_STATE_NUM];
    private float       m_fRealTime         = 0.0f;

	void Start()
    {
        m_nEngineAnimationState = ENGINE_OFF;

        m_arrAnimationTime[ENGINE_OFF]      = GetComponent<SkeletonAnimation>().skeleton.data.FindAnimation("animationoff").Duration;
        m_arrAnimationTime[ENGINE_ON]       = GetComponent<SkeletonAnimation>().skeleton.data.FindAnimation("animationon").Duration;
        m_arrAnimationTime[ENGINE_TURNON]   = GetComponent<SkeletonAnimation>().skeleton.data.FindAnimation("turnon").Duration;
        m_arrAnimationTime[ENGINE_TURNOFF]  = GetComponent<SkeletonAnimation>().skeleton.data.FindAnimation("turnoff").Duration;
	}
	
	void Update()
    {
        CheckAnimationState();
	}

    private void CheckAnimationState()
    {
        switch (m_nEngineAnimationState)
        {
            case ENGINE_OFF:
                GetComponent<SkeletonAnimation>().animationName = "animationoff";
                GetComponent<SkeletonAnimation>().loop = true;
                break;

            case ENGINE_ON:
                GetComponent<SkeletonAnimation>().animationName = "animationon";
                GetComponent<SkeletonAnimation>().loop = true;
                break;

            case ENGINE_TURNON:
                GetComponent<SkeletonAnimation>().animationName = "turnon";
                GetComponent<SkeletonAnimation>().loop = true;

                m_fRealTime += Time.deltaTime;

                if (m_fRealTime > m_arrAnimationTime[ENGINE_TURNON])
                {
                    m_nEngineAnimationState = ENGINE_ON;
                    m_fRealTime = 0.0f;
                }
                break;

            case ENGINE_TURNOFF:
                GetComponent<SkeletonAnimation>().animationName = "turnoff";
                GetComponent<SkeletonAnimation>().loop = true;

                m_fRealTime += Time.deltaTime;

                if (m_fRealTime > m_arrAnimationTime[ENGINE_TURNOFF])
                {
                    m_nEngineAnimationState = ENGINE_OFF;
                    m_fRealTime = 0.0f;
                }
                break;
        }
    }

    public void EngineOn(bool bOn)
    {
        if (bOn)
        {
            if (m_nEngineAnimationState == ENGINE_OFF)
            {
                m_nEngineAnimationState = ENGINE_TURNON;
            }
        } 
        else
        {
            if (m_nEngineAnimationState == ENGINE_ON)
            {
                m_nEngineAnimationState = ENGINE_TURNOFF;
            }
        }
    }

    public int GetAnimationState()
    {
        return m_nEngineAnimationState; 
    }

    public void SetAnimationState(int nState)
    {
        m_nEngineAnimationState = nState;
    }
}
