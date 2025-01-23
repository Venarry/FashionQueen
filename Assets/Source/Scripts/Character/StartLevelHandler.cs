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

        Show();
    }

    public void Disable()
    {
        _enabled = false;

        Hide();
    }

    private void OnScreenClick()
    {
        if (_enabled == false)
            return;

        if (_player.CharacterMover.IsStarted == true)
            return;

        StartCoroutine(_player.CharacterMover.GoToStartPoint());
        _levelSpawner.SpawnNext();
        _cameraMovement.StartMovement();
        Hide();
    }

    private void Show()
    {
        _clickMessage.SetActive(true);
        _shopButton.SetActive(true);
    }

    private void Hide()
    {
        _clickMessage.SetActive(false);
        _shopButton.SetActive(false);
    }
}
