using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIInventoryItemCell : MonoBehaviour {
	public Action<int, UIInventoryItemCell> selected;

	[Header("References")]
	[SerializeField] Image _container = null;
	[SerializeField] Image _itemIcon = null;
	[SerializeField] TextMeshProUGUI _itemCount = null;

	[Header("Content")]
	[SerializeField] Sprite _containerNeutral = null;
	[SerializeField] Sprite _containerSelected = null;

	int _id;

	public void SetID(int p_id) {
		_id = p_id;
	}

	public void SetContent(ItemType p_item, int p_quantity) {
		if (p_item.Type == Enums.Items.WEAPON) {
			_itemCount.gameObject.SetActive(true);
			_itemCount.text = ((ItemWeapon)p_item).weapon.Magazine.ToString() + "/" + ((ItemWeapon)p_item).weapon.MagazineSize.ToString();
		}
		else if (p_item.MaxQuantity == 1) {
			_itemCount.gameObject.SetActive(false);
		}
		else {
			_itemCount.gameObject.SetActive(true);
			_itemCount.text = p_quantity.ToString();
		}
		_itemIcon.gameObject.SetActive(true);
		_itemIcon.sprite = p_item.SpriteIcon;
	}

	public void SetContent() {
		_itemCount.gameObject.SetActive(false);
		_itemIcon.gameObject.SetActive(false);
	}

	public void SetSelected(bool p_isSelected) {
		if (p_isSelected) {
			_container.sprite = _containerSelected;
		}
		else {
			_container.sprite = _containerNeutral;
		}
	}

	public void Selected() {
		selected?.Invoke(_id, this);
	}
}
