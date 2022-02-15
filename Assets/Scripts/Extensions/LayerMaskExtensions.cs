using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskExtensions
{
	public static bool Contains(this LayerMask layerMask, int layer) => ((1 << layer) & layerMask) != 0;
	public static bool Contains(this LayerMask layerMask, GameObject gameObject) => layerMask.Contains(layer: gameObject.layer);
	public static bool Contains(this LayerMask layerMask, Collider collider) => layerMask.Contains(layer: collider.gameObject.layer);
}