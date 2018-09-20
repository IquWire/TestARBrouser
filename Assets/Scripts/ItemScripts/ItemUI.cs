using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text TitleText;
    public Text DescriptionText;

    public ItemButton Button;
    
    public void SetItem(ItemDataHolder itemData)
    {
        TitleText.text = itemData.TitleString;
        DescriptionText.text = itemData.DescriptionString;
        Button.url = itemData.LinkString;
    }

    public void Resize()
    {
        float sizeTitle = TitleText.rectTransform.sizeDelta.y;
        float sizeDescr = DescriptionText.rectTransform.sizeDelta.y;

        float totalSize = sizeTitle + sizeDescr;

        Vector2 rectSize = new Vector2(TitleText.rectTransform.sizeDelta.x, totalSize);
        
        Button.Resize(rectSize);
    }

    public void ClearItem()
    {
        TitleText.text = String.Empty;
        DescriptionText.text = String.Empty;
        Button.url = String.Empty;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
