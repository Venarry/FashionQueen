using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRateView : MonoBehaviour
{
    [SerializeField] private Image _smile;
    [SerializeField] private TMP_Text _rateText;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Set(Sprite sprite, int rate)
    {
        _smile.sprite = sprite;
        _rateText.text = rate.ToString();
    }
}
