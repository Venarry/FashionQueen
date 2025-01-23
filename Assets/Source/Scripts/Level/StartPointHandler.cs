using System.Collections;
using UnityEngine;

public class StartPointHandler : MonoBehaviour
{
    [SerializeField] private CharacterMover _playerMover;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private CameraMovement _targetFollower;
    [SerializeField] private EndLevelHandler _endLevelHandler;
    [SerializeField] private NextLevelButtonHandler _nextLevelButtonHandler;

    private readonly float _actionsDelay = 0.5f;
    private Character _enemy;

    public void Enable()
    {
        _playerMover.ReachedStartPoint += OnStartPointReach;
    }

    public void Disable()
    {
        _playerMover.ReachedStartPoint -= OnStartPointReach;
    }

    private void OnStartPointReach()
    {
        StartCoroutine(StartLevel());
    }

    private IEnumerator StartLevel()
    {
        yield return new WaitForSecondsRealtime(_actionsDelay);

        Vector3 enemySpawnPosition = new(_playerMover.Position.x * -1, _playerMover.Position.y, _playerMover.Position.z);
        GameObject cameraFollowGameobject = new("EnemyPosition");
        cameraFollowGameobject.transform.position = enemySpawnPosition;
        _targetFollower.Add(cameraFollowGameobject.transform);

        yield return StartCoroutine(_enemySpawner.SpawnWithProjection(enemySpawnPosition, Quaternion.identity, _playerMover.MoveSpeed, SetEnemy));

        _targetFollower.Remove(cameraFollowGameobject.transform);
        Destroy(cameraFollowGameobject);
        _clothPanelHandler.SetEnemy(_enemy);
        _endLevelHandler.SetEnemy(_enemy);
        _nextLevelButtonHandler.SetEnemy(_enemy);
        _targetFollower.Add(_enemy.transform);

        yield return new WaitForSecondsRealtime(_actionsDelay);

        StartCoroutine(_playerMover.Go());
        StartCoroutine(_enemy.CharacterMover.Go());
    }

    private void SetEnemy(Character character)
    {
        _enemy = character;
    }
}
