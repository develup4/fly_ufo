using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    private float animationTime = 8f;
    private float remainTime;

    void Awake()
    {
        remainTime = animationTime;
    }

    void Update()
    {
        remainTime -= Time.deltaTime;
        if(remainTime < 0f)
        {
            Application.LoadLevel("Store");
        }
    }
}
