using UnityEngine;
using System.Collections;

public class FlightBoard : MonoBehaviour {
    public Sprite[] numberTexture;
    public GameObject[] km;

    private bool isFeverMode;
    private int i, len, commaCount, tempNumber;
    private float tempValue, currentGab, prevX;
    private Transform UFO_transform;
    private GameObject[] childNumberBoard;
    private GameObject[] childCommaBoard;

    private const float startX = 0.0f;
    private const float gab_1 = 0.08f;
    private const float gab_comma = 0.04f;
    private const float gab_normal = 0.12f;

    void Awake()
    {
        int i;

        childNumberBoard = new GameObject[9];
        childCommaBoard = new GameObject[3];

        for(i=0; i<9; i++)
        {
            childNumberBoard[i] = transform.FindChild("Board" + (i+1)).gameObject;
        }

        for(i=0; i<3; i++)
        {
            childCommaBoard[i] = transform.FindChild("Comma" + (i+1)).gameObject;
        }

        UFO_transform = GameObject.Find("UFO").GetComponent<Transform>();
    }

    void Update()
    {
        if(!isFeverMode)
        {
            tempValue = UFO_transform.position.y / 10f;

            if(tempValue >= 0.0f)
            {
                len = 0;
                currentGab = 0.0f;
                prevX = -0.15f;

                // 숫자 표시
                do{
                    tempValue /= 10;
                    len++;
                }while((int)tempValue != 0);

                for(i=0; i<len; i++)
                {
                    childNumberBoard[i].GetComponent<SpriteRenderer>().enabled = true;
                }

                for(; i<9; i++)
                {
                    childNumberBoard[i].GetComponent<SpriteRenderer>().enabled = false;
                }

                tempValue = UFO_transform.position.y / 10f;
                commaCount = 0;

                for(i=len-1; i>=0; i--)
                {
                    tempNumber = (int)(tempValue / (int)Mathf.Pow(10, i));
                    tempValue = tempValue % (int)Mathf.Pow(10, i);

                    childNumberBoard[i].GetComponent<SpriteRenderer>().sprite = numberTexture[tempNumber];

                    if(tempNumber == 1)
                    {
                        childNumberBoard[i].transform.localPosition = new Vector3(prevX + currentGab + gab_1, 0.0f, 0.0f);
                        prevX += currentGab + gab_1;
                        currentGab = gab_1;
                    }
                    else
                    {
                        childNumberBoard[i].transform.localPosition = new Vector3(prevX + currentGab + gab_normal, 0.0f, 0.0f);
                        prevX += currentGab + gab_normal;
                        currentGab = gab_normal;
                    }

                    // 콤마 구분 관련
                    if(len < 4)
                    {
                        childCommaBoard[0].GetComponent<SpriteRenderer>().enabled = false;
                        childCommaBoard[1].GetComponent<SpriteRenderer>().enabled = false;
                        childCommaBoard[2].GetComponent<SpriteRenderer>().enabled = false;
                    }
                    else if(len < 7)
                    {
                        childCommaBoard[1].GetComponent<SpriteRenderer>().enabled = false;
                        childCommaBoard[2].GetComponent<SpriteRenderer>().enabled = false;
                    }
                    else if(len < 10)
                    {
                        childCommaBoard[2].GetComponent<SpriteRenderer>().enabled = false;
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

                km[0].transform.localPosition = new Vector3(prevX + currentGab + 0.116f, -0.03f, 0.0f);
                km[1].transform.localPosition = new Vector3(km[0].transform.localPosition.x + 0.252f, -0.03f, 0.0f);        // 0.252f = 0.116f + 0.136f
            }
        }
    }

    public void setIsFeverMode(bool tf)
    {
        isFeverMode = tf;
    }
}
