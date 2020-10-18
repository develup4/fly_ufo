using UnityEngine;
using System.Collections;

public class CharacterSelectManager : MonoBehaviour
{
	public static int g_UFONumber = 0;

	public GameObject[] TAB_IMAGE;
	public GameObject[] TAB_PUSH_IMAGE;
	public GameObject PART_PANEL;
	public GameObject CHARACTER;

	public SkeletonDataAsset[] ARRAY_SKELETON;

	private int m_nTab = 0;
	private int m_nCharacterIndex = 0;
	private int m_nBodyIndex = 0;

	void Update()
	{

		for (int i = 0; i < 3; i++)
		{
			if (m_nTab != i)
			{
				TAB_PUSH_IMAGE[i].GetComponentInChildren<UISprite>().enabled = true;
			}
			else
			{
				TAB_PUSH_IMAGE[i].GetComponentInChildren<UISprite>().enabled = false;
			}
		}
		for (int i = 0; i < 3; i++)
		{
			if (m_nTab != i)
			{
				TAB_IMAGE[i].GetComponentInChildren<UISprite>().enabled = false;
			}
			else
			{
				TAB_IMAGE[i].GetComponentInChildren<UISprite>().enabled = true;
			}
		}

		if (m_nTab == 0)
		{
			switch(m_nCharacterIndex)
			{
			case 0:
				PART_PANEL.GetComponent<UISprite>().spriteName = "alpaca";
				break;
			case 1:
				PART_PANEL.GetComponent<UISprite>().spriteName = "cat";
				break;
			case 2:
				PART_PANEL.GetComponent<UISprite>().spriteName = "dog";
				break;
			case 3:
				PART_PANEL.GetComponent<UISprite>().spriteName = "owl";
				break;
			case 4:
				PART_PANEL.GetComponent<UISprite>().spriteName = "panda";
				break;
			case 5:
				PART_PANEL.GetComponent<UISprite>().spriteName = "penguin";
				break;
			}
		}
		else if(m_nTab == 1)
		{
			switch(m_nBodyIndex)
			{
			case 0:
				PART_PANEL.GetComponent<UISprite>().spriteName = "body_alpaca";
				break;
			case 1:
				PART_PANEL.GetComponent<UISprite>().spriteName = "body_cat";
				break;
			case 2:
				PART_PANEL.GetComponent<UISprite>().spriteName = "body_dog";
				break;
			case 3:
				PART_PANEL.GetComponent<UISprite>().spriteName = "body_owl";
				break;
			case 4:
				PART_PANEL.GetComponent<UISprite>().spriteName = "body_panda";
				break;
			case 5:
				PART_PANEL.GetComponent<UISprite>().spriteName = "body_penguin";
				break;
			}
		}
		else if(m_nTab == 2)
		{
			PART_PANEL.GetComponent<UISprite>().spriteName = "store_background_engine";
		}
	}

	public void CharacterTabClick()
	{
		m_nTab = 0;
	}

	public void BodyTabClick()
	{
		m_nTab = 1;
	}

	public void EngineTabClick()
	{
		m_nTab = 2;
	}

	public void RightButtonClick()
	{
		if (m_nTab == 0)
		{
			m_nCharacterIndex++;
			if (m_nCharacterIndex > 5) m_nCharacterIndex = 5;
		}
		else if(m_nTab == 1)
		{
			m_nBodyIndex++;
			if (m_nBodyIndex > 5) m_nBodyIndex = 5;		
		}

		g_UFONumber = m_nCharacterIndex * 6 + m_nBodyIndex;
		CHARACTER.GetComponent<SkeletonAnimation> ().skeletonDataAsset = ARRAY_SKELETON [g_UFONumber];
	}

	public void LeftButtonClick()
	{
		if (m_nTab == 0)
		{
			m_nCharacterIndex--;
			if (m_nCharacterIndex < 0) m_nCharacterIndex = 0;
		}
		else if(m_nTab == 1)
		{
			m_nBodyIndex--;
			if (m_nBodyIndex < 0) m_nBodyIndex = 0;		
		}

		g_UFONumber = m_nCharacterIndex * 6 + m_nBodyIndex;
		CHARACTER.GetComponent<SkeletonAnimation> ().skeletonDataAsset = ARRAY_SKELETON [g_UFONumber];
	}

	public void FlightReady()
	{
		Application.LoadLevel ("FlightReady");
	}
}
