using UnityEngine;
using System.Collections;

public class MagnetEffect : MonoBehaviour {
    
	private GameObject UFO;
    private bool isMagnetEffected;
    private float destinationX;
    private float destinationY;
    private float currentX;
    private float currentY;

    private float magnetSpeed;

	void Awake(){
        isMagnetEffected = false;
        UFO = GameObject.Find("UFO");
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Magnet"))
        {
            isMagnetEffected = true;
        }
    }

    void Update()
    {
        if (isMagnetEffected)
        {
			if(!GetComponent<AcquireItem>().getIsCrash())
			{
	            destinationX = UFO.transform.position.x;
	            destinationY = UFO.transform.position.y;
	            currentX = transform.position.x;
	            currentY = transform.position.y;
	            magnetSpeed = UFO.GetComponent<UFO_Attribute>().maxMoveSpeed * 1.5f;

	            if (destinationX > currentX)
	            {
	                transform.Translate(magnetSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
	            }

	            if (destinationX < currentX)
	            {
	                transform.Translate(-magnetSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
	            }

	            if (destinationY > currentY)
	            {
	                transform.Translate(0.0f, magnetSpeed * Time.deltaTime, 0.0f, Space.World);
	            }

	            if (destinationY < currentY)
	            {
	                transform.Translate(0.0f, -magnetSpeed * Time.deltaTime, 0.0f, Space.World);
	            }
			}
        }
    }

    public void setIsMagnetEffected(bool magnet)
    {
        this.isMagnetEffected = magnet;
    }
}
