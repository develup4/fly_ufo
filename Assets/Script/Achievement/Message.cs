using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour
{
	public Sprite[] m_arrMessageImage;

	public void SetMessage(int nIndex)
	{
		this.GetComponent<SpriteRenderer>().sprite = m_arrMessageImage[nIndex];
		this.GetComponent<SpriteRenderer>().enabled = true;
	}
}
