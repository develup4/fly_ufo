using UnityEngine;
using System.Collections;

public class HP_Bar_glow : MonoBehaviour {
    private float currentHP, maxHP, positionY;
    private UFO_Attribute UFO_attribute;

    void Awake()
    {
        UFO_attribute = GameObject.Find("UFO").GetComponent<UFO_Attribute>();
        maxHP = UFO_attribute.MaxHP;
    }
    void Update()
    {
        currentHP = UFO_attribute.currentHP;
        Debug.Log((currentHP / maxHP) * 0.007621f);
        transform.localPosition = new Vector2(0.0f, (currentHP * 0.007621f) + 2.18f);
    }
}
