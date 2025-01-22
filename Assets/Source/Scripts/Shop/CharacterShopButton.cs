using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShopButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    [field: SerializeField] public Mesh Hair { get; private set; }
    [field: SerializeField] public Material HairMaterial { get; private set; }
    [field: SerializeField] public Mesh Dress { get; private set; }
    [field: SerializeField] public Material DressMaterial { get; private set; }
    [field: SerializeField] public Mesh Skirt { get; private set; }
    [field: SerializeField] public Material SkirtMaterial { get; private set; }
    [field: SerializeField] public Mesh Shoes { get; private set; }
    [field: SerializeField] public Material ShoesMaterial { get; private set; }

    public event Action<CharacterShopButton> Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke(this);
    }
}