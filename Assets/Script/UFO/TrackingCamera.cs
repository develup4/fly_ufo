using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour
{
    private GameObject      m_objUFO;
    private const float     TRACKING_CAMERA_Z = -10.0f;

	void Start()
    {
        m_objUFO = GameObject.Find("UFO");
	}
	
	void Update()
    {
		if (m_objUFO.GetComponent<UFO>().GetCameraTracking())
		{
	        // 캐릭터 따라다니도록
	        Vector3 vecUFOPosition = m_objUFO.transform.position;

	        if (m_objUFO.GetComponent<UFO>().GetIsXTracking() == false)
	        {
	            vecUFOPosition.x = this.transform.position.x;
	        }

			vecUFOPosition.y = m_objUFO.transform.position.y + 3.6f;
	        vecUFOPosition.z = TRACKING_CAMERA_Z;
	        this.transform.position = vecUFOPosition;
		}
	}
}