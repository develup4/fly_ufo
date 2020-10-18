using UnityEngine;
using System.Collections;

public class PolymorphItem : MonoBehaviour {
    
    public GameObject changeItem;

    public float changeItemWidth;
    public float changeItemHeight;

    private GameObject polymorphCollider;

    void Awake()
    {
        polymorphCollider = GameObject.Find("PolymorphCollider");
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            if (!GetComponent<AcquireItem>().getIsCrash())
            {
                polymorphCollider.GetComponent<BoxCollider2D>().enabled = true;
                polymorphCollider.GetComponent<PolymorphCollider>().setIsItemGet(true);
                polymorphCollider.GetComponent<PolymorphCollider>().setChangeItem(changeItem);
                polymorphCollider.GetComponent<PolymorphCollider>().setChangeItemSize(changeItemWidth, changeItemHeight);
            }
        }
    }
}
