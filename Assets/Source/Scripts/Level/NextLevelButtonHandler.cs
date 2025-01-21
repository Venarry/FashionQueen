using UnityEngine;
using UnityEngine.UI;

public class NextLevelButtonHandler : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private Character _player;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Transform _playerRespawnPoint;

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
        _levelSpawner.SpawnNext();

        _player.transform.position = _playerRespawnPoint.position;
        _player.CharacterMover.GoToStartPoint();

        _cameraMovement.Remove(_enemy.transform);
        _cameraMovement.OnResetLevel();

        _player.RateShower.OnRestartLevel();
        Destroy(_enemy.gameObject);

        _nextLevelButton.gameObject.SetActive(false);
    }
}
