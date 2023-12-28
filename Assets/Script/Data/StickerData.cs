using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StickerData", menuName = "ScriptableObjects/StickerData", order = 1)]
public class StickerData : ScriptableObject
{
    [SerializeField] private List<Sticker> stickers;
}
