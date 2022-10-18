using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweening : MonoBehaviour
{
    public LeanTweenType inType;
    public LeanTweenType outType;
    public float duration;
    public float delay;
    private void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(4, 4, 3), duration).setDelay(delay).setEase(inType);
    }

    void Start()
    {
        
    }

    public void PopOut()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setEase(outType).setOnComplete(DisableGameObject);
    }


    public void DisableGameObject()
    {
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        
    }
    
    
}
