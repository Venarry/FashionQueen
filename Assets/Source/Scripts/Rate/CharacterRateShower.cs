using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterRateShower : MonoBehaviour
{
    [SerializeField] private CharacterRateView[] _ratesView;
    [SerializeField] private TMP_Text _rateSumText;

    private CharacterView _characterView;
    private RateSmilesDataSource _rateSmilesDataSource;
    private int _showCounter;

    public int RateCount => _ratesView.Length;

    private void Awake()
    {
        _characterView = GetComponent<CharacterView>();
    }

    public void Init(RateSmilesDataSource rateSmilesDataSource)
    {
        _rateSmilesDataSource = rateSmilesDataSource;
    }

    public void OnRestartLevel()
    {
        _showCounter = 0;
    }

    public void ShowRateSum(int value)
    {
        _rateSumText.gameObject.SetActive(true);
        _rateSumText.text = value.ToString();
    }

    public void ShowNext()
    {
        Dictionary<int, int> rates = _characterView.Rate;
        Sprite[] smiles = _rateSmilesDataSource.Sprites;
        CharacterRateView currentRate = _ratesView[_showCounter];

        currentRate.Show();

        int rate = rates[_showCounter];
        currentRate.Set(smiles[rate - 1], rate);

        _showCounter++;
    }

    public void Hide()
    {
        foreach (CharacterRateView rate in _ratesView)
        {
            rate.Hide();
        }

        _rateSumText.gameObject.SetActive(false);
    }
}
