using UnityEngine;
using System.Collections;

public class AchievementScore : MonoBehaviour
{
	public Sprite[] m_arrScore;

	public void SetScore(int nIndex)
	{
		this.GetComponent<SpriteRenderer>().sprite = m_arrScore[nIndex];
		this.GetComponent<SpriteRenderer>().enabled = true;
	}
}
