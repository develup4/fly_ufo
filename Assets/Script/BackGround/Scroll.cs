using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour
{

    private GameObject UFO;

    private Vector2 uvOffset = Vector2.zero;
    private Vector2 dirVec;

    public float speed;
    public string material;

    private int YScrollCount = 0;
    private int XScrollCount = 0;

    // Use this for initialization
    void Start()
    {
        UFO = GameObject.Find("UFO");
    }

    // Update is called once per frame
    void Update()
    {
		if (UFO.GetComponent<UFO>().GetCameraTracking())
		{
	        dirVec = UFO.rigidbody2D.velocity;

			if (UFO.GetComponent<UFO>().GetIsXTracking()) 
			{
	            uvOffset.x += dirVec.x * Time.deltaTime * speed;
				uvOffset.y += dirVec.y * Time.deltaTime * speed;		
			}

			else 
			{
				uvOffset.y += dirVec.y * Time.deltaTime * speed;
			}

	        if (renderer.enabled)
	        {
	            renderer.material.SetTextureOffset(material, uvOffset);

	            if (uvOffset.y >= 1.0f)
	            {
	                uvOffset.y = 0.0f;
	                YScrollCount++;
	            }

	            if (uvOffset.y <= -1.0f)
	            {
	                uvOffset.y = 0.0f;
	                YScrollCount--;
	            }

	            if (uvOffset.x >= 1.0f)
	            {
	                uvOffset.x = 0.0f;
	                XScrollCount++;
	            }

	            if (uvOffset.x <= -1.0f)
	            {
	                uvOffset.x = 0.0f;
	                XScrollCount--;
	            }
	        }
		}
    }

    public int getYScrollCount()
    {
        return YScrollCount;
    }

    public int getXScrollCount()
    {
        return XScrollCount;
    }

    public void setUvOffset(Vector2 offset)
    {
        this.uvOffset = offset;
    }
}
