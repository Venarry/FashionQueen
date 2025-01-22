using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShopButton : MonoBehaviour
{
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public bool Locked { get; private set; }

    [SerializeField] private Button _button;
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

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);

        if(Locked == true)
        {
            Lock();
        }
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
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

    private void OnButtonClick()
    {
        Clicked?.Invoke(this);
    }
}