using UnityEngine;
using System;
using System.Collections;

public class UFO : MonoBehaviour
{
	public SkeletonDataAsset[]	ARRAY_SKELETON;

	public bool					KEYBOARD_MODE;
	public GameObject			ACHIEVEMENT_EFFECT;
	public GameObject			START_EFFECT;
	public GameObject			LEFT_DRIFT_EFFECT;
	public GameObject			RIGHT_DRIFT_EFFECT;
	public GameObject			DESTROY_EFFECT;
	public GameObject			MAX_SPEED_EFFECT;
	public GameObject 			BOOSTER_EFFECT_LEFT;
	public GameObject 			BOOSTER_EFFECT_RIGHT;
	public GameObject			DAMAGE_EFFECT;
	public GameObject			SOUL_EFFECT;

    public UISprite             m_leftBoostButton;
    public UISprite             m_rightBoostButton;

    private GameObject      	m_refGameResult;
    private GameObject      	m_refRightEngine;
    private GameObject      	m_refLeftEngine;
    private GameObject      	m_refRightLauncher;
    private GameObject      	m_refLeftLauncher;
    private GameObject      	m_refCountDown;
    private GameObject          m_refGameManager;
	private GameObject			m_refGameOver;

    private int             	m_nStartingState;           // 시작부 연출 상태

    private float           	m_fRealTime                 = 0.0f;
	private float 				m_fDriftAngle 				= 0.0f;
	private float 				m_fDriftAccumulateAngle 	= 0.0f;
	private float				m_fDriftSpeed				= 1.0f;
	private float				m_fStartDelay				= 0.0f;

    private bool            	m_bStartingPointDirect;     // 시작부 연출 중
    private bool            	m_bIsXTracking;				// 배경 트래킹 여부
    private bool            	m_bIgnoreObjectMode;        // 충돌 무시 모드
    private bool            	m_bBoosterMode;             // 부스터 모드
    private bool            	m_bGiantMode;               // 거대화 모드
    private bool                m_bControllable;            // 조작 가능 여부
	private bool 				m_bDriftRight;				// 우측 드리프트
	private bool 				m_bDriftLeft;				// 좌측 드리프트
	private bool				m_bCollideObstacle;			// 오브젝트 충돌 경험
	private bool				m_bDelayStart;				// 지연 출발 여부
	private bool				m_bCameraTracking;			// 카메라 트래킹 여부
	private bool				m_bGameOver;				// 게임오버 여부

    // 상수
    private const int       	STARTING_COUNT_STATE        = 0;
    private const int       	LOW_SPEED_STATE             = 1;
    private const int       	HIGH_SPEED_STATE            = 2;

    private const int       	LEFT_BUTTON                 = 0;
    private const int       	RIGHT_BUTTON                = 1;
    private const int       	DEFAULT_DAMAGE              = 40;
    private const int       	OBJECT_DESTROY_SCORE        = 125;
    private const float     	ENGINE_DISTANCE             = 5.0f;
    private const float     	SCREEN_WIDTH                = 7.2f;
    private const float     	MAP_HALF_WIDTH              = 10.5f;
    private const float     	START_LOW_SPEED             = 1.0f;
    private const float     	START_HIGH_SPEED            = 40.0f;

    void Start()
	{
        m_refCountDown          = GameObject.Find("CountDown");
        m_refGameResult         = GameObject.Find("Result");
        m_refRightEngine        = GameObject.Find("RightEngine");
        m_refLeftEngine         = GameObject.Find("LeftEngine");
        m_refRightLauncher      = GameObject.Find("Launcher_Right");
        m_refLeftLauncher       = GameObject.Find("Launcher_Left");
        m_refGameManager        = GameObject.Find("GameManager");
		m_refGameOver 			= GameObject.Find("GameOverMessage");

        m_nStartingState        = STARTING_COUNT_STATE;

        m_bStartingPointDirect  = true;
        m_bIsXTracking          = true;
        m_bIgnoreObjectMode     = false;
        m_bGiantMode            = false;
        m_bControllable         = true;
		m_bDriftRight			= false;
		m_bDriftLeft 			= false;
		m_bCollideObstacle 		= false;
		m_bDelayStart 			= false;
		m_bCameraTracking 		= true;
		m_bGameOver 			= false;

		// 우주선 종류 세팅
		this.GetComponent<SkeletonAnimation> ().skeletonDataAsset = ARRAY_SKELETON [CharacterSelectManager.g_UFONumber];
	}

    void Update()
    {
		ACHIEVEMENT_EFFECT.transform.position = this.transform.position;

		// 10초간 미조작시 업적 달성
		if (m_bDelayStart == false)
		{
			m_fStartDelay += Time.deltaTime;

			if (m_fStartDelay >= 10.0f)
			{
				m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.DELAY_START);
				m_bDelayStart = true;
			}
		}

        // 시작부 연출 처리
        if (m_bStartingPointDirect)
        {
            Vector2 vecStartVelocity = this.rigidbody2D.velocity;
            switch (m_nStartingState)
            {
                case STARTING_COUNT_STATE:
                    m_refCountDown.GetComponent<SkeletonAnimation>().animationName = "animation";

                    // 발사대 넘어뜨리기
                    System.Random random = new System.Random((int)(DateTime.Now.Ticks));
                    int nRandomRightForce = random.Next(0, 100);
                    int nRandomLeftForce = random.Next(0, 100);

                    m_refRightLauncher.GetComponent<Rigidbody2D>().AddForce(new Vector2(100.0f + (float)nRandomRightForce, 0.0f));
                    m_refLeftLauncher.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100.0f - (float)nRandomLeftForce, 0.0f));

					m_nStartingState = LOW_SPEED_STATE;

					START_EFFECT.SetActive(true);
					START_EFFECT.GetComponent<ParticleSystem>().Play();
                    
					return;

                case LOW_SPEED_STATE:
                    m_fRealTime += Time.deltaTime;
                    if (m_fRealTime > 2.7f)
                    {
                        m_nStartingState = HIGH_SPEED_STATE;
                        m_fRealTime = 0.0f;

                        vecStartVelocity.y = START_HIGH_SPEED;
                        this.rigidbody2D.velocity = vecStartVelocity;

						START_EFFECT.SetActive(true);
						START_EFFECT.GetComponent<ParticleSystem>().Play();
						START_EFFECT.audio.Play();

                        return;
                    }

                    vecStartVelocity.y = START_LOW_SPEED;
                    this.rigidbody2D.velocity = vecStartVelocity;

                    // 엔진 애니메이션
                    m_refLeftEngine.GetComponent<EngineAnimation>().SetAnimationState(EngineAnimation.ENGINE_TURNON);
                    m_refRightEngine.GetComponent<EngineAnimation>().SetAnimationState(EngineAnimation.ENGINE_TURNON);

                    return;

                case HIGH_SPEED_STATE:
                    m_fRealTime += Time.deltaTime;
                    if (m_fRealTime > 0.5f)
                    {
                        m_bStartingPointDirect = false;
                        m_fRealTime = 0.0f;
                    }

                    // 엔진 애니메이션
                    m_refLeftEngine.GetComponent<EngineAnimation>().SetAnimationState(EngineAnimation.ENGINE_ON);
                    m_refRightEngine.GetComponent<EngineAnimation>().SetAnimationState(EngineAnimation.ENGINE_ON);

                    return;
            }
        }

        // 오브젝트 무시 처리 복원
        if (GetComponent<UFO_Animation>().getAnimationState() == UFO_Animation.NORMAL_STATE && m_bBoosterMode == false && m_bGiantMode == false)
        {
            m_bIgnoreObjectMode = false;
        }

        // 시간마다 연료 감소
		if (m_refGameManager.GetComponent<MapControlManager>().getGameMode() != MapControlManager.FEVER_STAGE)
		{
			GetComponent<UFO_Attribute>().currentHP -= ((100 - GetComponent<UFO_Attribute> ().mileage) * Time.deltaTime);
			if (GetComponent<UFO_Attribute>().currentHP <= 0.0f)
			{
				// 게임오버 처리
				if (m_bGameOver == false) GameOver();
			}
		}

        // 좌우 벽처리
		Vector3 vecPosition = this.transform.position;
		Vector2 vecVelocity = this.rigidbody2D.velocity;

		if (m_refGameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.FEVER_STAGE)
		{
			float feverXPosition = m_refGameManager.GetComponent<MapControlManager> ().getFeverXPosition ();
			m_bIsXTracking = false;

			if (this.transform.position.x < (feverXPosition - SCREEN_WIDTH / 2.0f) + 1.4f)
			{
				vecPosition = this.transform.position;
				vecPosition.x = (feverXPosition - SCREEN_WIDTH / 2.0f) + 1.4f;
				this.rigidbody2D.transform.position = vecPosition;
	
				vecVelocity.x = 0.0f;
				this.rigidbody2D.velocity = vecVelocity;
			}
			else if (this.transform.position.x > (feverXPosition + SCREEN_WIDTH / 2.0f) - 1.4f)
			{
				vecPosition = this.transform.position;
				vecPosition.x = (feverXPosition + SCREEN_WIDTH / 2.0f) - 1.4f;
				this.rigidbody2D.transform.position = vecPosition;
	
				vecVelocity.x = 0.0f;
				this.rigidbody2D.velocity = vecVelocity;
			}
		}
		else 
		{	
			if (vecPosition.x < -SCREEN_WIDTH)
			{
				m_bIsXTracking = false;
				if (this.transform.position.x < -MAP_HALF_WIDTH)
				{
					vecPosition = this.transform.position;
					vecPosition.x = -MAP_HALF_WIDTH;
					this.rigidbody2D.transform.position = vecPosition;
					
					vecVelocity.x = 0.0f;
					this.rigidbody2D.velocity = vecVelocity;
				}
			}
			else if (vecPosition.x > SCREEN_WIDTH)
			{
				m_bIsXTracking = false;
				if (this.transform.position.x > MAP_HALF_WIDTH)
				{
					vecPosition = this.transform.position;
					vecPosition.x = MAP_HALF_WIDTH;
					this.rigidbody2D.transform.position = vecPosition;
					
					vecVelocity.x = 0.0f;
					this.rigidbody2D.velocity = vecVelocity;
				}
			}
			else if (this.transform.position.x <= SCREEN_WIDTH && this.transform.position.x >= -SCREEN_WIDTH)
			{
				m_bIsXTracking = true;
			}
		}

		// 엔진 처리
        if (m_bControllable)
        {
			if (KEYBOARD_MODE == false)
			{
	            if (Input.touchCount == 1)
	            {
					if (m_bDelayStart == false) m_bDelayStart = true;

	                if (Input.GetTouch(0).position.x < SCREEN_WIDTH / 2 * 100.0f)
	                {
	                    vecPosition = this.transform.position;
	                    vecPosition.z = -1.0f;
	                    vecPosition.x += ENGINE_DISTANCE;
	                    this.rigidbody2D.AddForceAtPosition(this.transform.rotation * Vector2.up * GetComponent<UFO_Attribute>().accel * 0.5f, vecPosition);

	                    this.rigidbody2D.AddForce(Vector2.up * GetComponent<UFO_Attribute>().accel);

	                    // 최대속력 처리
	                    if (this.rigidbody2D.velocity.y > GetComponent<UFO_Attribute>().maxMoveSpeed)
	                    {
							//MAX_SPEED_EFFECT.SetActive(true);
							//MAX_SPEED_EFFECT.GetComponent<ParticleSystem>().Play();

	                        Vector2 vecSpeed = this.rigidbody2D.velocity;
	                        vecSpeed.y = GetComponent<UFO_Attribute>().maxMoveSpeed;
	                        this.rigidbody2D.velocity = vecSpeed;
	                    }

	                    // 엔진 애니메이션
	                    m_refRightEngine.GetComponent<EngineAnimation>().EngineOn(true);
	                    m_refLeftEngine.GetComponent<EngineAnimation>().EngineOn(false);
	                }
	                else
	                {
	                    vecPosition = this.transform.position;
	                    vecPosition.z = -1.0f;
	                    vecPosition.x -= ENGINE_DISTANCE;
	                    this.rigidbody2D.AddForceAtPosition(this.transform.rotation * Vector2.up * GetComponent<UFO_Attribute>().accel * 0.5f, vecPosition);

	                    this.rigidbody2D.AddForce(Vector2.up * GetComponent<UFO_Attribute>().accel);

	                    // 최대속력 처리
	                    if (this.rigidbody2D.velocity.y > GetComponent<UFO_Attribute>().maxMoveSpeed)
	                    {
	                        Vector2 vecSpeed = this.rigidbody2D.velocity;
	                        vecSpeed.y = GetComponent<UFO_Attribute>().maxMoveSpeed;
	                        this.rigidbody2D.velocity = vecSpeed;

							//MAX_SPEED_EFFECT.SetActive(true);
							//MAX_SPEED_EFFECT.GetComponent<ParticleSystem>().Play();
	                    }

	                    // 엔진 애니메이션
	                    m_refLeftEngine.GetComponent<EngineAnimation>().EngineOn(true);
	                    m_refRightEngine.GetComponent<EngineAnimation>().EngineOn(false);
	                }
	            }
	            else if (Input.touchCount > 1)
	            {
					if (m_bDelayStart == false) m_bDelayStart = true;

	                vecPosition = this.transform.position;
	                vecPosition.x -= ENGINE_DISTANCE;
	                vecPosition.z = -1.0f;
	                this.rigidbody2D.AddForceAtPosition(this.transform.rotation * Vector2.up * GetComponent<UFO_Attribute>().accel, vecPosition);

	                // 최대속력 처리
	                if (this.rigidbody2D.velocity.y > GetComponent<UFO_Attribute>().maxMoveSpeed)
	                {
	                    Vector2 vecSpeed = this.rigidbody2D.velocity;
	                    vecSpeed.y = GetComponent<UFO_Attribute>().maxMoveSpeed;
	                    this.rigidbody2D.velocity = vecSpeed;
	                }

	                vecPosition = this.transform.position;
	                vecPosition.x += ENGINE_DISTANCE;
	                vecPosition.z = -1.0f;
	                this.rigidbody2D.AddForceAtPosition(this.transform.rotation * Vector2.up * GetComponent<UFO_Attribute>().accel, vecPosition);

	                // 최대속력 처리
	                if (this.rigidbody2D.velocity.y > GetComponent<UFO_Attribute>().maxMoveSpeed)
	                {
	                    Vector2 vecSpeed = this.rigidbody2D.velocity;
	                    vecSpeed.y = GetComponent<UFO_Attribute>().maxMoveSpeed;
	                    this.rigidbody2D.velocity = vecSpeed;

						//MAX_SPEED_EFFECT.SetActive(true);
						//MAX_SPEED_EFFECT.GetComponent<ParticleSystem>().Play();
	                }

	                // 엔진 애니메이션
	                m_refLeftEngine.GetComponent<EngineAnimation>().EngineOn(true);
	                m_refRightEngine.GetComponent<EngineAnimation>().EngineOn(true);
	            }
	            else if (Input.touchCount == 0)
	            {
	                // 입력없을시 기울기 평형
	                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime);

	                // 엔진 애니메이션
	                m_refLeftEngine.GetComponent<EngineAnimation>().EngineOn(false);
	                m_refRightEngine.GetComponent<EngineAnimation>().EngineOn(false);
	            }
				
				// 더블 터치시 드리프트 처리
				if (Input.touchCount > 0)
				{
					for(int i = 0; i < Input.touchCount; i++)
					{
						if (Input.GetTouch(i).tapCount > 1 && Input.GetTouch(i).tapCount % 2 == 0 && Input.GetTouch(i).phase == TouchPhase.Began)
						{
							if (Input.GetTouch(i).position.x < SCREEN_WIDTH / 2 * 100.0f && (this.rigidbody2D.transform.rotation * Vector2.up).x > 0.0f)
							{
								float fDegree = Mathf.Acos(Vector2.Dot(this.rigidbody2D.transform.rotation * Vector2.up, Vector2.up)) * Mathf.Rad2Deg;

								m_bDriftLeft = true;
								m_fDriftAngle = 2.0f * fDegree;

								break;
							}
							else if (Input.GetTouch(i).position.x >= SCREEN_WIDTH / 2 * 100.0f && (this.rigidbody2D.transform.rotation * Vector2.up).x < 0.0f)
							{
								float fDegree = Mathf.Acos(Vector2.Dot(this.rigidbody2D.transform.rotation * Vector2.up, Vector2.up)) * Mathf.Rad2Deg;

								m_bDriftRight = true;
								m_fDriftAngle = fDegree * 2.0f;

								break;
							}
						}
					}
				}
			}
			else 	// 키보드 모드
			{
	            if (Input.GetKey(KeyCode.RightArrow))
	            {
	                vecPosition = this.transform.position;
	                vecPosition.x -= ENGINE_DISTANCE;
	                vecPosition.z = -1.0f;

	                this.rigidbody2D.AddForceAtPosition(this.transform.rotation * Vector2.up * GetComponent<UFO_Attribute>().accel, vecPosition);

	                // 최대속력 처리
	                if (this.rigidbody2D.velocity.y > GetComponent<UFO_Attribute>().maxMoveSpeed)
	                {
	                    Vector2 vecSpeed = this.rigidbody2D.velocity;
	                    vecSpeed.y = GetComponent<UFO_Attribute>().maxMoveSpeed;
	                    this.rigidbody2D.velocity = vecSpeed;

						//MAX_SPEED_EFFECT.SetActive(true);
						//MAX_SPEED_EFFECT.GetComponent<ParticleSystem>().Play();
	                }

	                // 엔진 애니메이션
	                m_refLeftEngine.GetComponent<EngineAnimation>().EngineOn(true);

                    // 조작키 이미지
                    m_rightBoostButton.spriteName = "ingame_ui_inputkey_right_off";
	            }
	            else
	            {
	                // 엔진 애니메이션
	                m_refLeftEngine.GetComponent<EngineAnimation>().EngineOn(false);

                    // 조작키 이미지
                    m_rightBoostButton.spriteName = "ingame_ui_inputkey_right_on";
	            }
				if (Input.GetKey(KeyCode.LeftArrow))
	            {
					if (m_bDelayStart == false) m_bDelayStart = true;

	                vecPosition = this.transform.position;
	                vecPosition.x += ENGINE_DISTANCE;
	                vecPosition.z = -1.0f;
	                this.rigidbody2D.AddForceAtPosition(this.transform.rotation * Vector2.up * GetComponent<UFO_Attribute>().accel, vecPosition);

	                // 최대속력 처리
	                if (this.rigidbody2D.velocity.y > GetComponent<UFO_Attribute>().maxMoveSpeed)
	                {
	                    Vector2 vecSpeed = this.rigidbody2D.velocity;
	                    vecSpeed.y = GetComponent<UFO_Attribute>().maxMoveSpeed;
	                    this.rigidbody2D.velocity = vecSpeed;

						//MAX_SPEED_EFFECT.SetActive(true);
						//MAX_SPEED_EFFECT.GetComponent<ParticleSystem>().Play();
	                }

	                m_refRightEngine.GetComponent<EngineAnimation>().EngineOn(true);

                    // 조작키 이미지
                    m_leftBoostButton.spriteName = "ingame_ui_inputkey_left_off";
	            }
	            else
	            {
	                // 엔진 애니메이션
	                m_refRightEngine.GetComponent<EngineAnimation>().EngineOn(false);

                    // 조작키 이미지
                    m_leftBoostButton.spriteName = "ingame_ui_inputkey_left_on";
	            }

				// 입력없을시 기울기 평형
				if (Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) == false)
				{
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime);
					
					// 엔진 애니메이션
					m_refLeftEngine.GetComponent<EngineAnimation>().EngineOn(false);
					m_refRightEngine.GetComponent<EngineAnimation>().EngineOn(false);
				}

				// 드리프트 키입력
				if (Input.GetKeyDown(KeyCode.Space))
				{
					if ((this.rigidbody2D.transform.rotation * Vector2.up).x > 0.0f)
					{
						float fDegree = Mathf.Acos(Vector2.Dot(this.rigidbody2D.transform.rotation * Vector2.up, Vector2.up)) * Mathf.Rad2Deg;
						
						m_bDriftLeft = true;
						m_fDriftAngle = 2.0f * fDegree;
					}
					else
					{
						float fDegree = Mathf.Acos(Vector2.Dot(this.rigidbody2D.transform.rotation * Vector2.up, Vector2.up)) * Mathf.Rad2Deg;
						
						m_bDriftRight = true;
						m_fDriftAngle = fDegree * 2.0f;
					}
				}
			}
            
			// 드리프트 처리
			if (m_bDriftLeft)
			{
				this.rigidbody2D.transform.Rotate(Vector3.forward, m_fDriftAngle * Time.deltaTime * m_fDriftSpeed);
				m_fDriftAccumulateAngle += m_fDriftAngle * Time.deltaTime * m_fDriftSpeed;
				
				m_fDriftSpeed++;
				
				if (m_fDriftAngle < m_fDriftAccumulateAngle)
				{
					m_bDriftLeft = false;
					m_fDriftAngle = 0.0f;
					m_fDriftAccumulateAngle = 0.0f;
					m_fDriftSpeed = 1.0f;
				}

				RIGHT_DRIFT_EFFECT.SetActive(true);
				RIGHT_DRIFT_EFFECT.GetComponent<ParticleSystem>().Play();
				if (RIGHT_DRIFT_EFFECT.audio.isPlaying == false)
				{
					RIGHT_DRIFT_EFFECT.audio.Play();
				}
			}
			else if (m_bDriftRight)
			{
				this.rigidbody2D.transform.Rotate(Vector3.forward * -1.0f, m_fDriftAngle * Time.deltaTime * m_fDriftSpeed);
				m_fDriftAccumulateAngle += m_fDriftAngle * Time.deltaTime * m_fDriftSpeed;
				
				m_fDriftSpeed++;
				
				if (m_fDriftAngle < m_fDriftAccumulateAngle)
				{
					m_bDriftRight = false;
					m_fDriftAngle = 0.0f;
					m_fDriftAccumulateAngle = 0.0f;
					m_fDriftSpeed = 1.0f;
				}

				LEFT_DRIFT_EFFECT.SetActive(true);
				LEFT_DRIFT_EFFECT.GetComponent<ParticleSystem>().Play();
				LEFT_DRIFT_EFFECT.GetComponent<ParticleSystem>().Play();
				if (LEFT_DRIFT_EFFECT.audio.isPlaying == false)
				{
					LEFT_DRIFT_EFFECT.audio.Play();
				}
			}
        }

		// 좌우 최대각 처리
		Vector3 vecAngle = this.rigidbody2D.transform.eulerAngles;
		if (vecAngle.z > 45.0f && vecAngle.z < 180.0f)
		{
			vecAngle.z = 45.0f;
			this.rigidbody2D.transform.eulerAngles = vecAngle;
		}
		if (vecAngle.z > 180.0f && vecAngle.z < 315.0f)
		{
			vecAngle.z = 315.0f;
			this.rigidbody2D.transform.eulerAngles = vecAngle;
		}

        // 부스터 모드일때
        if (m_bBoosterMode)
        {
            if (m_refGameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.EARTH_STAGE ||
                m_refGameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.SPACE_STAGE)
            {
                vecVelocity = this.rigidbody2D.velocity;
                vecVelocity.y = GetComponent<UFO_Attribute>().maxMoveSpeed * 1.5f;
                this.rigidbody2D.velocity = vecVelocity;
            }
        }

		// 피버모드시 속도 고정
        if (m_refGameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.FEVER_STAGE)
        {
            vecVelocity = this.rigidbody2D.velocity;
            vecVelocity.y = 25.0f;
            this.rigidbody2D.velocity = vecVelocity;
		}

		// 업적체크
		if (m_refGameResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.FLY_DISTANCE_100) == false && this.transform.position.y > 1000.0f)
		{
			m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.FLY_DISTANCE_100);
		}
		if (m_refGameResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.FLY_DISTANCE_1000) == false && this.transform.position.y > 10000.0f)
		{
			m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.FLY_DISTANCE_1000);
		}
		if (m_refGameResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.FLY_DISTANCE_10000) == false && this.transform.position.y > 100000.0f)
		{
			m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.FLY_DISTANCE_10000);
		}
		if (m_bCollideObstacle == false && m_refGameResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.NOT_COLLIDE_OBSTACLE_30) == false && this.transform.position.y > 300.0f)
		{
			m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.NOT_COLLIDE_OBSTACLE_30);
		}
		if (m_bCollideObstacle == false && m_refGameResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.NOT_COLLIDE_OBSTACLE_100) == false && this.transform.position.y > 1000.0f)
		{
			m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.NOT_COLLIDE_OBSTACLE_100);
		}
		if (m_refGameResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.LUCKY_SEVEN) == false && this.transform.position.y > 7770.0f)
		{
			m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.LUCKY_SEVEN);
		}
    }

    public void CollideObject(bool bMeteor, bool bBlackHall)
    {
        if (m_bIgnoreObjectMode == false)
        {
			// 이펙트
			DAMAGE_EFFECT.SetActive(true);
			DAMAGE_EFFECT.GetComponent<ParticleSystem>().Play();

            // 체력 감소
            float fDamage = DEFAULT_DAMAGE;

            // 최대 속력의 75% 이상일 경우 1.5배의 데미지
            if (this.rigidbody2D.velocity.y >= GetComponent<UFO_Attribute>().maxMoveSpeed * 0.75f)
            {
                fDamage *= 1.5f;
            }

            // 방어력 처리
            fDamage -= GetComponent<UFO_Attribute>().armor;
            if (fDamage <= 0.0f)
            {
                fDamage = 1.0f;
            }

            // 체력 감소 처리
            GetComponent<UFO_Attribute>().currentHP -= fDamage;

            if (GetComponent<UFO_Attribute>().currentHP <= 0.0f)
            {
                GetComponent<UFO_Attribute>().currentHP = 0.0f;

				if (bMeteor && m_refGameResult.GetComponent<Result>().GetAccomplishment(AchievementNumberConstant.DESTROY_BY_METEOR) == false)
				{
					m_refGameResult.GetComponent<Result>().AccomplishAchievement(AchievementNumberConstant.DESTROY_BY_METEOR);
				}

                // 게임오버 처리
				if (m_bGameOver == false)
				{
					GameOver();
					return;
				}
            }

            // 속도 감소 처리
            if (!bBlackHall)
            {
                this.rigidbody2D.velocity *= 0.2f;
            }

			// 충돌 경험
			m_bCollideObstacle = true;
        }
    }

    public void GameOver()
    {
		m_bGameOver = true;

        m_refGameManager.GetComponent<MapControlManager>().setPatternOriginalPosition();

		m_refGameOver.GetComponent<MeshRenderer> ().enabled = true;
		m_refGameOver.GetComponent<SkeletonAnimation>().animationName = "animation";

		// 이펙트

		MAX_SPEED_EFFECT.SetActive (false);
		DESTROY_EFFECT.SetActive(true);
		DESTROY_EFFECT.GetComponent<ParticleSystem>().Play();
		audio.Play ();
		
		// 포물선으로 떨어지도록
		this.rigidbody2D.velocity = new Vector2(0.0f, 10.0f);

		GameObject.Find ("AchievementMessage").SetActive (false);
		GameObject.Find ("UI Root").SetActive (false);
		GameObject.Find ("MoneyBoard").SetActive (false);
		GameObject.Find ("PauseButton").SetActive (false);
		GameObject.Find ("ScoreBoard").SetActive (false);
		GameObject.Find ("FeverBoard").SetActive (false);
		GameObject.Find ("FlightBoard").SetActive (false);

		m_bCameraTracking = false;
		m_bControllable = false;

		GetComponent<SkeletonAnimation>().animationName = "crash";
		GetComponent<SkeletonAnimation>().loop = true;

		Vector3 vec = GameObject.Find ("FadeLayer").transform.position;
		vec.z = -2.0f;
		GameObject.Find ("FadeLayer").transform.position = vec;
		GameObject.Find ("FadeLayer").GetComponent<StageChange>().FadeIn();
    }

	public void MakeAchievementEffect()
	{
		if (m_bGameOver == false)
		{
			ACHIEVEMENT_EFFECT.SetActive(true);
			ACHIEVEMENT_EFFECT.GetComponent<ParticleSystem>().Play();
			m_refLeftEngine.audio.Play ();
		}
	}

	// Access Function
    public bool GetIsXTracking()                        {   return m_bIsXTracking;              }
    public bool GetIsBoosterMode()                      {   return m_bBoosterMode;              }
    public bool GetIsGiantMode()                        {   return m_bGiantMode;                }
    public bool GetControllable()                       {   return m_bControllable;             }
	public bool GetStartingPointDirecting()				{	return m_bStartingPointDirect;		}
	public bool GetCameraTracking()						{	return m_bCameraTracking;			}
    public bool GetIsGameOver()                         {   return m_bGameOver;                 }
    public void SetIsXTracking(bool bMode)              {   m_bIsXTracking = bMode;             }
    public void SetIgnoreObjectMode(bool bMode)         {   m_bIgnoreObjectMode = bMode;        }
    public void SetBoosterMode(bool bMode)              {   m_bBoosterMode = bMode;             }
    public void SetGiantMode(bool bMode)                {   m_bGiantMode = bMode;               }
    public void SetControllable(bool bMode)             {   m_bControllable = bMode;            }
}