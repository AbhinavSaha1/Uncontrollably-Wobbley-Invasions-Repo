using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
	public PlayerController playerController;

	private void Start()
	{
		this.playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
			this.playerController.isGrounded = true;
		}

	}
}
