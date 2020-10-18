using UnityEngine;
using System.Collections;

public class Level_Bar : MonoBehaviour {
    public int userLevel;
    public int userExp;
    public Transform levelLabel1;
    public Transform levelLabel2;
    public Transform expLabel1;
    public Transform expLabel2;
    public Transform percent;
    public UISlider expSlider;

    private int num1, num2;
    private float[] Level_fontSize = new float[10] { 18f, 9f, 15f, 14f, 16f, 15f, 16f, 14f, 14f, 16f };
    private float[] EXP_fontSize = new float[10] { 18f, 12f, 18f, 18f, 18f, 18f, 18f, 18f, 18f, 18f };
    private float percentSize = 28.8f;

    private const int maxLevel = 60;
    private int[] requireEXP = new int[maxLevel] {0, 260, 338, 439, 571, 743, 965, 1255, 1631, 2121, 2757, 3584,
        4660, 6058, 7875, 10237, 13308, 17301, 22491, 29238, 35086, 42103, 50524, 60629,
        72755, 87305, 104766, 125720, 150864, 165950, 182545, 200800, 220880, 242968, 267264, 293991,
        323390, 355729, 391302, 430432, 451953, 474551, 498279, 523193, 549352, 576820, 605661, 635944,
        667741, 701128, 718656, 736623, 755038, 773914, 793262, 813094, 833421, 854257, 875613, 897503
    };

    void OnEnable()
    {
		/*
        levelLabel1.GetComponent<UISprite>().spriteName = "main_ui_level_font_" + (userLevel / 10);
        levelLabel2.GetComponent<UISprite>().spriteName = "main_ui_level_font_" + (userLevel % 10);
        levelLabel1.GetComponent<UISprite>().MakePixelPerfect();
        levelLabel2.GetComponent<UISprite>().MakePixelPerfect();

        num1 = (int)(((float)userExp / (float)requireEXP[userLevel]) * 10f);
        num2 = (int)((((float)userExp / (float)requireEXP[userLevel]) * 100f) % 10f);

        expLabel1.GetComponent<UISprite>().spriteName = "ingame_ui_score_" + (num1 + 1);
        expLabel2.GetComponent<UISprite>().spriteName = "ingame_ui_score_" + (num2 + 1);
        expLabel1.GetComponent<UISprite>().MakePixelPerfect();
        expLabel2.GetComponent<UISprite>().MakePixelPerfect();
        expLabel1.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        expLabel2.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        renew();
        */
    }


    public void renew()
    {
        // 레벨 표시 부분
        int level1 = userLevel / 10;
        int level2 = userLevel % 10;
        float temp;

        if(userLevel < 10)
        {
            levelLabel1.gameObject.SetActive(false);
            levelLabel2.localPosition = new Vector3(0f, 0f, 0f);
        }
        else
        {
            temp = (Level_fontSize[level1] + Level_fontSize[level2] + 2.5f) / 2f;
            levelLabel1.localPosition = new Vector3(-temp + (Level_fontSize[level1] / 2f), 0f, 0f);
            levelLabel2.localPosition = new Vector3(temp - (Level_fontSize[level2] / 2), 0f, 0f);
        }

        // 경험치 표시 부분
        temp = (144f - (EXP_fontSize[num1] + EXP_fontSize[num2] + 5f + percentSize)) / 2f;
        expLabel1.localPosition = new Vector3(temp + 25f + (EXP_fontSize[num1] / 2f), 0f, 0f);
        expLabel2.localPosition = new Vector3(expLabel1.localPosition.x + 5f + ((EXP_fontSize[num1] + EXP_fontSize[num2]) / 2f), 0f, 0f);
        percent.localPosition = new Vector3(expLabel2.localPosition.x + 5f + ((EXP_fontSize[num2] + percentSize) / 2f), 0f, 0f);

        expSlider.value += (float)(((float)userExp / (float)requireEXP[userLevel]) * (144f / 169f)) + (25f / 169f);
    }
    

}