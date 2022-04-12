using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ItemData", order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite _preview;
    [SerializeField] private int _price;
    [SerializeField] private Tower _prefab;

    public Sprite Preview => _preview;
    public int Price => _price;
    public Tower Prefab => _prefab;
}
