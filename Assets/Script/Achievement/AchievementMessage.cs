using UnityEngine;
using System.Collections;

public class AchievementMessage : MonoBehaviour
{
	private float			m_fToastTime 	= 0.0f;
	private bool			m_bToastOn		= false;

	private GameObject		m_refMeesage;
	private GameObject		m_refIcon;
	private GameObject		m_refScore;
	private GameObject		m_refUIBox;

	private const float     TOAST_TIME	    = 2.1f;

	void Start()
	{
		m_refMeesage 	= GameObject.Find ("Message");
		m_refIcon 		= GameObject.Find ("Icon");
		m_refScore	 	= GameObject.Find ("Score");
		m_refUIBox	 	= GameObject.Find ("UIBox");
	}

	void Update()
	{
		if (m_bToastOn)
		{
			m_fToastTime += Time.deltaTime;

			if (m_fToastTime > TOAST_TIME)
			{
				m_refMeesage.GetComponent<SpriteRenderer>().enabled = false;
				m_refIcon.GetComponent<SpriteRenderer>().enabled = false;
				m_refScore.GetComponent<SpriteRenderer>().enabled = false;
				m_refUIBox.GetComponent<SpriteRenderer>().enabled = false;

				m_fToastTime = 0.0f;
				m_bToastOn = false;
			}
		}
	}

	public void ShowMessage(int nIndex, int nGrade)
	{
		// 토스트 표시
		m_bToastOn = true;

		m_refMeesage.GetComponent<Message>().SetMessage(nIndex);
		m_refIcon.GetComponent<AchievementIcon> ().SetIcon(nGrade - 1);
		m_refScore.GetComponent<AchievementScore> ().SetScore(nGrade - 1);
		m_refUIBox.GetComponent<SpriteRenderer>().enabled = true;
	}
}