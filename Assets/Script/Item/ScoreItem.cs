using UnityEngine;
using System.Collections;

public class ScoreItem : MonoBehaviour {

    public Sprite[] scoreItem;

    private int scoreLevel;
    private const int basicScore = 23;

	void Awake() {
        scoreLevel = GameObject.Find("GameManager").GetComponent<MapControlManager>().getItemLevel();
        GetComponent<SpriteRenderer>().sprite = scoreItem[scoreLevel - 1];
        GetComponent<AcquireItem>().score = basicScore + scoreLevel * 3;
    }
}
