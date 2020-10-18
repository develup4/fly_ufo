using UnityEngine;
using System.Collections;

public class MapControlManager : MonoBehaviour {

    public GameObject[] earthEazyPattern;
    public GameObject[] earthDifficultPattern;
    public GameObject[] spaceEazyPattern;
    public GameObject[] spaceDifficultPattern;
    public GameObject[] feverPattern;
    public GameObject[] bonusPattern;

    public GameObject warningObject;

    public int ItemLevel;

    public float spaceStageChangePosition;

    public float earthDifficultPosition;
    public float earthMiddlePosition;

    public float spaceDifficultPosition;
    public float spaceMiddlePosition;

    private GameObject ufo;
    private GameObject alphaLayer;                  // 밤, 낮 바꾸기 위해
    private GameObject colorLayer;
    private GameObject fadeLayer;
    private GameObject bigLayer;
    private GameObject smallLayer;
    private GameObject effectManager;

    private int GameMode;

    public const int EARTH_STAGE = 0;
	public const int SPACE_STAGE = 1;
	public const int FEVER_STAGE = 2;
	public const int CHANGE_STAGE = 3;

    private float makePatternMomentYPosition;           // 패턴이 만들어지는 위치
    private float makePatternYPosition;                 // 패턴이 생성되는 위치

    private float makeMovingObstacleMomentYPosition;    // 이동 오브젝트 만들어지는 위치
    private float makeMovingObstacleYPosition;          // 이동 오브젝트 생성되는 위치

    private float makeFeverPatternMomentYPosition;      // 피버 패턴 만들어지는 위치
    private float makeFeverPatternYPosition;            // 피버 패턴 생성되는 위치

    private float makeBonusPatternMomentYPosition;

    private int[] easyPatternOrder = {0, 1, 2, 3, 4};
    private int[] difficultPatternOrder = {0, 1, 2, 3, 4};
    private int easyPatternCount = 0;
    private int difficultPatternCount = 0;
    private int patternTypeCount = 0;

    private bool prevPatternType = false;
    private bool patternType = false;

    private int feverPatternCount = 0;

    private int probability;
    private int xPositionSelect;

    private float xDistance;
    private float yDistance;

    private float initPosition;

    private int prevStage;
    private Vector3 prevUfoPosition;
    private Vector2 prevSpeed;
    private Vector2 bigLayerPrevOffset;
    private Vector2 smallLayerPrevOffset;
    private Color bigLayerPrevColor;
    private Color smallLayerPrevColor;
    private Color alphaLayerPrevColor;

    private float feverXPosition;

    private float movingObstacleInterval;

	// Use this for initialization
	void Start () {
        ufo = GameObject.Find("UFO");
        colorLayer = GameObject.Find("ColorLayer");
        alphaLayer = GameObject.Find("AlphaLayer");
        bigLayer = GameObject.Find("BigLayer");
        smallLayer = GameObject.Find("SmallLayer");
        effectManager = GameObject.Find("EffectManager");

        makePatternMomentYPosition = 32.0f;
        makePatternYPosition = makePatternMomentYPosition + 38.4f;

        makeBonusPatternMomentYPosition = makePatternMomentYPosition + (38.4f * 10.0f);

        makeFeverPatternMomentYPosition = -4993.6f;
        makeFeverPatternYPosition = -4993.6f + 38.4f;

		makeMovingObstacleMomentYPosition = 100.0f;

        probability = 50;

        GameMode = EARTH_STAGE;

        patternOrder(easyPatternOrder);
        patternOrder(difficultPatternOrder);

        movingObstacleInterval = 40.0f;
	}
	
	// Update is called once per frame
	void Update () {

        // 패턴 생성 체크
        makePattern(transform.position.y);
        // 장애물 생성 체크
        makeObstacle(ufo.transform.position.y);

        if (ufo.transform.position.y > spaceStageChangePosition && GameMode == EARTH_STAGE)
        {
            GameMode = SPACE_STAGE;
            GetComponent<StageChange>().stageChange(SPACE_STAGE);
        }

        if (ufo.transform.position.y < spaceStageChangePosition && GameMode == SPACE_STAGE)
        {
            GameMode = EARTH_STAGE;
            GetComponent<StageChange>().stageChange(EARTH_STAGE);
        }
	}

    private void setItemOriginalPosition(AcquireItem[] item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            item[i].setOriginalPosition();
            item[i].setIsCrash(false);
        }
    }

    private void setObstacleOriginalPosition(Obstacle[] obstacle)
    {
        for (int i = 0; i < obstacle.Length; i++)
        {
            obstacle[i].setOriginalPosition();
            obstacle[i].setIsCrash(false);
            obstacle[i].setPolymorphObstacle(false);
            obstacle[i].GetComponentInChildren<CrashObstacle>().setIsCrash(false);
            obstacle[i].GetComponentInChildren<CrashObstacle>().setOriginalRotate();
        }
    }

    private void setItemOriginalMagenet(MagnetEffect[] item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            item[i].setIsMagnetEffected(false);
        }
    }

    public void setPatternOriginalPosition()
    {
        for (int i = 0; i < 5; i++)
        {
            earthEazyPattern[i].SetActive(false);
            earthDifficultPattern[i].SetActive(false); ;
            spaceEazyPattern[i].SetActive(false); ;
            spaceDifficultPattern[i].SetActive(false); ;
        }
        for (int i = 0; i < feverPattern.Length; i++)
        {
            feverPattern[i].SetActive(false);
        }

        for (int i = 0; i < bonusPattern.Length; i++)
        {
            bonusPattern[i].SetActive(false);
        }
    }

    // 패턴 만드는 함수
    private void makePattern(float YPosition)
    {
        // 일반 스테이지
        if (YPosition > makePatternMomentYPosition)
        {
            // 보너스 패턴 생성
            if (YPosition > makeBonusPatternMomentYPosition)
            {
                int randomValue;

                randomValue = Random.Range(0, bonusPattern.Length);

                bonusPattern[randomValue].SetActive(true);
                setItemOriginalPosition(bonusPattern[randomValue].GetComponentsInChildren<AcquireItem>());
                setItemOriginalMagenet(bonusPattern[randomValue].GetComponentsInChildren<MagnetEffect>());
                setObstacleOriginalPosition(bonusPattern[randomValue].GetComponentsInChildren<Obstacle>());
                bonusPattern[randomValue].transform.position = new Vector3(0.0f, makePatternYPosition, 0.0f);
                
                makeBonusPatternMomentYPosition += 38.4f * 10.0f;
            }

            else
            {
                if (easyPatternCount == 5)
                {
                    patternOrder(easyPatternOrder);
                    easyPatternCount = 0;
                }
                if (difficultPatternCount == 5)
                {
                    patternOrder(difficultPatternOrder);
                    difficultPatternCount = 0;
                }

                // 지구 패턴 생성
                if (GameMode == EARTH_STAGE)
                {
                    // 어려움
                    if (YPosition > earthDifficultPosition)
                    {
                        earthDifficultPattern[difficultPatternOrder[difficultPatternCount]].SetActive(true);
                        setItemOriginalPosition(earthDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<AcquireItem>());
                        setItemOriginalMagenet(earthDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<MagnetEffect>());
                        setObstacleOriginalPosition(earthDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<Obstacle>());
                        earthDifficultPattern[difficultPatternOrder[difficultPatternCount++]].transform.position = new Vector3(0.0f, makePatternYPosition, 0.0f);
                    }

                    // 중간
                    else if (YPosition > earthMiddlePosition)
                    {
                        if (Random.Range(0, 2) == 1)
                            patternType = false;
                        else
                            patternType = true;

                        if (patternType == prevPatternType)
                            patternTypeCount++;

                        if (patternTypeCount == 2)
                        {
                            patternType = !patternType;
                            patternTypeCount = 0;
                        }

                        if (patternType)
                        {
                            earthDifficultPattern[difficultPatternOrder[difficultPatternCount]].SetActive(true);
                            setItemOriginalPosition(earthDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<AcquireItem>());
                            setItemOriginalMagenet(earthDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<MagnetEffect>());
                            setObstacleOriginalPosition(earthDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<Obstacle>());
                            earthDifficultPattern[difficultPatternOrder[difficultPatternCount++]].transform.position = new Vector3(0.0f, makePatternYPosition, 0.0f);
                        }
                        else
                        {
                            earthEazyPattern[easyPatternOrder[easyPatternCount]].SetActive(true);
                            setItemOriginalPosition(earthEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<AcquireItem>());
                            setItemOriginalMagenet(earthEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<MagnetEffect>());
                            setObstacleOriginalPosition(earthEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<Obstacle>());
                            earthEazyPattern[easyPatternOrder[easyPatternCount++]].transform.position= new Vector3(0.0f, makePatternYPosition, 0.0f);
                        }
                    }

                    // 쉬움
                    else
                    {
                        earthEazyPattern[easyPatternOrder[easyPatternCount]].SetActive(true);
                        setItemOriginalPosition(earthEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<AcquireItem>());
                        setItemOriginalMagenet(earthEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<MagnetEffect>());
                        setObstacleOriginalPosition(earthEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<Obstacle>());
                        earthEazyPattern[easyPatternOrder[easyPatternCount++]].transform.position = new Vector3(0.0f, makePatternYPosition, 0.0f);
                    }
                }

                // 우주 패턴 생성
                else if (GameMode == SPACE_STAGE)
                {
                    // 어려움
                    if (YPosition > spaceDifficultPosition)
                    {
                        spaceDifficultPattern[difficultPatternOrder[difficultPatternCount]].SetActive(true);
                        setItemOriginalPosition(spaceDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<AcquireItem>());
                        setItemOriginalMagenet(spaceDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<MagnetEffect>());
                        setObstacleOriginalPosition(spaceDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<Obstacle>());
                        spaceDifficultPattern[difficultPatternOrder[difficultPatternCount++]].transform.position = new Vector3(0.0f, makePatternYPosition, 0.0f);
                    }

                    // 중간
                    else if (YPosition > spaceMiddlePosition)
                    {
                        if (Random.Range(0, 2) == 1)
                            patternType = false;
                        else
                            patternType = true;

                        if (patternType == prevPatternType)
                            patternTypeCount++;

                        if (patternTypeCount == 2)
                        {
                            patternType = !patternType;
                            patternTypeCount = 0;
                        }

                        if (patternType)
                        {
                            spaceDifficultPattern[difficultPatternOrder[difficultPatternCount]].SetActive(true);
                            setItemOriginalPosition(spaceDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<AcquireItem>());
                            setItemOriginalMagenet(spaceDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<MagnetEffect>());
                            setObstacleOriginalPosition(spaceDifficultPattern[difficultPatternOrder[difficultPatternCount]].GetComponentsInChildren<Obstacle>());
                            spaceDifficultPattern[difficultPatternOrder[difficultPatternCount++]].transform.position = new Vector3(0.0f, makePatternYPosition, 0.0f);
                        }
                        else
                        {
                            spaceEazyPattern[easyPatternOrder[easyPatternCount]].SetActive(true);
                            setItemOriginalPosition(spaceEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<AcquireItem>());
                            setItemOriginalMagenet(spaceEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<MagnetEffect>());
                            setObstacleOriginalPosition(spaceEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<Obstacle>());
                            spaceEazyPattern[easyPatternOrder[easyPatternCount++]].transform.position = new Vector3(0.0f, makePatternYPosition, 0.0f);
                        }
                    }

                    // 쉬움
                    else
                    {
                        spaceEazyPattern[easyPatternOrder[easyPatternCount]].SetActive(true);
                        setItemOriginalPosition(spaceEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<AcquireItem>());
                        setItemOriginalMagenet(spaceEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<MagnetEffect>());
                        setObstacleOriginalPosition(spaceEazyPattern[easyPatternOrder[easyPatternCount]].GetComponentsInChildren<Obstacle>());
                        spaceEazyPattern[easyPatternOrder[easyPatternCount++]].transform.position = new Vector3(0.0f, makePatternYPosition, 0.0f);
                    }
                }
            }

            prevPatternType = patternType;

            makePatternMomentYPosition += 38.4f;
            makePatternYPosition += 38.4f;
        }

        // 피버 스테이지 패턴 생성
        if (YPosition > makeFeverPatternMomentYPosition && GameMode == FEVER_STAGE)
        {
            if (feverPatternCount == 3)
            {
                feverPatternCount = 0;
            }

            feverPattern[feverPatternCount].SetActive(true);
            setItemOriginalPosition(feverPattern[feverPatternCount].GetComponentsInChildren<AcquireItem>());
            setItemOriginalMagenet(feverPattern[feverPatternCount].GetComponentsInChildren<MagnetEffect>());
            setObstacleOriginalPosition(feverPattern[feverPatternCount].GetComponentsInChildren<Obstacle>());
            feverPattern[feverPatternCount++].transform.position = new Vector3(transform.position.x, makeFeverPatternYPosition, 0.0f);

            makeFeverPatternMomentYPosition += 38.4f;
            makeFeverPatternYPosition += 38.4f;
        }
    }

    private void patternOrder(int[] patternOrder)
    {
        int temp;
        int firstIndex, secondIndex;
        int[] prevPatternOrder = new int[5];

        for (int i = 0; i < patternOrder.Length; i++)
        {
            prevPatternOrder[i] = patternOrder[i];
        }

        for (int i = 0; i < patternOrder.Length; i++)
        {
            firstIndex = Random.Range(0, patternOrder.Length);
            secondIndex = Random.Range(0, patternOrder.Length);

            temp = patternOrder[firstIndex];
            patternOrder[firstIndex] = patternOrder[secondIndex];
            patternOrder[secondIndex] = temp;
        }

        while(prevPatternOrder[3] == patternOrder[0] || prevPatternOrder[4] == patternOrder[0])
        {
            for (int i = 0; i < patternOrder.Length; i++)
            {
                firstIndex = Random.Range(0, patternOrder.Length);
                secondIndex = Random.Range(0, patternOrder.Length);

                temp = patternOrder[firstIndex];
                patternOrder[firstIndex] = patternOrder[secondIndex];
                patternOrder[secondIndex] = temp;
            }
        }
    }

    // 이동 오브젝트 만드는 함수
    private void makeObstacle(float YPosition)
    {
        int orderValue;

        if (YPosition >= 0.0f)
        {
            // 해당 높이에 도달했을때
            if (YPosition > makeMovingObstacleMomentYPosition)
            {
                orderValue = Random.Range(0, 100);

                // 지정한 확률이 나오면
                if (orderValue <= probability)
                {
                    // 지구일때
                    if (GameMode == EARTH_STAGE)
                    {
                        if (alphaLayer.GetComponent<AlphaChange>().getDay())
                            makeWarningObstacle(1);
                        else
                            makeWarningObstacle(2);
                    }

                    // 우주일때
                    else if (GameMode == SPACE_STAGE)
                    {
                        if (Random.Range(1, 3) == 1)
                            makeWarningObstacle(2);
                        else
                            makeWarningObstacle(3);
                    }

                    probability = 50;
                }

                // 지정한 확률이 나오지 않으면 확률 올림
                else
                    probability += 25;

                // 다음 이동 오브젝트 등장할 위치 설정
                makeMovingObstacleMomentYPosition += movingObstacleInterval;
            }
        }
    }

    // 경고 메세지 만드는 함수
    private void makeWarningObstacle(int obstacleIndex)
    {
        Vector2 warningPosition;

        // 좌, 우 위치 결정
        if (Random.Range(1, 3) == 1)
            xPositionSelect = -1;
        else
            xPositionSelect = 1;

        // 장애물에 따른 경고메세지 위치 결정
        if (obstacleIndex == 1 || obstacleIndex == 3)
        {
            xDistance = 3.4f * xPositionSelect;
            yDistance = Random.Range(0, 6);
        }
        else if (obstacleIndex == 2)
        {
            xDistance = Random.Range(-3, 3);
            yDistance = 6.2f;
        }

        warningPosition.x = transform.position.x + xDistance;
        warningPosition.y = transform.position.y + yDistance;

        // 경고 메세지 생성
        GameObject warning = (GameObject)Instantiate(warningObject, new Vector3(warningPosition.x, warningPosition.y, 0.0f), Quaternion.identity);
        warning.GetComponent<WarningObstacle>().setObstacleIndex(obstacleIndex);
        warning.GetComponent<WarningObstacle>().setObstacleLeftRight(xPositionSelect);
        warning.GetComponent<WarningObstacle>().setXYDistance(xDistance, yDistance);
    }

    public void saveFeverXPosition(float xPosition)             { this.feverXPosition = xPosition; }
    public void setGameMode(int mode)                           { this.GameMode = mode; }
    public void setUfoSpeed(Vector2 speed)                      { this.prevSpeed = speed; }
    public void setFeverPatternMomentYPosition(float yPosition) { this.makeFeverPatternMomentYPosition = yPosition; }
    public void setFeverPatternYPosition(float yPosition)       { this.makeFeverPatternYPosition = yPosition; }
    public void setPrevStage(int stage)                         { this.prevStage = stage; }
    public void setPrevUfoPosition(Vector3 ufoPosition)         { this.prevUfoPosition = ufoPosition; }
    public void setPrevSpeed(Vector2 velocity)                  { this.prevSpeed = velocity; }
    public void setBigLayerPrevColor(Color color)               { this.bigLayerPrevColor = color; }
    public void setSmallLayerPrevColor(Color color)             { this.smallLayerPrevColor = color; }
    public void setAlphaLayerPrevColor(Color color)             { this.alphaLayerPrevColor = color; }
    public void setBigLayerPrevOffset(Vector2 offset)           { this.bigLayerPrevOffset = offset; }
    public void setSmallLayerPrevOffset(Vector2 offset)         { this.smallLayerPrevOffset = offset; }
    public Vector3 getUfoPrevPosition()                         { return this.prevUfoPosition; }
    public Vector2 getPrevBigLayerOffset()                      { return this.bigLayerPrevOffset; }
    public Vector2 getPrevSmallLayerOffset()                    { return this.smallLayerPrevOffset; }
    public Vector2 getUfoPrevSpeed()                            { return this.prevSpeed; }
    public Color getBigLayerPrevColor()                         { return this.bigLayerPrevColor; }
    public Color getSmallLayerPrevColor()                       { return this.smallLayerPrevColor; }
    public Color getAlphaLayerPrevColor()                       { return this.alphaLayerPrevColor; }
    public float getFeverXPosition()                            { return this.feverXPosition; }
    public int getItemLevel()                                   { return this.ItemLevel; }
    public int getGameMode()                                    { return this.GameMode; }
    public int getPrevStage()                                   { return this.prevStage; }
}