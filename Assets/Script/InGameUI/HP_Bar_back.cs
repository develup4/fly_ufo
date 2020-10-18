using UnityEngine;
using System.Collections;

public class HP_Bar_back : MonoBehaviour {
    public Texture2D image;

    private float imageMaxHeight, maxHP;
    private float displayRateX, displayRateY, barSize;

    private const int basicResolutionX = 720;
    private const int basicResolutionY = 1280;

    void Awake()
    {
        maxHP = GameObject.Find("UFO").GetComponent<UFO_Attribute>().MaxHP;
        displayRateX = (float)Screen.width / (float)basicResolutionX;
        displayRateY = (float)Screen.height / (float)basicResolutionY;
        imageMaxHeight = image.height * displayRateX;
        barSize = imageMaxHeight * (maxHP / 515.0f);
    }

    void OnGUI()
    {
        Rect rect = new Rect(20.5f*displayRateX, 297.0f*displayRateY, image.width * displayRateX, barSize);
        GUI.BeginGroup(rect);
        GUIUtility.RotateAroundPivot(180.0f, new Vector2(17.5f * displayRateX, 391.0f * displayRateY));
        GUI.depth = 3;
        GUI.DrawTexture(new Rect(0, 0, image.width * displayRateX, image.height * displayRateY * (maxHP / 515.0f)), image);
        GUI.EndGroup();
    }
}
