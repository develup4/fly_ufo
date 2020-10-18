using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	public bool OnlyDeactivate;
	private bool 	m_bEffectOn 	= false;
	private float	m_fEffectTime 	= 0.0f;
	
	void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
	}

	IEnumerator CheckIfAlive ()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			if(!particleSystem.IsAlive(true))
			{
				if(OnlyDeactivate)
				{
					#if UNITY_3_5
						this.gameObject.SetActiveRecursively(false);
					#else
						this.gameObject.SetActive(false);
					#endif
				}
				else
					GameObject.Destroy(this.gameObject);
				break;
			}
		}
	}

	public void PlayEffect()
	{
		m_bEffectOn = true;
		this.GetComponent<ParticleSystem>().Play();
	}

	void Update()
	{
		if (m_bEffectOn)
		{
			m_fEffectTime += Time.deltaTime;
			if (m_fEffectTime > this.GetComponent<ParticleSystem>().duration)
			{
				GameObject.Destroy(this.gameObject);
			}
		}
	}
}