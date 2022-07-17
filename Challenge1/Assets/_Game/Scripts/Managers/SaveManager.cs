using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public const string saveName = "Save1";
    public GameData _gameData;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(saveName))
        {
            _gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(saveName));
        }
        else
        {
            _gameData = new GameData();
        }
    }

    public void ResetSave()
    {
        if (PlayerPrefs.HasKey(saveName))
            PlayerPrefs.DeleteKey(saveName);
    }

    private void OnApplicationPause(bool pause)
    {
        // TODO if performance is necesary remove save on pause
        Save();
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetString("", JsonUtility.ToJson(_gameData));
    }
}
