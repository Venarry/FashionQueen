using UnityEngine;

public class StartLevelHandler : MonoBehaviour
{
    [SerializeField] private ClickPanel _clickPanel;
    [SerializeField] private Character _player;
    [SerializeField] private GameObject _clickMessage;
    [SerializeField] private LevelSpawner _levelSpawner;

    private void OnEnable()
    {
        _clickPanel.Clicked += OnScreenClick;
    }

    private void OnDisable()
    {
        _clickPanel.Clicked -= OnScreenClick;
    }

    private void OnScreenClick()
    {
        if (_player.CharacterMover.IsStarted == true)
            return;

        _player.CharacterMover.GoToStartPoint();
        _levelSpawner.SpawnNext();
        _clickMessage.gameObject.SetActive(false);
    }
}
