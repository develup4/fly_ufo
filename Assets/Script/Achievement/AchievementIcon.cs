using UnityEngine;
using System.Collections;

public class AchievementIcon : MonoBehaviour
{
	public Sprite[] m_arrIcon;
	
	public void SetIcon(int nIndex)
	{
		this.GetComponent<SpriteRenderer>().sprite = m_arrIcon[nIndex];
		this.GetComponent<SpriteRenderer>().enabled = true;
	}
}
