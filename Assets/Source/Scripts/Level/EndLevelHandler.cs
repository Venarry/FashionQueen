using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EndLevelHandler : MonoBehaviour
{
    [SerializeField] private CharacterMover _playerMover;
    [SerializeField] private CameraMovement _camera;
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private EndLevelActionButton _buttonPrefab;
    [SerializeField] private List<EndLevelActionSO> _actions;

    private readonly List<EndLevelActionButton> _spawnedButtons = new();

    public void Enable()
    {
        _playerMover.ReachedFinish += OnFinishReach;
    }

    public void Disable()
    {
        _playerMover.ReachedFinish -= OnFinishReach;
    }

    private async void OnFinishReach()
    {
        _camera.GoToEndPoint();

        await Task.Delay(500);

        ShowActionButtons();
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
