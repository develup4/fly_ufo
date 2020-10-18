using UnityEngine;
using System.Collections;

public class LauncherAchievement : MonoBehaviour
{
	private bool 		m_bLeftEarCollide 	= false;
	private bool 		m_bRightEarCollide 	= false;
	private GameObject 	m_refGameResult;
	private GameObject	m_refUFO;

	void Start()
	{
		m_refGameResult = GameObject.Find("Result");
		m_refUFO 		= GameObject.Find ("UFO");
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (m_refUFO.GetComponent<UFO>().GetStartingPointDirecting() == false)
		{
			if (col.tag == "LeftEar")
			{
				m_bLeftEarCollide = true;
				if (m_bRightEarCollide)
				{
					if (m_refGameResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.MANIPULATE_RABBIT) == false)
					{
						m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.MANIPULATE_RABBIT);
					}
				}
			}
			else if (col.tag == "RightEar")
			{
				m_bRightEarCollide = true;
				if (m_bLeftEarCollide)
				{
					if (m_refGameResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.MANIPULATE_RABBIT) == false)
					{
						m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.MANIPULATE_RABBIT);
					}
				}
			}
		}
	}
}