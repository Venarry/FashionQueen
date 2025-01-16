using System.Threading.Tasks;
using UnityEngine;

public class EndLevelHandler : MonoBehaviour
{
    [SerializeField] private CharacterMover _playerMover;
    [SerializeField] private CameraMovement _camera;

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
    }
}
