using UnityEngine;
using System.Collections;

public class BoostButton : MonoBehaviour {
    public Sprite leftButton_image_on;
    public Sprite leftButton_image_off;
    public Sprite rightButton_image_on;
    public Sprite rightButton_image_off;
    public GameObject leftButton;
    public GameObject rightButton;

    private SpriteRenderer leftButton_Image;
    private SpriteRenderer rightButton_Image;
    private const float SCREEN_WIDTH = 7.2f;

    void Awake()
    {
        leftButton_Image = leftButton.GetComponent<SpriteRenderer>();
        rightButton_Image = rightButton.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).position.x < Screen.width / 2)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    leftButton_Image.sprite = leftButton_image_off;
                }

                if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    leftButton_Image.sprite = leftButton_image_on;
                }
            }
            else
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    rightButton_Image.sprite = rightButton_image_off;
                }

                if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    rightButton_Image.sprite = rightButton_image_on;
                }
            }

            if(Input.touchCount > 1)
            {
                if(Input.GetTouch(1).position.x < Screen.width / 2)
                {
                    if(Input.GetTouch(1).phase == TouchPhase.Began)
                    {
                        leftButton_Image.sprite = leftButton_image_off;
                    }

                    if(Input.GetTouch(1).phase == TouchPhase.Ended)
                    {
                        leftButton_Image.sprite = leftButton_image_on;
                    }
                }
                else
                {
                    if(Input.GetTouch(1).phase == TouchPhase.Began)
                    {
                        rightButton_Image.sprite = rightButton_image_off;
                    }

                    if(Input.GetTouch(1).phase == TouchPhase.Ended)
                    {
                        rightButton_Image.sprite = rightButton_image_on;
                    }
                }
            }
        }
    }
}
