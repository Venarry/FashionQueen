using TMPro;
using UnityEngine;

public class HandRate : MonoBehaviour
{
    [SerializeField] private TMP_Text _rateText;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Set(string rate)
    {
        _rateText.text = rate;
    }
}
