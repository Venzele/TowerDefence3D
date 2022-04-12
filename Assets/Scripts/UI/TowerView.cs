using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;

    private ItemData _itemData;
    private Color _canBuyColor, _cannotBuyColor;

    public event UnityAction<ItemData> ItemSelected;
    public event UnityAction<TowerView> ItemDisabled;

    private void OnEnable()
    {
        _canBuyColor = _image.color;
        _cannotBuyColor = new Color(0, 0, 0, 0.3f);
    }

    private void OnDisable()
    {
        ItemDisabled?.Invoke(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ItemSelected?.Invoke(_itemData);
    }

    public void Initialize(ItemData itemData)
    {
        _itemData = itemData;
        _icon.sprite = itemData.Preview;
        _price.text = itemData.Price.ToString();
    }

    public void ChangeOnCanBuyColor()
    {
        _image.color = _canBuyColor;
    }

    public void ChangeOnCannotBuyColor()
    {
        _image.color = _cannotBuyColor;
    }
}
