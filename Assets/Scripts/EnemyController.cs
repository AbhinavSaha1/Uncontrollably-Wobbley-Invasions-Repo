using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    [Header("Variables")]
    public float speed = 2;
	public float chaseDist = 5;
	public float turnSpeed = 500f;
    public float attackDist = 5f;

    public enum State {
		Idle,
		Chase
	}

    public State state = State.Idle;

	Transform player;
	Rigidbody body;

    void Awake () {
		body = GetComponent<Rigidbody>();
		player =  GameObject.FindGameObjectWithTag("Player").transform;
	}

    void Update () {
		switch (state) {
			case State.Idle:		
				IdleUpdate();
				break;
			case State.Chase:
				ChaseUpdate();
				break;
			default:
				break;
		}
	}

    void IdleUpdate () {
		body.velocity = Vector3.zero;
		float dist = Vector3.Distance(transform.position, player.position);
		if (dist < chaseDist) {
			state = State.Chase;
		}
	}

    void ChaseUpdate () {
		Vector3 dir  = (player.position - transform.position).normalized;
		Vector3 cross = Vector3.Cross(transform.forward, dir);
		transform.Rotate(Vector3.up * cross.y * turnSpeed * Time.deltaTime);

		float dist = Vector3.Distance(player.position, transform.position);
		if (dist > chaseDist) {
			state = State.Idle;
		}
		else {
			body.velocity = dir * speed;
			//Set walking anim bool to true
		}
	}
}
