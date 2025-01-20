using System.Collections.Generic;
using UnityEngine;

public class CharacterRateShower : MonoBehaviour
{
    [SerializeField] private CharacterRateView[] _ratesView;

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

    public void ShowNext()
    {
        Dictionary<int, int> rates = _characterView.Rate;
        Sprite[] smiles = _rateSmilesDataSource.Sprites;
        CharacterRateView currentRate = _ratesView[_showCounter];

        currentRate.Show();

        int rate = rates[_showCounter];
        Debug.Log(rate);
        currentRate.Set(smiles[rate - 1], rate);

        _showCounter++;
    }
}
