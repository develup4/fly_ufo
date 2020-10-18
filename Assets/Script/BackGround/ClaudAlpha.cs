using UnityEngine;
using System.Collections;

public class ClaudAlpha : MonoBehaviour
{
    private Color color;
    private GameObject gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("UFO");
        color = renderer.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        float length;

        length = transform.position.y - (12.8f * 3.0f);

        if (length > 0.0f)
        {
            if (length / 10 < 1.0f)
            {
                color.a = length / 10;
                renderer.material.SetColor("_Color", color);
            }
        }
    }
}
