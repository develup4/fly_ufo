using UnityEngine;
using System.Collections;

public class MagnetItem : MonoBehaviour {
    public float duration;
    public float magnetForce;

    private GameObject magnetCollider;

    void Awake()
    {
        magnetCollider = GameObject.Find("MagnetCollider");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!GetComponent<AcquireItem>().getIsCrash())
        {
            magnetCollider.GetComponent<CircleCollider2D>().enabled = true;
            magnetCollider.GetComponent<MagnetCollider>().setRemainTime(duration);
            magnetCollider.GetComponent<MagnetCollider>().setItemMagnetForce(magnetForce);
        }
	}
}
