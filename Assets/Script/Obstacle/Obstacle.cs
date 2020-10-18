using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    private bool isCrash = false;
    private bool polymorph = false;
    private Vector3 originalPosition;

	// Use this for initialization
	void Awake () {
        originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void setIsCrash(bool crash)
    {
        this.isCrash = crash;
    }

    public bool getIsCrash()
    {
        return this.isCrash;
    }

    public void setOriginalPosition()
    {
        transform.position = originalPosition;
    }

    public void setPolymorphObstacle(bool polymorph)
    {
        this.polymorph = polymorph;
    }

    public bool getPolymorphObstacle()
    {
        return this.polymorph;
    }
}