using UnityEngine;
using UnityEngine.UI;

public class ClothChoosePanel : MonoBehaviour
{
    [field: SerializeField] public Transform ButtonsParent { get; private set; }
    [SerializeField] private Image _timerBar;
    [SerializeField] private GameObject _timerParent;

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
        _timerParent.SetActive(true);
        //_timerBar.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        _timerParent.SetActive(false);
        //_timerBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
