using UnityEngine;
using System.Collections;

public class MeteorRotate : MonoBehaviour {

    private int leftright;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void setRotate(Vector2 vec)
    {
        if (leftright == -1)
        {
            float a = Mathf.Acos(Vector2.Dot(vec, Vector2.up));
            this.transform.Rotate(Vector3.forward, Mathf.Rad2Deg * a - 180.0f);
        }
        else if(leftright == 1)
        {
            float a = Mathf.Acos(Vector2.Dot(vec, Vector2.up * -1));
            this.transform.Rotate(Vector3.forward, Mathf.Rad2Deg * a);
        }
    }

    public void setLeftRight(int lr)
    {
        leftright = lr;
    }
}
