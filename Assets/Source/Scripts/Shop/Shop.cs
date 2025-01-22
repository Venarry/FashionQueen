using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private CharacterShopButton[] _characterButtons;
    [SerializeField] private Character _character;

    private WalletModel _walletModel;
    private SaveHandler _saveHandler;

    public int ActiveCloth { get; private set; } = 0;

    public void Init(WalletModel walletModel, SaveHandler saveHandler)
    {
        _walletModel = walletModel;
        _saveHandler = saveHandler;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _characterButtons.Length; i++)
        {
            CharacterShopButton button = _characterButtons[i];

            button.SetIndex(i);
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

    public void Load(bool[] data, int activeClothIndex)
    {
        for (int i = 0; i < _characterButtons.Length; i++)
        {
            if (data.Length <= i)
            {
                return;
            }

            if (data[i] == true)
            {
                _characterButtons[i].Unlock();
            }
        }

        SetCloth(_characterButtons[activeClothIndex]);
    }

    public void Show()
    {
        _parent.SetActive(true);
    }

    public void Hide()
    {
        _parent.SetActive(false);
    }

    public bool[] GetLockedData()
    {
        List<bool> data = new();

        foreach (CharacterShopButton button in _characterButtons)
        {
            data.Add(button.Locked == false);
        }

        return data.ToArray();
    }

    private void OnBuyClick(CharacterShopButton button)
    {
        if (button.Locked == true)
        {
            if (_walletModel.TryRemove(button.Price))
            {
                button.Unlock();
                SetCloth(button);
            }
        }
        else
        {
            SetCloth(button);
        }
    }

    private void SetCloth(CharacterShopButton button)
    {
        _character.CharacterView.Set(1, button.DressMaterial, button.Dress, 1);
        _character.CharacterView.Set(2, button.SkirtMaterial, button.Skirt, 1);
        ActiveCloth = button.ClothIndex;

        _saveHandler.Save();
    }
}
