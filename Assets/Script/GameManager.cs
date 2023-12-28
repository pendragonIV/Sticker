using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public GameObject magnifyingGlass;

    #region Game status
    public SceneChanger sceneChanger;
    public GameScene gameScene;

    private Level currentLevelData;
 
    private bool isGameWin = false;
    [SerializeField]
    private bool isLose = false;
    #endregion

    #region Sticker

    private GameObject randomSticker;
    private int choosenStickerID = -1;

    #endregion

    private void Start()
    {
        magnifyingGlass.SetActive(false);

        currentLevelData = LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex);
        GameObject map = Instantiate(currentLevelData.map);
        StickerManager.instance.SetContainer(map.transform.GetChild(0));
        SetStickerToFind();
        SetAchiveUI();

    }

    private void Update()
    {
        if (randomSticker && choosenStickerID != -1)
        {
            if (choosenStickerID == randomSticker.GetComponent<Sticker>().GetID())
            {
                StickerManager.instance.RemoveSticker(randomSticker);
                Destroy(randomSticker);

                if (StickerManager.instance.GetNumberOfStickers() <= 0)
                {
                    Win();
                }

                SetStickerToFind();
                SetAchiveUI();
            }
            else
            {
                Debug.Log("Wrong");
            }
            choosenStickerID = -1;
        }
    }

    public void MagnifyingGlassBtn()
    {
        bool isActive = magnifyingGlass.activeSelf;
        magnifyingGlass.SetActive(!isActive);
    }

    private void SetAchiveUI()
    {
        int maxSticker = StickerManager.instance.MaxNumberOfStickers();
        int currentSticker = StickerManager.instance.GetNumberOfStickers();
        gameScene.SetAchiveText(maxSticker - currentSticker + "/" + maxSticker);
    }

    private void SetStickerToFind()
    {
        randomSticker = StickerManager.instance.GetRandomSticker();
        if (randomSticker != null)
        {
            int stickerID = randomSticker.GetComponent<Sticker>().GetID();
            gameScene.SetStickerImage(StickerManager.instance.GetStickerSprite(stickerID));
        }
    }

    private void Win()
    {
        isGameWin = true;
        gameScene.ShowWinPanel();
    }

    public void SetChoosenStickerID(int id)
    {
        choosenStickerID = id;
    }   

    public bool IsGameWin()
    {
        return isGameWin;
    }

    public bool isGameLose()
    {
        return isLose;
    }

}
