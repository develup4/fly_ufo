using UnityEngine;
using System.Collections;

public class FeverItem : MonoBehaviour {
    
    public string character;

    private int i;

    private FeverBoard feverBoard;

    void Awake()
    {
        feverBoard = GameObject.Find("FeverBoard").GetComponent<FeverBoard>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Player"))
        {
            if (!GetComponent<AcquireItem>().getIsCrash())
            {
                for (i = 0; i < character.Length; i++)
                {
                    feverBoard.getItem(character[i]);
                }
            }
        }
    }
}
