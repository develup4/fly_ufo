using UnityEngine;
using System.Collections;

public class AlphaChange : MonoBehaviour {

	public float changeDayTime;
    public float changeAlphaSpeed;
    public int interval;

    private Color alphaLayerDayColor;
    private Color alphaLayerNightColor;
    private Color bigLayerDayColor;
    private Color smallLayerDayColor;
    private Color bigLayerNightColor;
    private Color smallLayerNightColor;

    private GameObject gameManager;
    private GameObject bigLayer;
    private GameObject smallLayer;

	private float realTime = 0.0f;
	private float changeTime = 0.0f;

	private bool day = true;

    private Color alphaColor;
    private Color bigColor;
    private Color smallColor;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        bigLayer = GameObject.Find("BigLayer");
        smallLayer = GameObject.Find("SmallLayer");

        alphaLayerDayColor.r = 1.0f;
        alphaLayerDayColor.g = 1.0f;
        alphaLayerDayColor.b = 1.0f;
        alphaLayerDayColor.a = 0.2f;

        alphaLayerNightColor.r = 1.0f;
        alphaLayerNightColor.g = 1.0f;
        alphaLayerNightColor.b = 1.0f;
        alphaLayerNightColor.a = 0.9f;

        bigLayerDayColor.r = 1.0f;
        bigLayerDayColor.g = 1.0f;
        bigLayerDayColor.b = 1.0f;
        bigLayerDayColor.a = 1.0f;

        bigLayerNightColor.r = 0.368f;
        bigLayerNightColor.g = 0.419f;
        bigLayerNightColor.b = 0.454f;
        bigLayerNightColor.a = 1.0f;

        smallLayerDayColor.r = 1.0f;
        smallLayerDayColor.g = 1.0f;
        smallLayerDayColor.b = 1.0f;
        smallLayerDayColor.a = 0.4f;

        smallLayerNightColor.r = 1.0f;
        smallLayerNightColor.g = 1.0f;
        smallLayerNightColor.b = 1.0f;
        smallLayerNightColor.a = 0.15f;

        GetComponent<SpriteRenderer>().color = alphaLayerDayColor;
        smallLayer.renderer.material.SetColor("_Color", smallLayerDayColor);
	}
	
	// Update is called once per frame
	void Update () {

        if (gameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.EARTH_STAGE)
        {
            realTime += Time.deltaTime;

            // 바뀌여야 될 시간이 지나면
            if (realTime > changeDayTime)
            {
                changeTime += Time.deltaTime;

                if (changeTime > changeAlphaSpeed / interval)
                {
                    alphaColor = transform.GetComponent<SpriteRenderer>().color;
                    bigColor = bigLayer.renderer.material.GetColor("_Color");
                    smallColor = smallLayer.renderer.material.GetColor("_Color");

                    if (day == false)
                    {
                        alphaColor.r -= Mathf.Abs(alphaLayerDayColor.r - alphaLayerNightColor.r) / interval;
                        alphaColor.g -= Mathf.Abs(alphaLayerDayColor.g - alphaLayerNightColor.g) / interval;
                        alphaColor.b -= Mathf.Abs(alphaLayerDayColor.b - alphaLayerNightColor.b) / interval;
                        alphaColor.a -= Mathf.Abs(alphaLayerDayColor.a - alphaLayerNightColor.a) / interval;

                        bigColor.r += Mathf.Abs(bigLayerDayColor.r - bigLayerNightColor.r) / interval;
                        bigColor.g += Mathf.Abs(bigLayerDayColor.g - bigLayerNightColor.g) / interval;
                        bigColor.b += Mathf.Abs(bigLayerDayColor.b - bigLayerNightColor.b) / interval;
                        bigColor.a += Mathf.Abs(bigLayerDayColor.a - bigLayerNightColor.a) / interval;

                        smallColor.r -= Mathf.Abs(smallLayerDayColor.r - smallLayerNightColor.r) / interval;
                        smallColor.g -= Mathf.Abs(smallLayerDayColor.g - smallLayerNightColor.g) / interval;
                        smallColor.b -= Mathf.Abs(smallLayerDayColor.b - smallLayerNightColor.b) / interval;
                        smallColor.a += Mathf.Abs(smallLayerDayColor.a - smallLayerNightColor.a) / interval;


                    }
                    else if (day == true)
                    {
                        alphaColor.r += Mathf.Abs(alphaLayerDayColor.r - alphaLayerNightColor.r) / interval;
                        alphaColor.g += Mathf.Abs(alphaLayerDayColor.g - alphaLayerNightColor.g) / interval;
                        alphaColor.b += Mathf.Abs(alphaLayerDayColor.b - alphaLayerNightColor.b) / interval;
                        alphaColor.a += Mathf.Abs(alphaLayerDayColor.a - alphaLayerNightColor.a) / interval;

                        bigColor.r -= Mathf.Abs(bigLayerDayColor.r - bigLayerNightColor.r) / interval;
                        bigColor.g -= Mathf.Abs(bigLayerDayColor.g - bigLayerNightColor.g) / interval;
                        bigColor.b -= Mathf.Abs(bigLayerDayColor.b - bigLayerNightColor.b) / interval;
                        bigColor.a -= Mathf.Abs(bigLayerDayColor.a - bigLayerNightColor.a) / interval;

                        smallColor.r += Mathf.Abs(smallLayerDayColor.r - smallLayerNightColor.r) / interval;
                        smallColor.g += Mathf.Abs(smallLayerDayColor.g - smallLayerNightColor.g) / interval;
                        smallColor.b += Mathf.Abs(smallLayerDayColor.b - smallLayerNightColor.b) / interval;
                        smallColor.a -= Mathf.Abs(smallLayerDayColor.a - smallLayerNightColor.a) / interval;
                    }

                    GetComponent<SpriteRenderer>().color = alphaColor;
                    bigLayer.renderer.material.SetColor("_Color", bigColor);
                    smallLayer.renderer.material.SetColor("_Color", smallColor);

                    changeTime = 0.0f;
                }

                if (alphaColor.a < 0.2f && day == false)
                {
                    day = true;
                    realTime = 0.0f;
                }

                if (alphaColor.a > 0.9f && day == true)
                {
                    day = false;
                    realTime = 0.0f;
                }
            }
        }
	}

    public bool getDay()
    {
        return day;
    }
}