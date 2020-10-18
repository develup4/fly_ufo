using UnityEngine;
using System.Collections;

public class FeverBoard : MonoBehaviour {
    
    public GameObject[] feverBoardChild;
    public float feverDuration;
    public float readyTime;

    private bool isGet_B;
    private bool isGet_O;
    private bool isGet_N;
    private bool isGet_U;
    private bool isGet_S;
    private bool isFeverMode;
    private bool isEnd;
    private float remainTime;
    private FeverBoardChild[] childComponent;

    private GameObject gameManager;
    private GameObject ufo;
    private GameObject effectManager;

    void Awake()
    {
        int i;
        childComponent = new FeverBoardChild[5];

        for(i=0; i<5; i++)
        {
            childComponent[i] = feverBoardChild[i].GetComponent<FeverBoardChild>();
        }

        gameManager = GameObject.Find("GameManager");
        ufo = GameObject.Find("UFO");
        effectManager = GameObject.Find("EffectManager");
    }

    void check()
    {
        if(isGet_B && isGet_O && isGet_N && isGet_U && isGet_S)
        {
            isFeverMode = true;
            isEnd = false;
            remainTime = feverDuration;

            for(int i=0; i<5; i++)
                childComponent[i].GetComponent<SkeletonAnimation>().timeScale = 5f / feverDuration;

            // 피버 스테이지로 이동
            gameManager.GetComponent<StageChange>().feverStage();
            GameObject.Find("FlightBoard").GetComponent<FlightBoard>().setIsFeverMode(true);
        }
    }

    void Update()
    {
        if(gameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.FEVER_STAGE)
        {
            remainTime -= Time.deltaTime;
            if(remainTime > feverDuration * 4 / 5)
            {
                childComponent[4].startAnimation();
            }
            else if(remainTime > feverDuration * 3 / 5)
            {
                childComponent[4].init();
                childComponent[3].startAnimation();
            }
            else if(remainTime > feverDuration * 2 / 5)
            {
                childComponent[3].init();
                childComponent[2].startAnimation();
            }
            else if(remainTime > feverDuration * 1 / 5)
            {
                childComponent[2].init();
                childComponent[1].startAnimation();
            }
            else if(remainTime > 0.0f)
            {
                childComponent[1].init();
                childComponent[0].startAnimation();
            }
            else if(remainTime < -0.1f)
            {
                childComponent[0].init();
                childComponent[1].init();
                childComponent[2].init();
                childComponent[3].init();
                childComponent[4].init();

                isGet_B = false;
                isGet_O = false;
                isGet_N = false;
                isGet_U = false;
                isGet_S = false;
                isFeverMode = false;

                ufo.rigidbody2D.gravityScale = 0.0f;
                ufo.rigidbody2D.velocity = new Vector2(0.0f, 0.0f);

                ufo.GetComponent<UFO>().SetControllable(false);

                if(!isEnd)
                {
                    effectManager.GetComponent<EffectManager>().makeFeverEffect(ufo.transform.position);
                    GameObject.Find("FlightBoard").GetComponent<FlightBoard>().setIsFeverMode(false);
                    isEnd = true;
                }
            }
        }
    }

    public void getItem(char alphabet)
    {
        if(!isFeverMode)
        {
            switch(alphabet)
            {
                case 'B':
                case 'b':
                    childComponent[0].itemGet();
                    isGet_B = true;
                    break;

                case 'O':
                case 'o':
                    childComponent[1].itemGet();
                    isGet_O = true;
                    break;

                case 'N':
                case 'n':
                    childComponent[2].itemGet();
                    isGet_N = true;
                    break;

                case 'U':
                case 'u':
                    childComponent[3].itemGet();
                    isGet_U = true;
                    break;

                case 'S':
                case 's':
                    childComponent[4].itemGet();
                    isGet_S = true;
                    break;
            }
            check();
        }
    }
}
