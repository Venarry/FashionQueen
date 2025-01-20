using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EndLevelHandler : MonoBehaviour
{
    [SerializeField] private Character _player;
    [SerializeField] private CameraMovement _camera;
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private EndLevelActionButton _buttonPrefab;
    [SerializeField] private List<EndLevelActionSO> _actions;

    private readonly List<EndLevelActionButton> _spawnedButtons = new();
    private Character _enemy;

    public void SetEnemy(Character character)
    {
        _enemy = character;
    }

    public void Enable()
    {
        _player.CharacterMover.ReachedFinish += OnFinishReach;
    }

    public void Disable()
    {
        _player.CharacterMover.ReachedFinish -= OnFinishReach;
    }

    private async void OnFinishReach()
    {
        await ShowFirstPlayerClothRate();
        await ShowEnemyClothRate();

        _camera.GoNext();

        await Task.Delay(500);

        ShowActionButtons();
    }

    private async Task ShowFirstPlayerClothRate()
    {
        _camera.GoNext();
        await Task.Delay(500);

        Dictionary<int, int> rates = _player.CharacterView.Rate;

        for (int i = 0; i < _player.RateShower.RateCount; i++)
        {
            _player.RateShower.ShowNext();
            await Task.Delay(1000);
        }

        await Task.Delay(2000);
    }

    private async Task ShowEnemyClothRate()
    {
        _camera.GoNext();
        await Task.Delay(500);

        Dictionary<int, int> rates = _enemy.CharacterView.Rate;

        for (int i = 0; i < _enemy.RateShower.RateCount; i++)
        {
            _enemy.RateShower.ShowNext();
            await Task.Delay(1000);
        }

        await Task.Delay(2000);
    }

    private void ShowActionButtons()
    {
        _buttonsParent.gameObject.SetActive(true);

        foreach (EndLevelActionSO action in _actions)
        {
            EndLevelActionButton button = Instantiate(_buttonPrefab, _buttonsParent);
            button.Init(action.AnimationName, action.Icon);

            _spawnedButtons.Add(button);
        }
    }
}
