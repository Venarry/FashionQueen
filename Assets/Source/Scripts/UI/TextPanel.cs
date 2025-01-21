using TMPro;
using UnityEngine;

public class TextPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Set(string text)
    {
        _label.text = text;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
