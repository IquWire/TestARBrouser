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

    [SerializeField] private RefreshImageController _refreshImage;

    public Text FeedText;

    public float TimeToUpdate;
    private DateTimeOffset _timeOffset;

    public void Awake()
    {
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

    private void CreateItemList()
    {
        UiItemsManager.FillList(DataLoader.GetList());
        FillFeedText();
    }

    private void RefreshElements()
    {
        RefreshFeedback();
        DataLoader.ClearList();
        
        CreateElements();
    }

    private void RefreshFeedback()
    {
        _refreshImage.PlayRefreshAnim();
    }
}
