using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfind2D {
	public class PathGrid : MonoBehaviour {
		public bool debugGizmos = true;
		public int MaxSize => _gridSize.x * _gridSize.y;

		[SerializeField] LayerMask _unwalkableMask = 0;
		[SerializeField] Vector2 _gridWorldSize = Vector2.zero;
		[SerializeField] float _nodeRadius = 1;

		Node[,] _grid;
		float _nodeDiameter;
		Vector2Int _gridSize;

		void Awake() {
			_nodeDiameter = _nodeRadius * 2;
			_gridSize.x = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
			_gridSize.y = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
			CreateGrid();
		}

		void CreateGrid() {
			_grid = new Node[_gridSize.x, _gridSize.y];
			Vector3 __worldBottonLeft = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.up * _gridWorldSize.y / 2;

			for (int x = 0; x < _gridSize.x; x++) {
				for (int y = 0; y < _gridSize.y; y++) {
					Vector3 __worldPoint = __worldBottonLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.up * (y * _nodeDiameter + _nodeRadius);
					bool __walkable = !(Physics2D.OverlapCircle(__worldPoint, _nodeRadius, _unwalkableMask));
					_grid[x, y] = new Node(__walkable, __worldPoint, x, y);
				}
			}
		}

		public List<Node> GetNodeNeighbours(Node p_node) {
			List<Node> __neighbours = new List<Node>();

			for (int x = -1; x <= 1; x++) {
				for (int y = -1; y <= 1; y++) {
					if (x == 0 && y == 0) {
						continue;
					}

					int __checkX = p_node.gridPos.x + x;
					int __checkY = p_node.gridPos.y + y;

					if (__checkX >= 0 && __checkX < _gridSize.x && __checkY >= 0 && __checkY < _gridSize.y) {
						__neighbours.Add(_grid[__checkX, __checkY]);
					}
				}
			}

			return __neighbours;
		}

		public Node NodeInWorldPosition(Vector3 p_worldPosition) {
			float __percentX = (p_worldPosition.x + _gridWorldSize.x / 2) / _gridWorldSize.x;
			float __percentY = (p_worldPosition.y + _gridWorldSize.y / 2) / _gridWorldSize.y;
			__percentX = Mathf.Clamp01(__percentX);
			__percentY = Mathf.Clamp01(__percentY);

			int __x = Mathf.RoundToInt((_gridSize.x - 1) * __percentX);
			int __y = Mathf.RoundToInt((_gridSize.y - 1) * __percentY);
			return _grid[__x, __y];
		}

		void OnDrawGizmos() {
			if (debugGizmos) {
				Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, _gridWorldSize.y, 1));

				if (_grid != null) {
					foreach (var __node in _grid) {
						Gizmos.color = __node.walkable ? Color.white : Color.red;
						Gizmos.DrawWireCube(__node.worldPosition, Vector3.one * (_nodeDiameter - 0.1f));
					}
				}
			}
		}
	}
}
