using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelHandler : MonoBehaviour
{
    [SerializeField] private Character _player;
    [SerializeField] private CameraMovement _camera;
    [SerializeField] private ClothChoosePanel _choosePanel;
    [SerializeField] private EndLevelActionButton _buttonPrefab;
    [SerializeField] private List<EndLevelActionSO> _actions;
    [SerializeField] private Button _nextLevelButton;

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
        int playerRate = _player.CharacterView.Rate.Values.Sum();
        int enemyRate = _enemy.CharacterView.Rate.Values.Sum();

        await ShowCharacterClothRate(_player, playerRate);
        await ShowCharacterClothRate(_enemy, enemyRate);

        _camera.GoNext();

        await Task.Delay(500);

        _player.RateShower.HideRate();
        _enemy.RateShower.HideRate();

        if (TryWinPlayer(playerRate, enemyRate) == true)
        {
            ShowActionButtons();
        }
        else
        {
            _enemy.Animator.ChangeAnimation(AnimationsName.GirlWalk);
            await _enemy.CharacterMover.GoToAttackPoint();

            EndLevelActionSO randomAction = _actions[Random.Range(0, _actions.Count)];
            _enemy.Animator.ChangeAnimation(randomAction.AnimationName);
            _player.Animator.ChangeAnimation(AnimationsName.GirlDieB);

            EndLevel();
        }
    }

    private bool TryWinPlayer(int playerRate, int enemyRate)
    {
        if(playerRate >= enemyRate)
        {
            return true;
        }
        else if(playerRate < enemyRate)
        {
            return false;
        }

        return false;
    }

    private async Task ShowCharacterClothRate(Character character, int rate)
    {
        _camera.GoNext();
        await Task.Delay(500);

        for (int i = 0; i < character.RateShower.RateCount; i++)
        {
            character.RateShower.ShowNext();
            await Task.Delay(1000);
        }

        character.RateShower.ShowRateSum(rate);

        await Task.Delay(2000);
    }

    private void EndLevel()
    {
        _player.RateShower.HideRate();
        _player.RateShower.HideRateSum();

        _enemy.RateShower.HideRate();
        _enemy.RateShower.HideRateSum();

        _nextLevelButton.gameObject.SetActive(true);
    }

    private void ShowActionButtons()
    {
        _choosePanel.ShowPanel();

        foreach (EndLevelActionSO action in _actions)
        {
            EndLevelActionButton button = Instantiate(_buttonPrefab, _choosePanel.ButtonsParent);
            button.Init(action.AnimationName, action.Icon);
            button.Clicked += OnClick;

            _spawnedButtons.Add(button);
        }
    }

    private void HideButtons()
    {
        _choosePanel.HideAll();

        if (_spawnedButtons.Count == 0)
            return;

        foreach (EndLevelActionButton button in _spawnedButtons)
        {
            Destroy(button.gameObject);
        }

        _spawnedButtons.Clear();
    }

    private async void OnClick(EndLevelActionButton button)
    {
        _player.Animator.ChangeAnimation(AnimationsName.GirlWalk);
        await _player.CharacterMover.GoToAttackPoint();
        _player.Animator.ChangeAnimation(button.ActionAnimationName);
        _enemy.Animator.ChangeAnimation(AnimationsName.GirlDieB);

        HideButtons();
        EndLevel();
    }
}
