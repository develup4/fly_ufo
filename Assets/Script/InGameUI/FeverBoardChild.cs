using UnityEngine;
using System.Collections;

public class FeverBoardChild : MonoBehaviour {
    private SkeletonAnimation animation;

    void Awake()
    {
        animation = GetComponent<SkeletonAnimation>();
    }

    public void itemGet()
    {
        animation.animationName = "on";
    }

    public void startAnimation()
    {
        animation.animationName = "animation";
    }

    public void init()
    {
        animation.animationName = "off";
    }
}
