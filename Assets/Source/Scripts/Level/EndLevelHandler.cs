using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private TextPanel _levelNamePanel;

    [SerializeField] private HandRate _handPrefab;
    [SerializeField] private Transform[] _handPoints;
    [SerializeField] private RouletteHandler _rouletteHandler;

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

    private void OnFinishReach()
    {
        StartCoroutine(Finish());
    }

    private IEnumerator Finish()
    {
        int playerRate = _player.CharacterView.Rate.Values.Sum();
        int enemyRate = _enemy.CharacterView.Rate.Values.Sum();

        yield return StartCoroutine(ShowCharacterClothRate(_player, playerRate));
        yield return StartCoroutine(ShowCharacterClothRate(_enemy, enemyRate));

        _camera.GoNext();

        yield return new WaitForSecondsRealtime(0.5f);

        _player.RateShower.HideAll();
        _enemy.RateShower.HideAll();

        _levelNamePanel.Hide();

        bool playerIsWin = TryWinPlayer(playerRate, enemyRate);

        StartCoroutine(HandleAttackAction(playerIsWin));
    }

    private IEnumerator HandleAttackAction(bool playerIsWin)
    {
        if (playerIsWin == true)
        {
            ShowActionButtons();
        }
        else
        {
            _enemy.Animator.ChangeAnimation(AnimationsName.GirlWalk);
            yield return StartCoroutine(_enemy.CharacterMover.GoToAttackPoint());

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

    private IEnumerator ShowCharacterClothRate(Character character, int rate)
    {
        _camera.GoNext();
        yield return new WaitForSecondsRealtime(0.5f);

        for (int i = 0; i < character.RateShower.RateCount; i++)
        {
            character.RateShower.ShowNext();
            yield return new WaitForSecondsRealtime(1);
        }

        yield return StartCoroutine(ShowHandsWithRates(character, rate));

        yield return new WaitForSecondsRealtime(2);
    }

    private IEnumerator ShowHandsWithRates(Character character, int rate)
    {
        Dictionary<int, int> rates = character.CharacterView.Rate;
        List<HandRate> hands = new();

        for (int i = 0; i < _handPoints.Length; i++)
        {
            Transform point = _handPoints[i];
            HandRate hand = Instantiate(_handPrefab, point);
            hand.Set(rates[i].ToString());
            hand.Show();

            hands.Add(hand);
            yield return new WaitForSecondsRealtime(0.6f);
        }

        character.RateShower.ShowRateSum(rate);

        yield return new WaitForSecondsRealtime(1);

        foreach (HandRate hand in hands)
        {
            Destroy(hand.gameObject);
        }

        hands.Clear();
    }

    private void EndLevel()
    {
        _nextLevelButton.gameObject.SetActive(true);
    }

    private void ShowActionButtons()
    {
        _choosePanel.ShowPanel();

        foreach (EndLevelActionSO action in _actions)
        {
            EndLevelActionButton button = Instantiate(_buttonPrefab, _choosePanel.ButtonsParent);
            button.Init(action.AnimationName, action.Icon);
            button.Clicked += OnActionClick;

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
            button.Clicked -= OnActionClick;
            Destroy(button.gameObject);
        }

        _spawnedButtons.Clear();
    }

    private void OnActionClick(EndLevelActionButton button)
    {
        StartCoroutine(HandleActionClick(button));
    }

    private IEnumerator HandleActionClick(EndLevelActionButton button)
    {
        HideButtons();

        _player.Animator.ChangeAnimation(AnimationsName.GirlWalk);
        yield return StartCoroutine(_player.CharacterMover.GoToAttackPoint());
        _player.Animator.ChangeAnimation(button.ActionAnimationName);
        _enemy.Animator.ChangeAnimation(AnimationsName.GirlDieB);

        yield return new WaitForSecondsRealtime(0.6f);

        _player.Animator.ChangeAnimation(AnimationsName.GirlWalk);
        yield return StartCoroutine(_player.CharacterMover.GoToRoulettePoint());

        _player.Animator.ChangeAnimation(AnimationsName.GirlDance);
        _rouletteHandler.Show(_player.CharacterView.Rate.Values.Sum());
        _rouletteHandler.GetButtonClicked += OnGetButtonClick;
    }

    private void OnGetButtonClick()
    {
        _rouletteHandler.GetButtonClicked -= OnGetButtonClick;
        EndLevel();
    }
}
