using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour
{
    public Sprite[] scoreImage;
    public Sprite[] numberTexture;

    private Result result;
    private GameObject[] childScoreBoard;
    private GameObject[] childCommaBoard;
    private int i, len, commaCount, tempValue, tempNumber;
    private float currentGab, prevX;

    private const float gab_1 = 0.1f;
    private const float gab_comma = 0.05f;
    private const float gab_normal = 0.15f;

    void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = scoreImage[GameObject.Find("UFO").GetComponent<UFO_Attribute>().scoreLevel-1];
        result = GameObject.Find("Result").GetComponent<Result>();

        childScoreBoard = new GameObject[9];
        childCommaBoard = new GameObject[3];

        for(i=0; i<9; i++)
        {
            childScoreBoard[i] = transform.FindChild("Board" + (i+1)).gameObject;
        }

        for(i=0; i<3; i++)
        {
            childCommaBoard[i] = transform.FindChild("Comma" + (i+1)).gameObject;
        }

        renew();
    }

    public void renew()
    {
        len = 0;
        currentGab = 0.25f;
        tempValue = result.score;
        prevX = 0.0f;

        while(tempValue != 0)
        {
            tempValue /= 10;
            len++;
        }

        tempValue = result.score;
        commaCount = 0;

        for(i=len-1; i>=0; i--)
        {
            tempNumber = tempValue / (int)Mathf.Pow(10, i);
            tempValue = tempValue % (int)Mathf.Pow(10, i);

            childScoreBoard[i].GetComponent<SpriteRenderer>().sprite = numberTexture[tempNumber];

            if(tempNumber == 1)
            {
                childScoreBoard[i].transform.transform.localPosition = new Vector3(prevX + currentGab + gab_1, 0.0f, 0.0f);
                prevX += currentGab + gab_1;
                currentGab = gab_1;
            }
            else
            {
                childScoreBoard[i].transform.localPosition = new Vector3(prevX + currentGab + gab_normal, 0.0f, 0.0f);
                prevX += currentGab + gab_normal;
                currentGab = gab_normal;
            }

            if(i == 3 || i == 6 || i == 9)
            {
                GameObject tempCommaObject = childCommaBoard[commaCount];

                tempCommaObject.GetComponent<SpriteRenderer>().enabled = true;
                tempCommaObject.transform.localPosition = new Vector3(prevX + currentGab + gab_comma, -0.15f, 0.0f);
                prevX += currentGab + gab_comma;
                currentGab = gab_comma;
                commaCount++;
            }
        }
    }
}
