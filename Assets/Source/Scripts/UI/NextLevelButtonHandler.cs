using UnityEngine;
using UnityEngine.UI;
using YG;

public class NextLevelButtonHandler : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private Character _player;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Transform _playerRespawnPoint;
    [SerializeField] private GameObject _clickMessage;
    [SerializeField] private GameObject _shopButton;

    private Character _enemy;

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnButtonClick);
    }

    public void SetEnemy(Character enemy)
    {
        _enemy = enemy;
    }

    private void OnButtonClick()
    {
        _nextLevelButton.gameObject.SetActive(false);

        _player.transform.position = _playerRespawnPoint.position;
        _player.transform.rotation = Quaternion.identity;

        _cameraMovement.Remove(_enemy.transform);
        _cameraMovement.OnResetLevel();

        _enemy.RateShower.HideRateSum();
        _player.RateShower.HideRateSum();
        _player.RateShower.OnRestartLevel();
        _player.CharacterView.SetStartCloth();
        _player.CharacterMover.OnLevelReset();
        _clickMessage.SetActive(true);
        _shopButton.SetActive(true);
        _player.Animator.ChangeAnimation(AnimationsName.GirlIdle, transitionDuration: 0);

        Destroy(_enemy.gameObject);

        YandexGame.FullscreenShow();
    }
}
