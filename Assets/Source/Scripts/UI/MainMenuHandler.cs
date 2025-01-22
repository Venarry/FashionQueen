using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private StartLevelHandler _startGamePanel;
    [SerializeField] private Shop _shop;
    [SerializeField] private CameraMovement _cameraMovement;

    private void OnEnable()
    {
        _shopButton.onClick.AddListener(OnShopClicked);
        _backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnDisable()
    {
        _shopButton.onClick.RemoveListener(OnShopClicked);
        _backButton.onClick.RemoveListener(OnBackClicked);
    }

    private void OnShopClicked()
    {
        _startGamePanel.Disable();
        _shop.Show();
        _shopButton.gameObject.SetActive(false);
        _cameraMovement.GoToShop();
    }

    private void OnBackClicked()
    {
        _startGamePanel.Enable();
        _shop.Hide();
        _shopButton.gameObject.SetActive(true);
        _cameraMovement.GoToStartPosition();
    }
}