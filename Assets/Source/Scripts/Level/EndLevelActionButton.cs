using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EndLevelActionButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;

    public string ActionAnimationName { get; private set; }

    public event Action<EndLevelActionButton> Clicked;

    public void Init(string animationName, Sprite icon)
    {
        ActionAnimationName = animationName;
        _icon.sprite = icon;
    }

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
