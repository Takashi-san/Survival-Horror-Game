using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemType : ScriptableObject {
	[Header("Item base info")]
	[SerializeField] protected Enums.Items _type = Enums.Items.NONE;
	[SerializeField] protected bool _canUse = false;
	[SerializeField] protected bool _canDiscard = false;
	[SerializeField] [Min(1)] protected int _maxQuantity = 1;
	[SerializeField] protected Sprite _spriteIcon = null;
	[SerializeField] protected Sprite _spriteGame = null;
	[SerializeField] string _name = null;
	[SerializeField] string _description = null;

	public Enums.Items Type => _type;
	public bool CanUse => _canUse;
	public bool CanDiscard => _canDiscard;
	public int MaxQuantity => _maxQuantity;
	public Sprite SpriteIcon => _spriteIcon;
	public Sprite SpriteGame => _spriteGame;
	public string Name => _name;
	public string Description => _description;

	public abstract bool Use();
}
