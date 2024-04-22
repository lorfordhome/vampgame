using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColliderSize : MonoBehaviour
{
	//public GameObject colliderToScale;
	private Vector3 colliderSize = new Vector2(0.5f, 1.3f);
	void Update()
	{
		if (gameObject.transform.localScale != colliderSize)
		{
			transform.localScale = colliderSize;
		}
	}
}
