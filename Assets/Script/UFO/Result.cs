using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour
{
    public 	int 			score;
    public 	int 			money;
    private ScoreBoard 		scoreBoard;
    private MoneyBoard 		moneyBoard;
	private bool[]			m_arrAchievement;		// 업적 달성 저장
	private GameObject		m_refAchievementTable;
	private GameObject		m_refAchievementMessage;
	private GameObject		m_refUFO;

    void Awake()
    {
        scoreBoard 				= GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
        moneyBoard 				= GameObject.Find("MoneyBoard").GetComponent<MoneyBoard>();
		m_refAchievementTable 	= GameObject.Find("AchievementTable");
		m_refAchievementMessage	= GameObject.Find("AchievementMessage");
		m_refUFO 				= GameObject.Find ("UFO");
		m_arrAchievement 		= new bool[m_refAchievementTable.GetComponent<AchievementTable>().Count];
    }

    public void renewScore()
    {
        scoreBoard.renew();
        moneyBoard.renew();

		if (GetAccomplishment (AchievementNumberConstant.GAMEMONEY_10000) == false && money >= 10000)
		{
			AccomplishAchievement(AchievementNumberConstant.GAMEMONEY_10000);
		}
		if (GetAccomplishment (AchievementNumberConstant.GAMEMONEY_100000) == false && money >= 100000)
		{
			AccomplishAchievement(AchievementNumberConstant.GAMEMONEY_100000);
		}
		if (GetAccomplishment (AchievementNumberConstant.GAMEMONEY_1000000) == false && money >= 1000000)
		{
			AccomplishAchievement(AchievementNumberConstant.GAMEMONEY_1000000);
		}
		if (GetAccomplishment (AchievementNumberConstant.GAMEMONEY_10000000) == false && money >= 10000000)
		{
			AccomplishAchievement(AchievementNumberConstant.GAMEMONEY_10000000);
		}
		if (GetAccomplishment (AchievementNumberConstant.LUCKY_SEVEN) == false && money == 777)
		{
			AccomplishAchievement(AchievementNumberConstant.LUCKY_SEVEN);
		}
		if (GetAccomplishment (AchievementNumberConstant.LUCKY_SEVEN) == false && score == 777)
		{
			AccomplishAchievement(AchievementNumberConstant.LUCKY_SEVEN);
		}
    }

	public void AccomplishAchievement(int nAchievementNumber)
	{
		// 반복성 업적이 아닌데 이미 달성한 경우 처리하지 않음(아직 일일은 미개발)
		if (m_arrAchievement[nAchievementNumber] && m_refAchievementTable.GetComponent<AchievementTable>().Repetition[nAchievementNumber] == false)
		{
			return;
		}

		// 체크
		m_arrAchievement[nAchievementNumber] = true;

		// 보상처리(일단 게임머니만 적용)
		money += m_refAchievementTable.GetComponent<AchievementTable>().Reward_GameMoney[nAchievementNumber];
		renewScore();

		// 메시지 표시
		m_refAchievementMessage.GetComponent<AchievementMessage>().ShowMessage(nAchievementNumber, m_refAchievementTable.GetComponent<AchievementTable>().AchievementPoint[nAchievementNumber] / 10);

		// 업적 이펙트 표시
		m_refUFO.GetComponent<UFO>().MakeAchievementEffect();
	}

	public bool GetAccomplishment(int nAchievementNumber)
	{
		return m_arrAchievement[nAchievementNumber];
	}
}

public class AchievementNumberConstant
{
	public const int	GAMEMONEY_10000 							= 10;
	public const int	GAMEMONEY_100000 							= 11;
	public const int	GAMEMONEY_1000000 							= 12;
	public const int	GAMEMONEY_10000000 							= 13;
	public const int	FIRST_COLLIDE_AIRPLANE_ACHIEVEMENT_NUMBER 	= 20;
	public const int	FIRST_COLLIDE_CLOUD_ACHIEVEMENT_NUMBER 		= 21;
	public const int	DELAY_START							 		= 300;
	public const int	DESTROY_BY_METEOR					 		= 301;
	public const int	MANIPULATE_RABBIT					 		= 302;
	public const int	LUCKY_SEVEN							 		= 303;
	public const int	FULL_HP								 		= 304;
	public const int	FLY_DISTANCE_100 							= 600;
	public const int	FLY_DISTANCE_1000 							= 601;
	public const int	FLY_DISTANCE_10000 							= 602;
	public const int	NOT_COLLIDE_OBSTACLE_30 					= 610;
	public const int	NOT_COLLIDE_OBSTACLE_100 					= 611;
	
}