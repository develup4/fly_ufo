using UnityEngine;
using System.Collections;

public class TrackingMap : MonoBehaviour {

	private GameObject ufo;

	public float Layer;

	// Use this for initialization
	void Start () {
        ufo = GameObject.Find("UFO");
	}
	
	// Update is called once per frame
	void Update () {
		if (ufo.GetComponent<UFO>().GetCameraTracking())
		{
	        Vector3 vecUFOPosition = ufo.transform.position;

	        if (ufo.GetComponent<UFO>().GetIsXTracking() == false)
	        {
	            vecUFOPosition.x = this.transform.position.x;
	        }

	        vecUFOPosition.y = ufo.transform.position.y + 3.6f;
			vecUFOPosition.z = Layer;

			this.transform.position = vecUFOPosition;
		}
	}
}