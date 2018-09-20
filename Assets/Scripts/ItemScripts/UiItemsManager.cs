using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiItemsManager : MonoBehaviour
{
    public RectTransform Content;
    
    public ItemUI ItemPrefab;
    
    private List<ItemUI> ItemsList;

    private bool _wasCreated;

    private ItemUI CreateItem(ItemDataHolder data)
    {
        ItemUI tempItem = Instantiate(ItemPrefab);
        tempItem.SetItem(data);
        tempItem.transform.SetParent(Content);
        tempItem.transform.localScale = Vector3.one;
        tempItem.Resize();

        return tempItem;
    }

    public void FillList(List<ItemDataHolder> dataList)
    {
        if (!_wasCreated)
        {
            CreateList(dataList);
            _wasCreated = true;
        }
        else
        {
            RewriteList(dataList);
        }
    }

    private void CreateList(List<ItemDataHolder> dataList)
    {
        ItemsList = new List<ItemUI>();
        for (int i = 0; i < dataList.Count; i++)
        {
            ItemsList.Add(CreateItem(dataList[i]));
        }
    }

    private void RewriteList(List<ItemDataHolder> dataList)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            if (i < ItemsList.Count)
            {
                ItemsList[i].SetItem(dataList[i]);
            }
            else
            {
                ItemsList.Add(CreateItem(dataList[i]));
            }
        }

        if (dataList.Count < ItemsList.Count)
        {
            for (int i = dataList.Count; i < ItemsList.Count; i++)
            {
                ItemsList[i].DestroyItem();
                
            }
            ItemsList.RemoveRange(dataList.Count, ItemsList.Count - dataList.Count);
        }
    }
    
    public void SoftResetList()
    {
        if (ItemsList != null && ItemsList.Count > 0)
        {
            foreach (var itemUi in ItemsList)
            {
                itemUi.ClearItem();
            }

            ItemsList.Clear();
        }
    }

    public void HardResetList()
    {
        if (ItemsList != null && ItemsList.Count > 0)
        {
            foreach (var itemUi in ItemsList)
            {
                itemUi.DestroyItem();
            }

            ItemsList.Clear();
        }
    }
}
