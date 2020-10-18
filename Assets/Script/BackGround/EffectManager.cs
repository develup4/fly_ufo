using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {

    public GameObject 	feverEffect;
	public GameObject[]	Effects;

    public void makeFeverEffect(Vector3 position)
    {
        Instantiate(feverEffect, new Vector3(position.x, position.y, -3.0f), Quaternion.identity);
    }

	public void MakeEffect(int nIndex, Vector3 vecPosition)
	{
		GameObject obj = Instantiate(Effects[nIndex], vecPosition, Quaternion.identity) as GameObject;
		obj.GetComponent<CFX_AutoDestructShuriken> ().PlayEffect ();
	}
}
