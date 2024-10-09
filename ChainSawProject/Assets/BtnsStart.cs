using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnsStart : MonoBehaviour
{
    public PlayerInput pi;
    [SerializeField] private string _gameScene;
    [SerializeField] private string _tutorialScene;
    [SerializeField] private string _mainScene;

    public void Exit()
    {
        Application.Quit();
    }

    public void GameScene()
    {
        LoadingSceneManager.LoadScene(_gameScene);
    }

    public void TutorialScene()
    {
        LoadingSceneManager.LoadScene(_tutorialScene);
    }

    public void Resume()
    {
        pi.isEsc = !pi.isEsc;
        this.gameObject.SetActive(false);
    }

    public void goMain()
    {
        LoadingSceneManager.LoadScene(_mainScene);
    }
}
