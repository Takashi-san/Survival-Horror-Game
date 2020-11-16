using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInventoryMenu : MonoBehaviour {
	public bool IsActive => _isActive;

	[SerializeField] GameObject _itemCellPrefab = null;

	[Header("References")]
	[SerializeField] GameObject _inventoryMenu = null;
	[SerializeField] RectTransform _itemGrid = null;
	[SerializeField] TextMeshProUGUI _itemName = null;
	[SerializeField] TextMeshProUGUI _itemDescription = null;
	[SerializeField] GameObject _buttonUse = null;
	[SerializeField] GameObject _buttonMove = null;
	[SerializeField] GameObject _buttonDiscard = null;

	int _selectedItem = -1;
	List<UIInventoryItemCell> _cellList;
	bool _isMoving = false;
	bool _isActive = false;

	public void Activate() {
		_inventoryMenu.SetActive(true);

		foreach (Transform __cell in _itemGrid) {
			Destroy(__cell.gameObject);
		}

		_cellList = new List<UIInventoryItemCell>();
		for (int i = 0; i < InventorySystem.instance.Inventory.Count; i++) {
			UIInventoryItemCell __cell = Instantiate(_itemCellPrefab, _itemGrid).GetComponent<UIInventoryItemCell>();
			if (InventorySystem.instance.Inventory[i] == null) {
				__cell.SetContent();
			}
			else {
				__cell.SetContent(InventorySystem.instance.Inventory[i].ItemType, InventorySystem.instance.Inventory[i].Quantity);
			}
			__cell.SetID(i);
			__cell.selected += ItemSelected;
			_cellList.Add(__cell);
		}

		_buttonUse.SetActive(false);
		_buttonMove.SetActive(false);
		_buttonDiscard.SetActive(false);
		_itemName.text = "";
		_itemDescription.text = "";
		_isMoving = false;
		_isActive = true;
	}

	public void Deactivate() {
		_inventoryMenu.SetActive(false);
		_isActive = false;
	}

	public void UseButton() {
		InventorySystem.instance.Use(_selectedItem);
		_selectedItem = -1;

		_isMoving = false;
		foreach (var __cell in _cellList) {
			__cell.SetSelected(false);
		}
		_buttonUse.SetActive(false);
		_buttonMove.SetActive(false);
		_buttonDiscard.SetActive(false);
		_itemName.text = "";
		_itemDescription.text = "";
		UpdateGrid();
	}

	public void MoveButton() {
		_isMoving = true;
	}

	public void DiscardButton() {
		InventorySystem.instance.DiscardItem(_selectedItem);
		_selectedItem = -1;

		_isMoving = false;
		foreach (var __cell in _cellList) {
			__cell.SetSelected(false);
		}
		_buttonUse.SetActive(false);
		_buttonMove.SetActive(false);
		_buttonDiscard.SetActive(false);
		_itemName.text = "";
		_itemDescription.text = "";
		UpdateGrid();
	}

	void ItemSelected(int p_id, UIInventoryItemCell p_cell) {
		if (_isMoving) {
			InventorySystem.instance.SwapItem(_selectedItem, p_id);
			_selectedItem = -1;
			_isMoving = false;
			UpdateGrid();
		}
		else {
			if (InventorySystem.instance.Inventory[p_id] != null) {
				_selectedItem = p_id;

				ItemType __item = InventorySystem.instance.Inventory[p_id].ItemType;
				_buttonUse.SetActive(__item.CanUse);
				_buttonMove.SetActive(true);
				_buttonDiscard.SetActive(__item.CanDiscard);
				_itemName.text = __item.Name;
				_itemDescription.text = __item.Description;

				foreach (var __cell in _cellList) {
					__cell.SetSelected(false);
				}
				p_cell.SetSelected(true);
				return;
			}
		}

		foreach (var __cell in _cellList) {
			__cell.SetSelected(false);
		}
		_buttonUse.SetActive(false);
		_buttonMove.SetActive(false);
		_buttonDiscard.SetActive(false);
		_itemName.text = "";
		_itemDescription.text = "";
	}

	void UpdateGrid() {
		for (int i = 0; i < _cellList.Count; i++) {
			if (InventorySystem.instance.Inventory[i] == null) {
				_cellList[i].SetContent();
			}
			else {
				_cellList[i].SetContent(InventorySystem.instance.Inventory[i].ItemType, InventorySystem.instance.Inventory[i].Quantity);
			}
		}
	}
}
