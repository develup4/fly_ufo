using UnityEngine;
using System.Collections;

public class PatternDelete : MonoBehaviour {

    private GameObject UFO_Object;

	// Use this for initialization
	void Start () {
        UFO_Object = GameObject.Find("UFO");	
	}
	
	// Update is called once per frame
	void Update () {
        if (UFO_Object.transform.position.y - transform.position.y > 38.6f)
        {
            transform.position = new Vector3(100.0f, 0.0f, 0.0f);
            gameObject.SetActive(false);
        }
	}
}
