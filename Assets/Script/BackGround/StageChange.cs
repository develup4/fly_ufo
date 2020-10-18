using UnityEngine;
using System.Collections;

public class StageChange : MonoBehaviour
{
    public Material bigEarth;
    public Material smallEarth;
    public Material bigSpace;
    public Material smallSpace;
    public Material bigFever;
    public Material smallFever;

    private Color color;

    private GameObject ufo;
    private GameObject gameManager;
    private GameObject effectManager;
    private GameObject bigLayer;
    private GameObject smallLayer;
    private GameObject alphaLayer;
    private GameObject colorLayer;
    private GameObject fadeLayer;

    private bool fadeout = false;
    private bool fadein = false;
	private bool onlyfadein = false;
	private bool effectend = false;

    private int nextStage;

    // Use this for initialization
    void Start()
    {
        ufo = GameObject.Find("UFO");
        gameManager = GameObject.Find("GameManager");
        effectManager = GameObject.Find("EffectManager");
        bigLayer = GameObject.Find("BigLayer");
        smallLayer = GameObject.Find("SmallLayer");
        alphaLayer = GameObject.Find("AlphaLayer");
        colorLayer = GameObject.Find("ColorLayer");
        fadeLayer = GameObject.Find("FadeLayer");

        color = fadeLayer.GetComponent<SpriteRenderer>().color;
        color.a = 0.0f;

        fadeLayer.GetComponent<SpriteRenderer>().color = color;

        color.r = 0.274f;
        color.g = 0.764f;
        color.b = 0.96f;
        color.a = 1.0f;

        colorLayer.GetComponent<SpriteRenderer>().color = color;
    }

    // 피버 스테이지 이동 함수
    public void feverStage()
    {
        gameManager.GetComponent<MapControlManager>().setFeverPatternMomentYPosition(-4993.6f);
        gameManager.GetComponent<MapControlManager>().setFeverPatternYPosition(-4993.6f + 38.4f);
        gameManager.GetComponent<MapControlManager>().setPrevStage(gameManager.GetComponent<MapControlManager>().getGameMode());
        gameManager.GetComponent<MapControlManager>().setGameMode(MapControlManager.CHANGE_STAGE);
        gameManager.GetComponent<MapControlManager>().setPrevUfoPosition(ufo.transform.position);
        gameManager.GetComponent<MapControlManager>().saveFeverXPosition(ufo.transform.position.x);
        gameManager.GetComponent<MapControlManager>().setPrevSpeed(ufo.rigidbody2D.velocity);
        gameManager.GetComponent<MapControlManager>().setBigLayerPrevColor(bigLayer.renderer.material.color);
        gameManager.GetComponent<MapControlManager>().setSmallLayerPrevColor(smallLayer.renderer.material.color);
        gameManager.GetComponent<MapControlManager>().setAlphaLayerPrevColor(alphaLayer.GetComponent<SpriteRenderer>().color);
        gameManager.GetComponent<MapControlManager>().setBigLayerPrevOffset(bigLayer.renderer.material.GetTextureOffset("_MainTex"));
        gameManager.GetComponent<MapControlManager>().setSmallLayerPrevOffset(smallLayer.renderer.material.GetTextureOffset("_MainTex"));

        ufo.rigidbody2D.gravityScale = 0.0f;
        ufo.rigidbody2D.velocity = new Vector2(0.0f, 0.0f);

        ufo.GetComponent<UFO>().SetControllable(false);

        effectManager.GetComponent<EffectManager>().makeFeverEffect(ufo.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.CHANGE_STAGE ||
            gameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.FEVER_STAGE)
        {
            if (fadeout)
            {
                color = fadeLayer.GetComponent<SpriteRenderer>().color;
                color.a += 0.01f;
                fadeLayer.GetComponent<SpriteRenderer>().color = color;

				if (onlyfadein && color.a > 2.20f && effectend == false)
				{
				    ufo.GetComponent<UFO>().SOUL_EFFECT.SetActive (true);
					ufo.GetComponent<UFO>().SOUL_EFFECT.GetComponent<ParticleSystem> ().Play ();
					effectend = true;
				}

				if (onlyfadein && color.a > 4.00f && effectend)
				{
					Application.LoadLevel("Score");
				}

                if (color.a >= 1 && onlyfadein == false)
                {
                    fadeout = false;

                    color.a = 1;
                    fadeLayer.GetComponent<SpriteRenderer>().color = color;

                    if (nextStage == MapControlManager.EARTH_STAGE)
                    {
                        Color earthColor;

                        earthColor.r = 0.274f;
                        earthColor.g = 0.764f;
                        earthColor.b = 0.96f;
                        earthColor.a = 1.0f;

                        colorLayer.GetComponent<SpriteRenderer>().color = earthColor;

                        bigLayer.GetComponent<MeshRenderer>().material = bigEarth;
                        smallLayer.GetComponent<MeshRenderer>().material = smallEarth;
                    }

                    else if (nextStage == MapControlManager.SPACE_STAGE)
                    {
                        Color spaceColor;

                        spaceColor.r = 0.043f;
                        spaceColor.g = 0.074f;
                        spaceColor.b = 0.113f;
                        spaceColor.a = 1.0f;

                        colorLayer.GetComponent<SpriteRenderer>().color = spaceColor;

                        bigLayer.GetComponent<MeshRenderer>().material = bigSpace;
                        smallLayer.GetComponent<MeshRenderer>().material = smallSpace;
                    }

                    if (onlyfadein == false) fadein = true;
                }
            }
        }

        if (fadein)
        {
            color = fadeLayer.GetComponent<SpriteRenderer>().color;
            color.a -= 0.01f;
            fadeLayer.GetComponent<SpriteRenderer>().color = color;

            if (color.a <= 0)
            {
                fadein = false;
                color.a = 0;
                fadeLayer.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

	public void FadeIn()
	{
		fadeout = true;
		onlyfadein = true;
	}

    public void stageChange(int nextStage)
    {
        this.nextStage = nextStage;
        this.fadeout = true;
    }

    public void feverChange()
    {
        Color feverColor;

        feverColor.r = 1.0f;
        feverColor.g = 1.0f;
        feverColor.b = 1.0f;
        feverColor.a = 1.0f;

        bigLayer.GetComponent<MeshRenderer>().material = bigFever;
        smallLayer.GetComponent<MeshRenderer>().material = smallFever;

        bigLayer.GetComponent<Scroll>().setUvOffset(new Vector2(0.0f, 0.0f));
        smallLayer.GetComponent<Scroll>().setUvOffset(new Vector2(0.0f, 0.0f));

        bigLayer.renderer.material.SetColor("_Color", feverColor);
        smallLayer.renderer.material.SetColor("_Color", feverColor);

        gameManager.GetComponent<MapControlManager>().setGameMode(MapControlManager.FEVER_STAGE);
    }

    public void prevStage(int prev)
    {
        bigLayer.renderer.material.SetColor("_Color", gameManager.GetComponent<MapControlManager>().getBigLayerPrevColor());
        smallLayer.renderer.material.SetColor("_Color", gameManager.GetComponent<MapControlManager>().getSmallLayerPrevColor());
        alphaLayer.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<MapControlManager>().getAlphaLayerPrevColor();

        bigLayer.GetComponent<Scroll>().setUvOffset(gameManager.GetComponent<MapControlManager>().getPrevBigLayerOffset());
        smallLayer.GetComponent<Scroll>().setUvOffset(gameManager.GetComponent<MapControlManager>().getPrevSmallLayerOffset());

        if (prev == MapControlManager.EARTH_STAGE)
        {
            bigLayer.GetComponent<MeshRenderer>().material = bigEarth;
            smallLayer.GetComponent<MeshRenderer>().material = smallEarth;
        }

        else if (prev == MapControlManager.SPACE_STAGE)
        {
            bigLayer.GetComponent<MeshRenderer>().material = bigSpace;
            smallLayer.GetComponent<MeshRenderer>().material = smallSpace;
        }

        gameManager.GetComponent<MapControlManager>().setGameMode(prev);
    }
}