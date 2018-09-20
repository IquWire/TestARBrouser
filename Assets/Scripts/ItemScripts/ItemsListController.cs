using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemsListController : MonoBehaviour
{
    [Inject]
    private DataLoader DataLoader;

    [Inject]
    private UiItemsManager UiItemsManager;

    public Text FeedText;

    public float TimeToUpdate;
    private DateTimeOffset _timeOffset;

    public void Awake()
    {
        //CreateElements();

        this.UpdateAsObservable().Timestamp()
            .Where(x => x.Timestamp > _timeOffset.AddSeconds(TimeToUpdate))
            .Subscribe(x =>
            {
                RefreshElements();
                _timeOffset = x.Timestamp;
            });
    }

    private void FillFeedText()
    {
        FeedText.text = DataLoader.FeedTitle;
    }

    public void CreateElements()
    {
        StartCoroutine(DataLoader.GetListCoroutine(CreateItemList));
    }

    public void CreateItemList()
    {
        UiItemsManager.FillList(DataLoader.GetList());
        FillFeedText();
    }

    public void RefreshElements()
    {
        Debug.Log("Refresh list");
        DataLoader.ClearList();
        
        CreateElements();
    }
}
