using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShopButton : MonoBehaviour
{
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public bool Locked { get; private set; }

    [SerializeField] private Button _button;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Image _lockImage;
    [SerializeField] private TMP_Text _priceLabel;

    [field: SerializeField] public Mesh Hair { get; private set; }
    [field: SerializeField] public Material HairMaterial { get; private set; }
    [field: SerializeField] public Mesh Dress { get; private set; }
    [field: SerializeField] public Material DressMaterial { get; private set; }
    [field: SerializeField] public Mesh Skirt { get; private set; }
    [field: SerializeField] public Material SkirtMaterial { get; private set; }
    [field: SerializeField] public Mesh Shoes { get; private set; }
    [field: SerializeField] public Material ShoesMaterial { get; private set; }

    public int ClothIndex { get; private set; } = 0;


    public event Action<CharacterShopButton> Clicked;
    public event Action<CharacterShopButton> ApplyClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _applyButton.onClick.AddListener(OnApplyClick);

        if(Locked == true)
        {
            Lock();
        }
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _applyButton.onClick.RemoveListener(OnApplyClick);
    }

    public void SetIndex(int index)
    {
        ClothIndex = index;
    }

    public void Lock()
    {
        _lockImage.gameObject.SetActive(true);
        _priceLabel.text = Price.ToString();
        Locked = true;
    }

    public void Unlock()
    {
        _lockImage.gameObject.SetActive(false);
        Locked = false;
    }

    public void ShowApply()
    {
        _applyButton.gameObject.SetActive(true);
    }

    public void HideApply()
    {
        _applyButton.gameObject.SetActive(false);
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke(this);
    }
    
    private void OnApplyClick()
    {
        ApplyClicked?.Invoke(this);
    }
}