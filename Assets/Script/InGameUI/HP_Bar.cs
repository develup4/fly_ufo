using UnityEngine;
using System.Collections;

public class HP_Bar : MonoBehaviour {

    public Transform UFO_Rocation;
    public SkeletonAnimation glowAnimation;
    public UISprite icon;

    private UFO_Attribute UFO_attribute;
    private UISlider slider;
    private float currentHP, maxHP, positionY;

    private const float HP_LIMIT = 510;
    private const float PixelPerHP = 765f / 510f;

    void Awake()
    {
        UFO_attribute = GameObject.Find("UFO").GetComponent<UFO_Attribute>();
        slider = GetComponent<UISlider>();

        currentHP = UFO_attribute.currentHP;
        maxHP = UFO_attribute.MaxHP;

        float rate = maxHP / HP_LIMIT;
        transform.GetChild(0).localScale = new Vector3(1f, rate, 1f);
        transform.GetChild(1).localScale = new Vector3(1f, rate, 1f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "UFO")
        {
            slider.alpha = 0.3f;
            icon.alpha = 0.3f;
        }
    }

    public void Update()
    {
        currentHP = UFO_attribute.currentHP;
        maxHP = UFO_attribute.MaxHP;

        slider.value = currentHP / maxHP;
        glowAnimation.transform.localPosition = new Vector3(-322.5f, -383f + ((currentHP > 5.0f ? currentHP : 5.0f) * PixelPerHP), 0f);

        if(UFO_Rocation.localPosition.x > -9.5f)
        {
            slider.alpha = 1f;
            icon.alpha = 1f;
        }
    }
}
