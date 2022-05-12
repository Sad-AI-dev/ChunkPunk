using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    public static SettingController instance;

    public List<ISetting> settings = new();

    private void Start()
    {
        if (PlayerPrefs.GetInt("HasSaved") > 0) {
            StartCoroutine(LoadCo());
        }
    }

    IEnumerator LoadCo()
    {
        yield return null;
        foreach (ISetting setting in settings) {
            setting.Load();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("HasSaved", 1);
    }
}
