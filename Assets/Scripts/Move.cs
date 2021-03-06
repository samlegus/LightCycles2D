﻿using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
	#region Inspector

	public GameObject wallPrefab;
	public float speed = 16f;
	public float maxSpeed = 50f;
	public bool debugMessages = true;

	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;

	#endregion

	#region Private Variables

	private Rigidbody2D _myRigidbody;
	private Collider2D _wallCollider;
	private Vector2 _lastWallEnd;
	private bool _alive = true;
	
	#endregion
		
	#region Events
	
	void Start () 
	{
		_myRigidbody = GetComponent<Rigidbody2D>();
		_myRigidbody.velocity = Vector2.zero;
	}

	void Update () 
	{
		if(_alive)
		{
			if(Input.GetKeyDown (upKey))
			{
				_myRigidbody.velocity = Vector2.up * speed;
				SpawnWall();
			}
			if(Input.GetKeyDown (downKey))
			{
				_myRigidbody.velocity = -Vector2.up * speed;
				SpawnWall();
			}
			if(Input.GetKeyDown (leftKey))
			{
				_myRigidbody.velocity = -Vector2.right * speed;
				SpawnWall();
			}
			if(Input.GetKeyDown (rightKey))
			{
				_myRigidbody.velocity = Vector2.right * speed;
				SpawnWall();
			}
			
			if(_wallCollider != null)
			{
				FitColliderBetweenPoints(_wallCollider, _lastWallEnd, transform.position);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(debugMessages)
		{
			Debug.Log ( gameObject.name + "'s trigger has collided with " + other.gameObject.name);
		}
		
		if(other != _wallCollider)
		{
			_alive = false;
			
			//Let's not just destroy the object.
			gameObject.GetComponent<Renderer>().enabled = false;
			_myRigidbody.velocity = Vector2.zero;
		}
	}	
	
	void OnGUI()
	{
		if(_alive == false)
		{
			GUI.Box ( new Rect(Screen.width / 2 - 75,
		                       Screen.height / 2 - 12 ,
			                   150, 
			                   25), 
		         			   gameObject.name + "has lost!");
		}
	}

	#endregion

	#region Methods

	void SpawnWall()
	{
		_lastWallEnd = transform.position;
		
		// Spawn a new Lightwall
		GameObject wall = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
		_wallCollider = wall.GetComponent<Collider2D>();
	}

	void FitColliderBetweenPoints(Collider2D co, Vector2 a, Vector2 b)
	{
		// Calculate the Center Position
		co.transform.position = a + (b - a) * 0.5f;
		
		// Scale it (horizontally or vertically)    
		float dist = Vector2.Distance(a, b);
		if (a.x != b.x)
			co.transform.localScale = new Vector2(dist + 1, 1);
		else
			co.transform.localScale = new Vector2(1, dist + 1);
	}
	
	//For new GUI slider testing, this is called by OnValueChanged?
	public void SetSpeedViaNormal(float normalizedSpeed)
	{
		speed = maxSpeed * normalizedSpeed;
	}
	
	#endregion
}
