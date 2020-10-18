using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {
    private float remainTime = animationTime;
    private const float animationTime = 4f;

    void Update()
    {
        remainTime -= Time.deltaTime;
        if(remainTime < 0)
        {
			Application.LoadLevel("Title");
        }
    }
}
