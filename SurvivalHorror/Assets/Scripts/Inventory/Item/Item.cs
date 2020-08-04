using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	ItemType _itemType;
	int _quantity;

	public ItemType ItemType => _itemType;
	public int Quantity => _quantity;

	public Item(ItemType item, int quantity) {
		_itemType = item;
		if (quantity > _itemType.MaxQuantity) {
			_quantity = _itemType.MaxQuantity;
		}
		else {
			_quantity = quantity > 0 ? quantity : 1;
		}
	}

	public void Use() {
		if (_itemType.CanUse) {
			bool used = _itemType.Use();
			if (used && (_itemType.Type == Enums.Items.CONSUMABLE)) {
				Consume(1);
			}
		}
	}

	public int Add(int amount) {
		if (amount < 0) {
			return amount;
		}

		_quantity += amount;
		if (_quantity <= _itemType.MaxQuantity) {
			return 0;
		}
		else {
			int rest = _quantity - _itemType.MaxQuantity;
			_quantity = _itemType.MaxQuantity;
			return rest;
		}
	}

	public int Consume(int amount) {
		if (amount < 0) {
			return amount;
		}

		_quantity -= amount;
		if (_quantity > 0) {
			return 0;
		}
		else {
			int rest = -_quantity;
			_quantity = 0;
			InventorySystem.instance.DiscardItem(this);
			return rest;
		}
	}
}
