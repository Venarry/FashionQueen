using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteHandler : MonoBehaviour
{
    private const float _baseMultiplier = 1.0f;
    private const float _firstMultiplier = 1.5f;
    private const float _secondMultiplier = 2f;

    [SerializeField] private GameObject _parent;
    [SerializeField] private RectTransform _slider;
    [SerializeField] private Transform _rouletteColorsParent;
    [SerializeField] private RouletteColor _colorPrefab;
    [SerializeField] private Button _getButton;
    [SerializeField] private TMP_Text _potentialPrise;
    [SerializeField] private float _sliderWidth;

    private readonly Dictionary<float, float> _moneyMultiplier = new()
    {
        [0.25f] = _baseMultiplier,
        [0.45f] = _firstMultiplier,
        [0.55f] = _secondMultiplier,
        [0.75f] = _firstMultiplier,
        [1f] = _baseMultiplier,
    };

    private readonly Dictionary<float, Color> _multiplierColors = new()
    {
        [_baseMultiplier] = Color.red,
        [_firstMultiplier] = Color.yellow,
        [_secondMultiplier] = Color.green,
    };

    private readonly float _minValue = 0f;
    private readonly float _maxValue = 1f;
    private float _currentSlideValue = 0;
    private int _currentWalletValue = 0;
    private WalletModel _walletModel;
    private Coroutine _activeSlide;
    private SaveHandler _saveHandler;

    private int Prise => Mathf.FloorToInt(_currentWalletValue * GetMoneyMultiplier());

    public event Action GetButtonClicked;

    private void Awake()
    {
        SpawnImages();
    }

    public void Init(WalletModel walletModel, SaveHandler saveHandler)
    {
        _walletModel = walletModel;
        _saveHandler = saveHandler;
    }

    private void SpawnImages()
    {
        float[] sizes = _moneyMultiplier.Keys.ToArray();
        float[] multipliers = _moneyMultiplier.Values.ToArray();

        SpawnColor(sizes[0], multipliers[0]);

        for (int i = 1; i < sizes.Length; i++)
        {
            SpawnColor(sizes[i] - sizes[i - 1], multipliers[i]);
        }
    }

    private void SpawnColor(float targetSize, float multiplier)
    {
        RouletteColor color = Instantiate(_colorPrefab, _rouletteColorsParent);
        color.Init(_multiplierColors[multiplier], multiplier.ToString(), targetSize * _sliderWidth);
        //color.rectTransform.sizeDelta = new Vector2(targetSize * _sliderWidth, color.rectTransform.sizeDelta.y);
        //color.color = _multiplierColors[multiplier];
    }

    private void OnEnable()
    {
        _getButton.onClick.AddListener(OnGetButtonClick);
    }

    private void OnDisable()
    {
        _getButton.onClick.RemoveListener(OnGetButtonClick);
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
        float multiplier = GetMoneyMultiplier();
        _walletModel.Add(Prise);

        Debug.Log($"base {_currentWalletValue}; mult {multiplier}; itog {Prise}");
        _saveHandler.Save();

        GetButtonClicked?.Invoke();
        Hide();
    }

    private float GetMoneyMultiplier()
    {
        float lastMultiplier = 1f;

        foreach (KeyValuePair<float, float> multiplier in _moneyMultiplier)
        {
            if(_currentSlideValue > multiplier.Key)
            {
                continue;
            }
            else
            {
                lastMultiplier = multiplier.Value;
                break;
            }
        }

        return lastMultiplier;
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

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            _currentSlideValue = Mathf.Lerp(startValue, targetValue, i / duration);
            _slider.anchoredPosition = new Vector3(_currentSlideValue * _sliderWidth, 0, 0);
            _potentialPrise.text = Prise.ToString();

            yield return null;
        }
    }
}
