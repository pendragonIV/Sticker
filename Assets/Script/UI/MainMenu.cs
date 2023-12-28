using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Transform gameLogo;
    [SerializeField]
    private Transform tutorPanel;
    [SerializeField]
    private Transform guideLine;
    [SerializeField]
    private Button playButton;


    private void Start()
    {
        tutorPanel.gameObject.SetActive(false);
        gameLogo.GetComponent<Image>().fillAmount = 0;

        gameLogo.GetComponent<CanvasGroup>().alpha = 0f;
        gameLogo.GetComponent<CanvasGroup>().DOFade(1, 2f).SetUpdate(true);

        SetUpPlayBtn();
    }

    private void SetUpPlayBtn()
    {
        playButton.interactable = false;
        playButton.GetComponent<CanvasGroup>().alpha = 0f;
        playButton.GetComponent<CanvasGroup>().DOFade(1, 1f).SetUpdate(true);
        playButton.transform.localScale = Vector3.one * 7;
        playButton.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() =>
        {
            playButton.interactable = true;
        });
    }

    private void Update()
    {
        if(gameLogo.GetComponent<Image>().fillAmount < 1)
        {
            gameLogo.GetComponent<Image>().fillAmount += Time.deltaTime;
        }
    }

    public void ShowTutorPanel()
    {
        tutorPanel.gameObject.SetActive(true);
        guideLine.gameObject.SetActive(true);
        FadeIn(tutorPanel.GetComponent<CanvasGroup>(), guideLine.GetComponent<RectTransform>());

    }

    public void HideTutorPanel()
    {
        StartCoroutine(FadeOut(tutorPanel.GetComponent<CanvasGroup>(), guideLine.GetComponent<RectTransform>()));

    }   

    private void FadeIn(CanvasGroup canvasGroup ,RectTransform rectTransform)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, .3f).SetUpdate(true);

        rectTransform.anchoredPosition = new Vector3(0, 1700, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 200), .3f, false).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup, RectTransform rectTransform)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0, .3f).SetUpdate(true);

        rectTransform.anchoredPosition = new Vector3(0, 200, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 1700), .3f, false).SetEase(Ease.OutQuint).SetUpdate(true);

        yield return new WaitForSecondsRealtime(.3f);
        guideLine.gameObject.SetActive(true);
        tutorPanel.gameObject.SetActive(false);

    }

}
