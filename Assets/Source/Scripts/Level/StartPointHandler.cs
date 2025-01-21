using System;
using System.Threading.Tasks;
using UnityEngine;

public class StartPointHandler : MonoBehaviour
{
    [SerializeField] private CharacterMover _playerMover;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private CameraMovement _targetFollower;
    [SerializeField] private EndLevelHandler _endLevelHandler;
    [SerializeField] private NextLevelButtonHandler _nextLevelButtonHandler;

    private readonly int _actionsDelay = 500;
    private Character _enemy;

    public void Enable()
    {
        _playerMover.ReachedStartPoint += OnStartPointReach;
    }

    public void Disable()
    {
        _playerMover.ReachedStartPoint -= OnStartPointReach;
    }

    private async void OnStartPointReach()
    {
        try
        {
            await Task.Delay(_actionsDelay);

            Vector3 enemySpawnPosition = new(_playerMover.Position.x * -1, _playerMover.Position.y, _playerMover.Position.z);
            GameObject cameraFollowGameobject = new("EnemyPosition");
            cameraFollowGameobject.transform.position = enemySpawnPosition;
            _targetFollower.Add(cameraFollowGameobject.transform);

            _enemy = await _enemySpawner.SpawnWithProjection(enemySpawnPosition, Quaternion.identity, _playerMover.MoveSpeed);

            _targetFollower.Remove(cameraFollowGameobject.transform);
            Destroy(cameraFollowGameobject);
            _clothPanelHandler.SetEnemy(_enemy);
            _endLevelHandler.SetEnemy(_enemy);
            _nextLevelButtonHandler.SetEnemy(_enemy);
            _targetFollower.Add(_enemy.transform);

            await Task.Delay(_actionsDelay);

            _playerMover.Go();
            _enemy.CharacterMover.Go();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
}
