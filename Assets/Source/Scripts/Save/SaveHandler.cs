using System;
using UnityEngine;

public class SaveHandler
{
    private SaveData _saveData;
    private WalletModel _walletModel;
    private LevelSpawner _levelSpawner;

    public SaveHandler(WalletModel walletModel, LevelSpawner levelSpawner)
    {
        _walletModel = walletModel;
        _levelSpawner = levelSpawner;
    }

    public void Load()
    {
        LoadSave();

        _walletModel.LoadData(_saveData.Money);
        _levelSpawner.LoadData(_saveData.LevelIndex);
    }

    public void Save()
    {
        _saveData.Money = _walletModel.Value;
        _saveData.LevelIndex = _levelSpawner.ActiveLevelIndex;

        string saveStringData = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString(GameConstants.KeySave, saveStringData);
    }

    private void LoadSave()
    {
        if (PlayerPrefs.HasKey(GameConstants.KeySave) == true)
        {
            string saveStringData = PlayerPrefs.GetString(GameConstants.KeySave);
            _saveData = JsonUtility.FromJson<SaveData>(saveStringData);
        }
        else
        {
            _saveData = new();
        }
    }
}


[Serializable]
public class SaveData
{
    public int Money = 0;
    public int LevelIndex = 0;
}
