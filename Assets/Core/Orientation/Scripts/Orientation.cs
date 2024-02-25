using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Orientation : MonoBehaviour
{
    private static bool _isCompleted = false;

    [SerializeField] private GameObject _hint;

    [SerializeField] private Button _fade;
    
    [SerializeField] private bool _isFadeClickable = false;
    
    [SerializeField] private bool _portrait = false;
    
    [SerializeField] private bool _landscape = true;

    [SerializeField] private bool _dontDestroyOnLoad = true;

    [SerializeField] private bool _autoHide = true;
    
    [SerializeField] private float _hideDelay = 2;
    
    private void Awake()
    {
        if(_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
        
        if(_isFadeClickable)
            _fade.onClick.AddListener(HideHint);
        
        if(!_isCompleted)
            ShowHint();
    }

    private void ShowHint()
    {
        _hint.SetActive(true);
        
        _isCompleted = true;

        SetOrientation(_portrait, _landscape);
        
        if (_autoHide)
        {
            var tween = DOVirtual.DelayedCall(_hideDelay, () =>
            {
                _hint.SetActive(false);
            }).Play();
        }
    }
    
    private void HideHint()
    {
        if(!_hint.activeSelf)
            return;

        _hint.SetActive(false);
    }
    
    private void SetOrientation(bool portrait,bool landscape)
    {
        Screen.autorotateToPortrait = portrait;
        Screen.autorotateToPortraitUpsideDown = portrait;
        Screen.autorotateToLandscapeLeft = landscape;
        Screen.autorotateToLandscapeRight = landscape;
        
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
    
    private void Update()
    {
        if (_autoHide) return;
        
        if(!_hint.activeSelf)
            return;

        if (!_landscape) return;
        
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            HideHint();
        }
    }
}
