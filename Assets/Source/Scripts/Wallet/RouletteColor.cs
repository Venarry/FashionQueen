using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteColor : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _multiplierText;

    public void Init(Color color, string multiplierText, float sizeX)
    {
        _image.color = color;
        _multiplierText.text = $"x{multiplierText}";
        _image.rectTransform.sizeDelta = new Vector2(sizeX, _image.rectTransform.sizeDelta.y);
    }
}
