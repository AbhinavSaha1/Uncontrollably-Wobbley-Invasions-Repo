using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
	public Animator animator;
	private Item _grabbedItem;
	[SerializeField] private Rigidbody _rigidbody;
	public int isLeftorRight;

	private bool _canGrab;

	private void Start()
	{
		this._rigidbody = this.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(this.isLeftorRight))
		{
			if (this.isLeftorRight == 0)
			{
				this.animator.SetBool("Left hand  grab", true);
			}
			else if (this.isLeftorRight == 1)
			{
				this.animator.SetBool("Right hand grab", true);
			}

			this._canGrab = true;
		}
		else if (Input.GetMouseButtonUp(this.isLeftorRight))
		{
			if (this.isLeftorRight == 0)
			{
				this.animator.SetBool("Left hand  grab", false);
			}
			else if (this.isLeftorRight == 1)
			{
				this.animator.SetBool("Right hand grab", false);
			}

			if (this._grabbedItem != null)
			{
				this._grabbedItem.Release();

				this._grabbedItem = null;
			}

			this._canGrab = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (this._canGrab && this._grabbedItem == null && other.TryGetComponent<Item>(out this._grabbedItem))
		{
			this._grabbedItem.PickUp(this._rigidbody);
		}
	}
}
