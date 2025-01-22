using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RouletteHandler : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Button _getButton;

    private readonly float _minValue = 0f;
    private readonly float _maxValue = 1f;
    [SerializeField] private float _currentSlideValue = 0;
    private int _currentWalletValue = 0;
    private WalletModel _walletModel;
    private Coroutine _activeSlide;

    public event Action GetButtonClicked;

    private void OnEnable()
    {
        _getButton.onClick.AddListener(OnGetButtonClick);
    }

    private void OnDisable()
    {
        _getButton.onClick.RemoveListener(OnGetButtonClick);
    }

    public void Init(WalletModel walletModel)
    {
        _walletModel = walletModel;
    }

    public void Show(int baseValue)
    {
        DisableSlide();
        _currentWalletValue = baseValue;
        _parent.SetActive(true);

        _activeSlide = StartCoroutine(Slide());
    }

    public void Hide()
    {
        DisableSlide();
        _parent.SetActive(false);
    }

    private void OnGetButtonClick()
    {
        _walletModel.Add(_currentWalletValue);

        GetButtonClicked?.Invoke();
        Hide();
    }

    private void DisableSlide()
    {
        if(_activeSlide != null)
        {
            StopCoroutine(_activeSlide);
            _activeSlide = null;
        }
    }

    private IEnumerator Slide()
    {
        _currentSlideValue = 0;

        while (true)
        {
            yield return StartCoroutine(SlideTo(1, 1));
            yield return StartCoroutine(SlideTo(0, 1));
        }
    }

    private IEnumerator SlideTo(float targetValue, float duration)
    {
        float startValue = _currentSlideValue;

        for (float i = 0; i < duration; i+=Time.deltaTime)
        {
            _currentSlideValue = Mathf.Lerp(startValue, targetValue, i / duration);

            yield return null;
        }
    }
}
