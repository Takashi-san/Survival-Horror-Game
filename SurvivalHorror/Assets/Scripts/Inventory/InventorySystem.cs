using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class InventorySystem : MonoBehaviour {
	[SerializeField] [Min(0)] int _size = 0;
	List<Item> _inventory = new List<Item>();

	public int Size => _size;
	public List<Item> Inventory => _inventory;
	public Action addedItem;
	public Action<int, int> itemSwaped;
	public Action<int> itemDiscarded;
	public static InventorySystem instance;

	void Awake() {
		if (instance == null) {
			instance = this;
		}
		else {
			Debug.LogWarning("multiple inventory instances!");
			Destroy(this);
		}

		for (int i = 0; i < _size; i++) {
			_inventory.Add(null);
		}
	}

	public void Use(int index) {
		if ((index >= 0) && (index < _inventory.Count)) {
			if (_inventory[index] != null) {
				_inventory[index].Use();
			}
		}
	}

	public void SwapItem(int indexA, int indexB) {
		Item tmp = _inventory[indexA];
		_inventory[indexA] = _inventory[indexB];
		_inventory[indexB] = tmp;
		if (itemSwaped != null)
			itemSwaped(indexA, indexB);
	}

	public int AddItem(ItemType item, int quantity) {
		List<int> emptyIndex = new List<int>();
		int rest = quantity;

		// Check for existing item stack.
		for (int i = 0; i < _inventory.Count; i++) {
			if (_inventory[i] != null) {
				if (_inventory[i].ItemType == item) {
					rest = _inventory[i].Add(rest);
					if (rest == 0) {
						if (addedItem != null)
							addedItem();
						return 0;
					}
				}
			}
			else {
				emptyIndex.Add(i);
			}
		}

		// Try to put on empty spaces.
		if (emptyIndex.Count != 0) {
			foreach (int i in emptyIndex) {
				if (rest > item.MaxQuantity) {
					_inventory[i] = new Item(item, item.MaxQuantity);
					rest -= item.MaxQuantity;
				}
				else {
					_inventory[i] = new Item(item, rest);
					if (addedItem != null)
						addedItem();
					return 0;
				}
			}
		}

		// No more space, return the leftover that didn't fit.
		if (addedItem != null)
			addedItem();
		return rest;
	}

	public void DiscardItem(Item item) {
		int index = _inventory.IndexOf(item);
		if (index != -1) {
			_inventory[index] = null;
			if (itemDiscarded != null)
				itemDiscarded(index);
		}
		else {
			Debug.Log("Did not find item do discard!");
		}
	}

	public void DiscardItem(int index) {
		if ((index >= 0) && (index < _inventory.Count)) {
			_inventory[index] = null;
			if (itemDiscarded != null)
				itemDiscarded(index);
		}
		else {
			Debug.Log("invalid index to discard");
		}
	}

	public int GetAmmo(Enums.Ammo type, int quantity) {
		Dictionary<int, int> ammo = new Dictionary<int, int>();
		int rest = quantity;

		// Find the ammo stacks.
		for (int i = 0; i < _inventory.Count; i++) {
			if (_inventory[i] != null) {
				if (_inventory[i].ItemType.Type == Enums.Items.AMMO) {
					if (_inventory[i].ItemType is ItemAmmo ia) {
						if (ia.Ammo == type) {
							ammo.Add(i, _inventory[i].Quantity);
						}
					}
				}
			}
		}

		// Start consuming the stacks.
		foreach (KeyValuePair<int, int> kvp in ammo.OrderBy(i => i.Value)) {
			rest = _inventory[kvp.Key].Consume(rest);
			if (rest == 0) {
				return 0;
			}
		}

		// Unable to get the full quantity requested.
		return rest;
	}

	public int CheckAmmo(Enums.Ammo type) {
		int ammo = 0;

		// Find the ammo stacks.
		foreach (Item item in _inventory) {
			if (item != null) {
				if (item.ItemType.Type == Enums.Items.AMMO) {
					if (item.ItemType is ItemAmmo ia) {
						if (ia.Ammo == type) {
							ammo += item.Quantity;
						}
					}
				}
			}
		}

		return ammo;
	}
}
