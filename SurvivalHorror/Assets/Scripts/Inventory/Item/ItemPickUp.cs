using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : InteractableObject {
	[SerializeField] ItemType _item = null;
	[SerializeField] [Min(1)] int _quantity = 1;

	public override void Interact() {
		_quantity = InventorySystem.instance.AddItem(_item, _quantity);
		if (_quantity <= 0) {
			Destroy(gameObject);
		}
	}
}
