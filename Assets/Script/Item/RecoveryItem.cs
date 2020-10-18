using UnityEngine;
using System.Collections;

public class RecoveryItem : MonoBehaviour {
    
    public int recoveryAmount;

    private UFO_Attribute 	UFO_attribute;

    void Awake()
    {
        UFO_attribute = GameObject.Find("UFO").GetComponent<UFO_Attribute>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Player")){

            if (!GetComponent<AcquireItem>().getIsCrash())
            {
                UFO_attribute.currentHP += recoveryAmount;
                
                if (UFO_attribute.currentHP > UFO_attribute.MaxHP)
                {
                    UFO_attribute.currentHP = UFO_attribute.MaxHP;
                }
            }
        }
    }
}
