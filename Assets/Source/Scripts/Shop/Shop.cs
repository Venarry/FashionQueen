using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private CharacterShopButton[] _characterButtons;
    [SerializeField] private Character _character;

    private WalletModel _walletModel;
    private SaveHandler _saveHandler;

    public void Init(WalletModel walletModel, SaveHandler saveHandler)
    {
        _walletModel = walletModel;
        _saveHandler = saveHandler;
    }

    private void OnEnable()
    {
        foreach (CharacterShopButton button in _characterButtons)
        {
            button.Clicked += OnBuyClick;
        }
    }

    private void OnDisable()
    {
        foreach (CharacterShopButton button in _characterButtons)
        {
            button.Clicked -= OnBuyClick;
        }
    }

    public void Show()
    {
        _parent.SetActive(true);
    }

    public void Hide()
    {
        _parent.SetActive(false);
    }

    private void OnBuyClick(CharacterShopButton button)
    {
        _character.CharacterView.Set(1, button.DressMaterial, button.Dress, 1);
        _character.CharacterView.Set(2, button.SkirtMaterial, button.Skirt, 1);
    }
}
