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
        ShowFirstPlayerClothRate();
        ShowEnemyClothRate();
        _camera.GoNext();

        await Task.Delay(500);

        ShowActionButtons();
    }

    private async void ShowFirstPlayerClothRate()
    {
        _camera.GoNext();
        Dictionary<int, int> rates = _player.CharacterView.Rate;
        Debug.Log("Player");
        foreach (KeyValuePair<int, int> item in rates)
        {
            Debug.Log(item.Value);
        }
    }

    private async void ShowEnemyClothRate()
    {
        _camera.GoNext();
        Dictionary<int, int> rates = _enemy.CharacterView.Rate;
        Debug.Log("Enemy");
        foreach (KeyValuePair<int, int> item in rates)
        {
            Debug.Log(item.Value);
        }
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
