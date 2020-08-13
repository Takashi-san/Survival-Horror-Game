using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryMenu : MonoBehaviour {
	[SerializeField] GameObject _itemGrid = null;
	[SerializeField] GameObject _itemCellPrefab = null;
	[SerializeField] TextMeshProUGUI _itemNameField = null;
	[SerializeField] TextMeshProUGUI _itemDescriptionField = null;
	[SerializeField] GameObject _actionsMenu = null;
	[SerializeField] GameObject _useButton = null;
	[SerializeField] GameObject _shortcutButton = null;
	[SerializeField] GameObject _discardButton = null;

	List<InventoryItemCell> _cells = new List<InventoryItemCell>();
	InventoryItemCell _selectedCell;
	InventoryQuickAccess _quickAccess;
	bool _isMoving = false;

	void OnEnable() {
		_quickAccess = FindObjectOfType<InventoryQuickAccess>();
		List<Item> inventory = InventorySystem.instance.Inventory;
		int size = InventorySystem.instance.Size;

		if (size != _cells.Count) {
			if (size > _cells.Count) {
				int diff = size - _cells.Count;
				for (int i = 0; i < diff; i++) {
					InventoryItemCell cell = Instantiate(_itemCellPrefab).GetComponent<InventoryItemCell>();
					cell.transform.SetParent(_itemGrid.transform, false);
					cell.setText += SetText;
					cell.selected += ItemSelected;
					_cells.Add(cell);
				}
			}
			else {
				int diff = _cells.Count - size;
				for (int i = 0; i < diff; i++) {
					_cells.RemoveAt(0);
				}
			}
		}

		for (int i = 0; i < size; i++) {
			if (inventory[i] == null) {
				_cells[i].SetIcon(null);
			}
			else {
				_cells[i].SetIcon(inventory[i].ItemType.SpriteIcon);
			}
		}

		_itemNameField.text = null;
		_itemDescriptionField.text = null;
	}

	void Update() {
		if (Input.GetMouseButtonDown(1)) {
			DeactivateActions();
		}
	}

	void SetText(InventoryItemCell cell) {
		List<Item> inventory = InventorySystem.instance.Inventory;
		int index = _cells.IndexOf(cell);

		if (inventory[index] == null) {
			_itemNameField.text = null;
			_itemDescriptionField.text = null;
		}
		else {
			_itemNameField.text = inventory[index].ItemType.Name;
			_itemDescriptionField.text = inventory[index].ItemType.Description;
		}
	}

	void ItemSelected(InventoryItemCell cell) {
		if (_isMoving) {
			InventorySystem.instance.SwapItem(_cells.IndexOf(_selectedCell), _cells.IndexOf(cell));
			_isMoving = false;
			OnEnable();
		}
		else {
			if (InventorySystem.instance.Inventory[_cells.IndexOf(cell)] != null) {
				ActivateActions(cell);
			}
		}
	}

	void OnDisable() {
		DeactivateActions();
	}

	void ActivateActions(InventoryItemCell cell) {
		ItemType item = InventorySystem.instance.Inventory[_cells.IndexOf(cell)].ItemType;
		_actionsMenu.GetComponent<RectTransform>().position = (Vector2)Input.mousePosition;
		_selectedCell = cell;
		_useButton.SetActive(item.CanUse);
		_shortcutButton.SetActive(item.CanUse);
		_discardButton.SetActive(item.CanDiscard);
		_actionsMenu.SetActive(true);
	}

	void DeactivateActions() {
		_selectedCell = null;
		_isMoving = false;
		_actionsMenu.SetActive(false);
	}

	#region Item Actions

	public void UseAction() {
		InventorySystem.instance.Use(_cells.IndexOf(_selectedCell));
		DeactivateActions();
		OnEnable();
	}

	public void ShortcutAction(int position) {
		_quickAccess.SetShortcut(position, _cells.IndexOf(_selectedCell));
		DeactivateActions();
	}

	public void MoveAction() {
		_isMoving = true;
		_actionsMenu.SetActive(false);
	}

	public void DiscardAction() {
		InventorySystem.instance.DiscardItem(_cells.IndexOf(_selectedCell));
		DeactivateActions();
		OnEnable();
	}

	#endregion
}
