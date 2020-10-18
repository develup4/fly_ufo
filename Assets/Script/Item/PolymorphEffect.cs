using UnityEngine;
using System.Collections;

public class PolymorphEffect : MonoBehaviour {
    private struct ItemLocation
    {
        public float locationX;
        public float locationY;
    }

    public float width;
    public float height;

    private int i, j, row, column;
    private float startX, startY;
    private float changeItemWidth, changeItemHeight;
    private float widthGap = 10.0f, heightGap = 10.0f;
    private ItemLocation[] itemLocation;

    public void polymorph(GameObject changeItem)
    {
        ItemLocation[] itemLocation = new ItemLocation[row * column];

        startX = gameObject.transform.position.x - (width / 2.0f / 100.0f);
        startY = gameObject.transform.position.y + (height / 2.0f / 100.0f);

        for(i=0; i<row; i++)
        {
            for(j=0; j<column; j++)
            {
                itemLocation[i*column + j].locationX = startX + ((j * (changeItemWidth + widthGap)) / 100.0f);
                itemLocation[i*column + j].locationY = startY - ((i * (changeItemHeight + heightGap)) / 100.0f);
            }
        }

        for(i=0; i<row*column; i++)
        {
            GameObject polymorphItem;

            polymorphItem = (GameObject)Instantiate(changeItem, new Vector3(itemLocation[i].locationX, itemLocation[i].locationY, -1.0f), Quaternion.identity);
            polymorphItem.GetComponent<AcquireItem>().setPolymorphItem(true);
        }
    }

    public void setChangeItemSize(float width, float height)
    {
        this.changeItemWidth = width;
        this.changeItemHeight = height;

        startX += (changeItemWidth / 2.0f / 100.0f);
        startY -= (changeItemHeight / 2.0f / 100.0f);

        row = (int)(this.height / (changeItemHeight + widthGap));
        column = (int)(this.width / (changeItemWidth + heightGap));
    }
}
