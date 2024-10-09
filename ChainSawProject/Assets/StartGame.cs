using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
        Score.Instance.DieCount = 0;
    }

    public void StartBtn()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
