using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StickerManager : MonoBehaviour
{
    public static StickerManager instance;

    #region Sticker props
    private int numberOfStickers;
    [SerializeField]
    private List<GameObject> stickers;
    [SerializeField]
    private GameObject stickerPrefab;
    [SerializeField]
    private Transform stickerContainer;
    private Ray ray;
    [SerializeField]
    private Sprite[] stickerImage;
    [SerializeField]
    private Material[] stickerMaterial;
    [SerializeField]
    private Material rootMaterial;
    private List<Vector3> placedPosition;

    #endregion

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

        numberOfStickers = stickerImage.Length;
        placedPosition = new List<Vector3>();
        stickerMaterial = new Material[numberOfStickers];
    }

    public void SetContainer(Transform container)
    {
        stickerContainer = container;
        StickerInitalizer();
    }
    #region Sticker Management

    private void StickerInitalizer()
    {
        for (int i = 0; i < numberOfStickers; i++)
        {
            GameObject sticker = Instantiate(stickerPrefab, stickerContainer);

            sticker.transform.position = CastRandomPosition();
            stickerMaterial[i] = MaterialCreator(rootMaterial, stickerImage[i]);
            sticker.GetComponent<DecalProjector>().material = stickerMaterial[i];
            sticker.transform.rotation = StickerRotating(sticker.transform.position);
            sticker.GetComponent<Sticker>().SetID(i);
            stickers.Add(sticker);
        }
    }

    private Material MaterialCreator(Material root, Sprite stickerImg)
    {
        Material material = new Material(root);
        material.name = stickerImg.name;
        material.SetTexture("Base_Map", stickerImg.texture);
        return material;
    }

    private Quaternion StickerRotating(Vector3 stickerPos)
    {
        Vector3 direction = stickerContainer.position - stickerPos;
        Quaternion lookAt = Quaternion.LookRotation(direction);
        return lookAt;
    }

    #endregion

    #region Random

    private Vector3 CastRandomPosition()
    {
        Vector3 outPos = Vector3.zero;
        do
        {
            Vector3 randomDirection = GetRandomDirection();
            float longestAxis = GetLongestAxis();

            Vector3 containerPos = stickerContainer.position;
            if (LevelManager.instance.currentLevelIndex == 0)
            {
                containerPos = new Vector3(stickerContainer.position.x, stickerContainer.position.y + longestAxis, stickerContainer.position.z);
            }

            Vector3 startPosition = containerPos + randomDirection * longestAxis;
            Vector3 castDirection = containerPos - startPosition;
            ray = new Ray(startPosition, castDirection);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Surface")))
            {
                Vector3 outDir = startPosition - hit.point;
                Debug.DrawRay(hit.point, outDir, Color.red, Mathf.Infinity);
                outPos = hit.point + outDir.normalized * .0001f;
            }
        } while (placedPosition.Contains(outPos));

        placedPosition.Add(outPos);

        return outPos;
    }

    private float GetLongestAxis()
    {
        Vector3 extents = stickerContainer.GetComponent<Collider>().bounds.extents;

        float longestAxis = extents.x;
        if (extents.y > longestAxis)
        {
            longestAxis = extents.y;
        }
        if (extents.z > longestAxis)
        {
            longestAxis = extents.z;
        }

        return longestAxis;
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 extents = stickerContainer.GetComponent<Collider>().bounds.extents;

        float randomX = Random.Range(-extents.x, extents.x );
        float randomY = Random.Range(-extents.y, extents.y);
        float randomZ = Random.Range(-extents.z, extents.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    #endregion

    #region Getters and Setters

    public void SetNumberOfStickers(int number)
    {
        numberOfStickers = number;
    }

    public GameObject GetRandomSticker()
    {
        if(stickers.Count == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, stickers.Count);
        return stickers[randomIndex];
    }

    public void RemoveSticker(GameObject sticker)
    {
        stickers.Remove(sticker);
    }

    public Sprite GetStickerSprite(int ID)
    {
        return stickerImage[ID];
    }

    public int GetNumberOfStickers()
    {
        return stickers.Count;
    }

    public int MaxNumberOfStickers()
    {
        return numberOfStickers;
    }

    #endregion
}

