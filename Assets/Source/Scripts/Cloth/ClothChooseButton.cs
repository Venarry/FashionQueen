using System;
using UnityEngine;
using UnityEngine.UI;

public class ClothChooseButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private Button _applyButton;

    public Material Material { get; private set; }
    public Mesh Mesh { get; private set; }
    public int Rate { get; private set; }
    public int ClothIndex { get; private set; } = -1;

    public event Action<ClothChooseButton> Clicked;
    public event Action<ClothChooseButton> ApplyClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _applyButton.onClick.AddListener(OnApplyButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _applyButton.onClick.RemoveListener(OnApplyButtonClick);
    }

    public void Init(Material material, Mesh mesh, int rate, int clothIndex, Sprite sprite)
    {
        Material = material;
        Mesh = mesh;
        Rate = rate;
        ClothIndex = clothIndex;
        _image.sprite = sprite;
    }

    public void ShowApplyButton()
    {
        _applyButton.gameObject.SetActive(true);
    }

    public void HideApplyButton()
    {
        _applyButton.gameObject.SetActive(false);
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke(this);
    }

    private void OnApplyButtonClick()
    {
        ApplyClicked?.Invoke(this);
    }
}