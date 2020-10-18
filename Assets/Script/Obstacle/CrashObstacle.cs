using UnityEngine;
using System.Collections;

public class CrashObstacle : MonoBehaviour {

    private bool isCrash;

	// Use this for initialization
	void Start () {
        isCrash = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isCrash)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 20.0f));
        }
	}

    public void setIsCrash(bool crash)
    {
        isCrash = crash;
    }

    public void setOriginalRotate()
    {
        transform.rotation = Quaternion.identity;
    }
}
