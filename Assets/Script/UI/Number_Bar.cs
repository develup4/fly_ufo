using UnityEngine;
using System.Collections;

public class Number_Bar : MonoBehaviour {
    public int number;
    public int maxiumNumber;
    public Transform[] numberLabel;
    public Transform[] commaLabel;

    private int[] num = new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    private float[] fontSize = new float[] {14f, 8f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f};
    private float commaSize = 5f;

    void OnEnable()
    {
        renew();
    }

    void renew()
    {
		/*
        for(int i=0; i < numberLabel.Length; i++)
            numberLabel[i].GetComponent<UISprite>().spriteName = "None";
        
        for(int i=0; i<commaLabel.Length; i++)
            commaLabel[i].GetComponent<UISprite>().spriteName = "None";

        if(number == 0)
        {
            numberLabel[0].GetComponent<UISprite>().spriteName = "main_ui_font_bar_1";
        }
        else
        {
            int count = 0;
            int commaCount = 0;
            int tempCash = number;
            float prevX = (GetComponent<UISprite>().localSize.x / 2f) - 10f;

            if(number > maxiumNumber)
            {
                tempCash = maxiumNumber;
            }

            while (tempCash != 0)
            {
                num[count] = tempCash % 10;
                tempCash /= 10;
                count++;
            }

            for(int i=0; i<count; i++)
            {
                if(i % 3 == 0 && i != 0)
                {
                    commaLabel[commaCount].GetComponent<UISprite>().spriteName = "main_ui_font_bar_comma";
                    commaLabel[commaCount].localPosition = new Vector3(prevX - (commaSize / 2f) + 3f, -8f, 0f);
                    prevX = commaLabel[commaCount].localPosition.x - (commaSize / 2f) - 0.5f;
                    commaCount++;
                }
                numberLabel[i].GetComponent<UISprite>().spriteName = "main_ui_font_bar_" + (num[i] + 1);
                numberLabel[i].GetComponent<UISprite>().MakePixelPerfect();
                numberLabel[i].localPosition = new Vector3(prevX - (fontSize[num[i]] / 2f), 0f, 0f);
                prevX = numberLabel[i].localPosition.x - (fontSize[num[i]] / 2f) - 3f;
            }
        }
        */
    }
}
