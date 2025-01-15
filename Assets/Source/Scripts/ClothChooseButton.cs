using System;
using UnityEngine;
using UnityEngine.UI;

public class ClothChooseButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public Material Material { get; private set; }
    public Mesh Mesh { get; private set; }
    public int Rate { get; private set; }
    public int ClothIndex { get; private set; } = -1;

    public event Action<ClothChooseButton> Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Init(Material material, Mesh mesh, int rate, int clothIndex, Sprite sprite)
    {
        Material = material;
        Mesh = mesh;
        Rate = rate;
        ClothIndex = clothIndex;
        //_image.sprite = sprite;
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke(this);
    }
}