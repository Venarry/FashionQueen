using System.Collections.Generic;
using UnityEngine;

public class ClothPanelHandler : MonoBehaviour
{
    private const int ClothIndexHair = 0;
    private const int ClothIndexDress = 1;
    private const int ClothIndexSkirt = 2;
    private const int ClothIndexShoes = 3;

    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private ClothChooseButton _buttonPrefab;
    [SerializeField] private Character _characterMover;

    private readonly List<ClothChooseButton> _spawnedButton = new();
    private ClothData[][] _clothes;
    private ClothData[] _currentStageCloth;
    private int _stageCounter = 0;

    public void Enable()
    {
        _characterMover.CharacterMover.Reached += OnPointReach;
    }

    public void Disable()
    {
        _characterMover.CharacterMover.Reached -= OnPointReach;
    }

    public void SetData(ClothData[][] clothDatas)
    {
        _clothes = clothDatas;
        _stageCounter = 0;
    }

    private void ShowNextStageCloth()
    {
        RemoveButtons();
        ShowClothButtons();

        _stageCounter++;
    }

    private void ShowClothButtons()
    {
        _currentStageCloth = _clothes[_stageCounter];

        foreach (ClothData cloth in _currentStageCloth)
        {
            ClothChooseButton button = Instantiate(_buttonPrefab, _buttonsParent);
            button.Init(cloth.Material, cloth.Mesh, cloth.Rate, _stageCounter, cloth.Icon);
            button.Clicked += OnClothButtonClick;

            _spawnedButton.Add(button);
        }
    }

    private void RemoveButtons()
    {
        if(_spawnedButton.Count == 0)
            return;

        foreach (ClothChooseButton button in _spawnedButton)
        {
            button.Clicked -= OnClothButtonClick;
            Destroy(button.gameObject);
        }

        _spawnedButton.Clear();
    }

    private void OnPointReach()
    {
        ShowNextStageCloth();
    }

    private void OnClothButtonClick(ClothChooseButton button)
    {
        switch (button.ClothIndex)
        {
            case ClothIndexHair:
                _characterMover.CharacterView.SetHair(button.Material, button.Mesh);
                break;

            case ClothIndexDress:
                _characterMover.CharacterView.SetDress(button.Material, button.Mesh);
                break;

            case ClothIndexSkirt:
                _characterMover.CharacterView.SetSkirt(button.Material, button.Mesh);
                break;

            case ClothIndexShoes:
                _characterMover.CharacterView.SetShoes(button.Material, button.Mesh);
                break;
        }
    }
}
