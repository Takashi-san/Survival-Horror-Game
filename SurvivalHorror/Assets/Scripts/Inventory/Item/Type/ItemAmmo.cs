using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Ammo", menuName = "ScriptableObjects/Item/Ammo")]
public class ItemAmmo : ItemType {
	[Header("Item ammo info")]
	[SerializeField] Enums.Ammo _ammo = Enums.Ammo.NONE;
	public Enums.Ammo Ammo => _ammo;

	public override bool Use() { return true; }
}
