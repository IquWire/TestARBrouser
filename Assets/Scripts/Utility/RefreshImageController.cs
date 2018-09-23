using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshImageController : MonoBehaviour
{
    [SerializeField]
    [Range(5f,20f)]
    private float _speed;

    [SerializeField]
    private int _roundCount;

    private const float _speedMultiplier = 10000f;
    
    void Awake ()
    {        
        gameObject.SetActive(false);
        ResetRotation();
    }

    public void PlayRefreshAnim()
    {
        ResetRotation();
        gameObject.SetActive(true);
        
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        float endRotation = 360 * _roundCount; 
        float currentRotation = 0;

        float step = (_speed * _speedMultiplier/ 360) * Time.deltaTime;

        while (currentRotation < endRotation)
        {
            currentRotation += step;
            if (currentRotation >= endRotation)
            {
                currentRotation = endRotation;
            }
            Vector3 currRotation = new Vector3(0,0, -currentRotation);
            transform.localEulerAngles = currRotation;
            yield return null;
            
        }
        OnEndRotation();
    }

    private void OnEndRotation()
    {
        gameObject.SetActive(false);
        ResetRotation();
    }

    private void ResetRotation()
    {
        transform.localEulerAngles = Vector3.zero;
    }
}
