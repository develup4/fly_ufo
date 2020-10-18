using UnityEngine;
using System.Collections;

public class CollideEarth : MonoBehaviour
{
    private GameObject UFO_Object;

    void Start()
    {
        UFO_Object = GameObject.Find("UFO");
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && UFO_Object.rigidbody2D.velocity.y < UFO_Object.GetComponent<UFO_Attribute>().maxMoveSpeed * -0.75f)
        {
            UFO_Object.GetComponent<UFO>().CollideObject(false, false);
            UFO_Object.GetComponent<UFO_Animation>().setAnimationState(UFO_Animation.CRUSH_STATE);
        }
    }
}
