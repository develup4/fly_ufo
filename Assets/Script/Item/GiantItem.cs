using UnityEngine;
using System.Collections;

public class GiantItem : MonoBehaviour {
    public float duration;
    public float growthSize;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            if (!GetComponent<AcquireItem>().getIsCrash())
            {
                col.GetComponent<GiantEffect>().setRemainTime(duration);
                col.GetComponent<GiantEffect>().setIsItemGet(true);
                col.GetComponent<GiantEffect>().setGrowthSize(growthSize);
                col.GetComponent<UFO>().SetIgnoreObjectMode(true);
                col.GetComponent<UFO>().SetGiantMode(true);
            }
        }
    }
}
