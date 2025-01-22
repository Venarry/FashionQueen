using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueText;

    private WalletModel _walletModel;

    public void Init(WalletModel walletModel)
    {
        _walletModel = walletModel;
    }

    public void Enable()
    {
        _walletModel.Added += OnValueChange;
        _walletModel.Removed += OnValueChange;

        OnValueChange();
    }

    public void Disable()
    {
        _walletModel.Added -= OnValueChange;
        _walletModel.Removed -= OnValueChange;
    }

    private void OnValueChange()
    {
        _valueText.text = _walletModel.Value.ToString();
    }
}
