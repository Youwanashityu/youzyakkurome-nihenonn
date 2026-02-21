
using UnityEngine;

public interface ICharacterData
{
    public float[] LoveLvPoints { get; }
    public Sprite DefaultCharaImage { get; }
    public Sprite DefaultMiniImage { get; }
    public ItemType[] EventItemList { get; }
}