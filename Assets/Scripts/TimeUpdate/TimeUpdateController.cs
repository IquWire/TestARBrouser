using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpdateController : MonoBehaviour
{
    public Text TimeText;

    public delegate void OnTimeUpdateDelegate();
    public event OnTimeUpdateDelegate OnTimeUpdateEvent;

    public float TimeToUpdate;

    private float _currentTime;

    void Awake ()
    {
        StartCoroutine(SendEventCoroutine());
    }

    private IEnumerator SendEventCoroutine()
    {
        while (true)
        {
            if (_currentTime < TimeToUpdate)
            {
                _currentTime += Time.deltaTime;
                yield return null;
            }
            else
            {
                OnTimeUpdateEventHandler();
                ResetTime();
            }

            TimeText.text = ((int) (_currentTime)).ToString();
        }
    }

    public void ResetTime()
    {
        _currentTime = 0;
    }

    public void OnTimeUpdateEventHandler()
    {
        if (OnTimeUpdateEvent != null)
        {
            OnTimeUpdateEvent();
        }
    }
}
