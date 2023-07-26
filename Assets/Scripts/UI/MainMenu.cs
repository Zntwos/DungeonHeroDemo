using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button replayBtn;

    private void Start()
    {
        replayBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void OnDestroy()
    {
        replayBtn.onClick.RemoveAllListeners();
    }
}
