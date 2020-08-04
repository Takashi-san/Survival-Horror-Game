using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Puzzle", menuName = "ScriptableObjects/Item/Puzzle")]
public class ItemPuzzle : ItemType {
	[Header("Item puzzle info")]
	[SerializeField] string _puzzle = null;
	public string Puzzle => _puzzle;

	public override bool Use() { return true; }
}
