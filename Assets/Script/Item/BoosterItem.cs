using UnityEngine;
using System.Collections;

public class BoosterItem : MonoBehaviour {
    public float duration;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            if (!GetComponent<AcquireItem>().getIsCrash())
            {
                col.GetComponent<BoosterEffect>().setRemainTime(duration);
                col.GetComponent<BoosterEffect>().setIsItemGet(true);
                col.GetComponent<UFO>().SetIgnoreObjectMode(true);

                col.GetComponent<UFO>().BOOSTER_EFFECT_LEFT.SetActive(true);
                col.GetComponent<UFO>().BOOSTER_EFFECT_LEFT.GetComponent<ParticleSystem>().Play();

                col.GetComponent<UFO>().BOOSTER_EFFECT_RIGHT.SetActive(true);
                col.GetComponent<UFO>().BOOSTER_EFFECT_RIGHT.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
