using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EndLevelActionButton : MonoBehaviour
{
    private Button _button;
    private int _actionIndex;

    public event Action<EndLevelActionButton> Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Init(int index)
    {
        _actionIndex = index;
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
