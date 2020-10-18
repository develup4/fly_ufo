using UnityEngine;
using System.Collections;

public class HP_glow : MonoBehaviour {
    private UFO_Attribute UFO_attribute;

    void Awake()
    {
        UFO_attribute = GameObject.Find("UFO").GetComponent<UFO_Attribute>();
    }

    void Update()
    {

    }
}
