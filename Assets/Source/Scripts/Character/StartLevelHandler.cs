using UnityEngine;

public class StartLevelHandler : MonoBehaviour
{
    [SerializeField] private ClickPanel _clickPanel;
    [SerializeField] private Character _player;
    [SerializeField] private GameObject _clickMessage;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private GameObject _shopButton;

    private bool _enabled = true;

    private void OnEnable()
    {
        _clickPanel.Clicked += OnScreenClick;
    }

    private void OnDisable()
    {
        _clickPanel.Clicked -= OnScreenClick;
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
    }

    private void OnScreenClick()
    {
        if (_enabled == false)
            return;

        if (_player.CharacterMover.IsStarted == true)
            return;

        _player.CharacterMover.GoToStartPoint();
        _levelSpawner.SpawnNext();
        _cameraMovement.StartMovement();
        _clickMessage.SetActive(false);
        _shopButton.SetActive(false);
    }
}
