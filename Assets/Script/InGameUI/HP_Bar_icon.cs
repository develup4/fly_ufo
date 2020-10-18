using UnityEngine;
using System.Collections;

public class HP_Bar_icon : MonoBehaviour {
    public Texture2D icon;

    private float displayRateX, displayRateY;
    private const float basicResolutionX = 720;
    private const float basicResolutionY = 1280;

    void OnGUI()
    {
        displayRateX = (float)Screen.width / basicResolutionX;
        displayRateY = (float)Screen.height / basicResolutionY;

        //Debug.Log(Screen.width + ", " + Screen.height);
        //Debug.Log(displayRateX + ", " + displayRateY);
        //Debug.Log(10.0f * displayRateX + ", " + 1062.0f * displayRateY);
        GUI.BeginGroup(new Rect(10.0f * displayRateX, 1062.0f * displayRateY, icon.width * displayRateX, icon.height * displayRateY));
        GUI.depth = 1;
        GUI.DrawTexture(new Rect(0, 0, icon.width * displayRateX, icon.height * displayRateY), icon);
        GUI.EndGroup();
    }
}
