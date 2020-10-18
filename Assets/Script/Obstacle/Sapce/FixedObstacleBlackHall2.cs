using UnityEngine;
using System.Collections;

public class FixedObstacleBlackHall2 : MonoBehaviour {

    public AudioClip hitSound;

    private GameObject ufo;

	// Use this for initialization
	void Start () {
        ufo = GameObject.Find("UFO");
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0.0f, 0.0f, 5.0f));
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (col.gameObject.GetComponent<UFO_Animation>().getAnimationState() == UFO_Animation.NORMAL_STATE)
            {
                if (!audio.isPlaying)
                {
                    audio.clip = hitSound;
                    audio.Play();
                }

                col.gameObject.GetComponent<UFO>().CollideObject(false, true);
                col.gameObject.GetComponent<UFO_Animation>().setAnimationState(UFO_Animation.CRUSH_STATE);
            }
        }
    }
}
