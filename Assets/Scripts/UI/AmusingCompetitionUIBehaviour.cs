using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmusingCompetitionUIBehaviour : MonoBehaviour
{
    public AmusingCompetitionBehaviour controller;
    public TapPanel tapPanel;
    public EndScreenPanel endScreenPanel;
    public AdPanel adPanel;

    private void Awake()
    {
        tapPanel.Show();
        endScreenPanel.Hide();
        adPanel.Hide();
        controller.OnShowEndScreen.AddListener(OnShowEndScreen);
        controller.OnGameStarted.AddListener(OnGameStarted);
    }

    public void StartLevel()
    {
        controller.StartLevel();
    }

    public void OnGameStarted()
    {
        tapPanel.Hide();
    }

    public void ShowAd()
    {
        endScreenPanel.Hide();
        adPanel.Show();
        StartCoroutine(CRHideAdPanel());
    }

    protected IEnumerator CRHideAdPanel()
    {
        yield return new WaitForSeconds(2.0f);
        adPanel.Hide();
        StartLevel();
    }

    protected void OnShowEndScreen(GameScore score)
    {
        endScreenPanel.Show(score);
    }
    
}

[System.Serializable]
public class TapPanel
{
    public GameObject panel;

    public void Show()
    {
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}

[System.Serializable]
public class EndScreenPanel
{
    public GameObject panel;
    public Text scoreText;

    public void Show(GameScore score)
    {
        panel.SetActive(true);
        scoreText.text = score.points.ToString();
    }

    protected void OnPlayAgainButton()
    {
        Hide();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}

[System.Serializable]
public class AdPanel
{
    public GameObject panel;
    public void Show()
    {
        //Retrieve ad somehow
        //...
        //
        panel.SetActive(true);
    }

    public void Hide()
    {
        //cleanup
        panel.SetActive(false);
    }
}