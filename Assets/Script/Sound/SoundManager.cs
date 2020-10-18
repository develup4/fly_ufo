using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource normalBGM;
    public AudioSource feverBGM;

    private GameObject gameManager;

    private bool isNormalBGM;
    private bool isFeverBGM;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        
        normalBGM.Play();

        isNormalBGM = true;
        isFeverBGM = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.EARTH_STAGE ||
            gameManager.GetComponent<MapControlManager>().getGameMode() == MapControlManager.SPACE_STAGE)
        {
            if (isNormalBGM == true)
            {
                feverBGM.Stop();
                normalBGM.Play();
                isNormalBGM = false;
                isFeverBGM = true;
            }
        }

        else
        {
            if (isFeverBGM == true)
            {
                normalBGM.Stop();
                feverBGM.Play();
                isFeverBGM = false;
                isNormalBGM = true;
            }
        }
	}

    public void SoundPlay(AudioSource audio)
    {
        audio.Play();
    }
}
