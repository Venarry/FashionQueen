using UnityEngine;
using UnityEngine.UI;

public class ClothChoosePanel : MonoBehaviour
{
    [field: SerializeField] public Transform ButtonsParent { get; private set; }
    [SerializeField] private Image _timerBar;

    private void Awake()
    {
        HideAll();
    }

    public void FillBar(float value)
    {
        _timerBar.fillAmount = value;
    }

    public void ShowAll()
    {
        ShowPanel();
        ShowTimer();
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void ShowTimer()
    {
        _timerBar.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        _timerBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
