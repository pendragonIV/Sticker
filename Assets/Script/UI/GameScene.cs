using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Image stickerImg;
    [SerializeField]
    private TMP_Text achiveText;
    [SerializeField]
    private Transform winPanel;
    [SerializeField]
    private Transform[] flashs;

    private void Start()
    {
        stickerImg.GetComponent<RectTransform>().DOShakeRotation(2f, new Vector3(0, 0f, 15f), 0, 90f, false).SetLoops(-1, LoopType.Yoyo);
        foreach (Transform flash in flashs)
        {
            StartCoroutine(FlashEffect(flash));
        }
    }

    private IEnumerator FlashEffect(Transform flash)
    {
        Vector3 flashPos = flash.GetComponent<RectTransform>().anchoredPosition;
        while (true)
        {
            flash.GetComponent<RectTransform>().DOAnchorPos(new Vector3(flashPos.x + 160, flashPos.y, flashPos.z), 1f);
            yield return new WaitForSeconds(3f);
            flash.GetComponent<RectTransform>().anchoredPosition = flashPos;
        }
    }

    public void SetStickerImage(Sprite sprite)
    {
        stickerImg.sprite = sprite;
    }

    public void SetAchiveText(string text)
    {
        achiveText.text = text;
    }

    public void ShowWinPanel()
    {
        winPanel.gameObject.SetActive(true);
        
    }
}
