using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryQuickAccess : MonoBehaviour {
	[SerializeField] [Range(1, 9)] int _size = 3;
	[SerializeField] GameObject _quickCell = null;
	List<int> _indexes = new List<int>();
	List<InventoryQuickCell> _cells = new List<InventoryQuickCell>();

	void Start() {
		for (int i = 0; i < _size; i++) {
			InventoryQuickCell cell = Instantiate(_quickCell).GetComponent<InventoryQuickCell>();
			cell.transform.SetParent(transform, false);
			_cells.Add(cell);
			_indexes.Add(-1);
		}

		// InventorySystem.instance.itemDiscarded += ItemDiscarded;
		// InventorySystem.instance.itemSwaped += ItemSwaped;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			if (_indexes[0] != -1) {
				InventorySystem.instance.Use(_indexes[0]);
			}
		}
		if (_size == 1)
			return;
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			if (_indexes[1] != -1) {
				InventorySystem.instance.Use(_indexes[1]);
			}
		}
		if (_size == 2)
			return;
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			if (_indexes[2] != -1) {
				InventorySystem.instance.Use(_indexes[2]);
			}
		}
		if (_size == 3)
			return;
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			if (_indexes[0] != -1) {
				InventorySystem.instance.Use(_indexes[3]);
			}
		}
		if (_size == 4)
			return;
		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			if (_indexes[1] != -1) {
				InventorySystem.instance.Use(_indexes[4]);
			}
		}
		if (_size == 5)
			return;
		if (Input.GetKeyDown(KeyCode.Alpha6)) {
			if (_indexes[2] != -1) {
				InventorySystem.instance.Use(_indexes[5]);
			}
		}
		if (_size == 6)
			return;
		if (Input.GetKeyDown(KeyCode.Alpha7)) {
			if (_indexes[0] != -1) {
				InventorySystem.instance.Use(_indexes[6]);
			}
		}
		if (_size == 7)
			return;
		if (Input.GetKeyDown(KeyCode.Alpha8)) {
			if (_indexes[1] != -1) {
				InventorySystem.instance.Use(_indexes[7]);
			}
		}
		if (_size == 8)
			return;
		if (Input.GetKeyDown(KeyCode.Alpha9)) {
			if (_indexes[2] != -1) {
				InventorySystem.instance.Use(_indexes[8]);
			}
		}
		if (_size == 9)
			return;
	}

	void UpdateIcon(int index) {
		if (_indexes[index] != -1) {
			_cells[index].SetIcon(InventorySystem.instance.Inventory[_indexes[index]].ItemType.SpriteIcon);
		}
		else {
			_cells[index].SetIcon(null);
		}
	}

	public void SetShortcut(int key, int index) {
		for (int i = 0; i < _indexes.Count; i++) {
			if (_indexes[i] == index) {
				_indexes[i] = -1;
				UpdateIcon(i);
				break;
			}
		}
		_indexes[key - 1] = index;
		UpdateIcon(key - 1);
	}

	void ItemDiscarded(int index) {
		for (int i = 0; i < _indexes.Count; i++) {
			if (_indexes[i] == index) {
				_indexes[i] = -1;
				UpdateIcon(i);
			}
		}
	}

	void ItemSwaped(int indexA, int indexB) {
		for (int i = 0; i < _indexes.Count; i++) {
			if (_indexes[i] == indexA) {
				_indexes[i] = indexB;
				UpdateIcon(i);
			}
			else if (_indexes[i] == indexB) {
				_indexes[i] = indexA;
				UpdateIcon(i);
			}
		}
	}
}
