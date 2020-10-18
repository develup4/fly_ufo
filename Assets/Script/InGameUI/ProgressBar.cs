using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {
    public Texture2D image;
    public float percent;

    private float imageMaxHeight, maxHP;
    private float displayRateX, displayRateY;
    private UFO_Attribute UFO_attribute;

    private const int basicResolutionX = 720;
    private const int basicResolutionY = 1280;

    void Awake()
    {
        UFO_attribute = GameObject.Find("UFO").GetComponent<UFO_Attribute>();
        maxHP = UFO_attribute.MaxHP;
        displayRateX = (float)Screen.width / (float)basicResolutionX;
        displayRateY = (float)Screen.height / (float)basicResolutionY;
        imageMaxHeight = image.height * displayRateX;
    }

    void OnGUI()
    {
        float barSize = percent * imageMaxHeight * (maxHP / 515.0f);

        percent = UFO_attribute.currentHP / UFO_attribute.MaxHP;

        Rect rect = new Rect(20.5f*displayRateX, 297.0f*displayRateY, image.width * displayRateX, barSize);
        GUI.BeginGroup(rect);
        GUIUtility.RotateAroundPivot(180.0f, new Vector2(17.5f * displayRateX, 391.0f * displayRateY));
        GUI.depth = 2;
        GUI.DrawTexture(new Rect(0, 0, image.width * displayRateX, image.height * displayRateY * (maxHP / 515.0f)), image);
        GUI.EndGroup();
    }
}
