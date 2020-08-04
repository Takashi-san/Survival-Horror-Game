using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryItemCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[SerializeField] Image _itemIcon = null;
	bool _isPointerOnTop = false;

	public Action<InventoryItemCell> setText;
	public Action<InventoryItemCell> selected;

	public void SetIcon(Sprite itemIcon) {
		_itemIcon.sprite = itemIcon;
		if (itemIcon == null) {
			_itemIcon.color = Color.clear;
		}
		else {
			_itemIcon.color = Color.white;
		}
	}

	void Update() {
		if (_isPointerOnTop) {
			if (Input.GetMouseButtonDown(0)) {
				if (selected != null)
					selected(this);
			}
		}
	}

	public void OnPointerEnter(PointerEventData pointerData) {
		_isPointerOnTop = true;
		if (setText != null)
			setText(this);
	}

	public void OnPointerExit(PointerEventData pointerData) {
		_isPointerOnTop = false;
	}
}
