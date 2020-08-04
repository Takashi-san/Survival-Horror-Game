using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryQuickCell : MonoBehaviour {
	[SerializeField] Image _itemIcon = null;

	public void SetIcon(Sprite itemIcon) {
		_itemIcon.sprite = itemIcon;
		if (itemIcon == null) {
			_itemIcon.color = Color.clear;
		}
		else {
			_itemIcon.color = Color.white;
		}
	}
}
