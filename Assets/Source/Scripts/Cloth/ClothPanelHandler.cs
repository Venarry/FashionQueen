using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothPanelHandler : MonoBehaviour
{
    private const int ClothIndexHair = 0;
    private const int ClothIndexDress = 1;
    private const int ClothIndexSkirt = 2;
    private const int ClothIndexShoes = 3;

    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private ClothChooseButton _buttonPrefab;
    [SerializeField] private Character _player;
    [SerializeField] private Image _timerImage;

    private readonly List<ClothChooseButton> _spawnedButtons = new();
    private Character _enemy;
    private ClothData[][] _clothes;
    private ClothData[] _currentStageCloth;
    private int _stageCounter = 0;
    private readonly float _showTime = 5;
    private readonly float _timeScale = 0.2f;
    private Coroutine _activeShowing;

    private void Awake()
    {
        Hide();
    }

    public void Enable()
    {
        _player.CharacterMover.ReachedNewStage += OnPointReach;
    }

    public void Disable()
    {
        _player.CharacterMover.ReachedNewStage -= OnPointReach;
    }

    public void SetData(ClothData[][] clothData)
    {
        _clothes = clothData;
        _stageCounter = 0;
    }

    public void SetEnemy(Character character)
    {
        _enemy = character;
    }

    private void ShowNextStageCloth()
    {
        if(_stageCounter >= _clothes.Length)
        {
            return;
        }

        StopShowing();
        RemoveButtons();
        ShowClothButtons();

        _stageCounter++;

        _activeShowing = StartCoroutine(Showing());
    }

    private void StopShowing()
    {
        if (_activeShowing == null)
            return;

        StopCoroutine(_activeShowing);
        _activeShowing = null;
    }

    private IEnumerator Showing()
    {
        GameTimeScaler.Add(nameof(ClothPanelHandler), _timeScale);

        for (float i = 0; i < _showTime; i += Time.unscaledDeltaTime)
        {
            _timerImage.fillAmount = i / _showTime;

            yield return null;
        }

        _activeShowing = null;
        Hide();
        GameTimeScaler.Remove(nameof(ClothPanelHandler));
    }

    private void Hide()
    {
        StopShowing();
        RemoveButtons();
        _timerImage.gameObject.SetActive(false);
        _buttonsParent.gameObject.SetActive(false);

        GameTimeScaler.Remove(nameof(ClothPanelHandler));
    }

    private void ShowClothButtons()
    {
        _currentStageCloth = _clothes[_stageCounter];
        _buttonsParent.gameObject.SetActive(true);
        _timerImage.gameObject.SetActive(true);

        foreach (ClothData cloth in _currentStageCloth)
        {
            ClothChooseButton button = Instantiate(_buttonPrefab, _buttonsParent);
            button.Init(cloth.Material, cloth.Mesh, cloth.Rate, _stageCounter, cloth.Icon);
            button.Clicked += OnClothButtonClick;

            _spawnedButtons.Add(button);
        }
    }

    private void RemoveButtons()
    {
        if(_spawnedButtons.Count == 0)
            return;

        foreach (ClothChooseButton button in _spawnedButtons)
        {
            button.Clicked -= OnClothButtonClick;
            Destroy(button.gameObject);
        }

        _spawnedButtons.Clear();
    }

    private void OnPointReach()
    {
        ShowNextStageCloth();
    }

    private void OnClothButtonClick(ClothChooseButton button)
    {
        _player.CharacterView.Set(button.ClothIndex, button.Material, button.Mesh, button.Rate);

        ClothChooseButton enemyButton = _spawnedButtons[Random.Range(0, _spawnedButtons.Count)];
        _enemy.CharacterView.Set(enemyButton.ClothIndex, enemyButton.Material, enemyButton.Mesh, enemyButton.Rate);

        Hide();
    }
}