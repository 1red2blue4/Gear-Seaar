using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

public class Sender : MonoBehaviour {
	//Position of wireless sender
	private IVector3 _position;
	public IVector3 Position
	{
		set { _position = value; }
		get { return _position; }
	}
	private int _speed;
	public SpeedHandler _sHandler;
	private LineRenderer _line;

	private GridHandler.Gear _type;
	public GridHandler.Gear Type
	{
		set { _type = value; }
		get { return _type; }
	}
	private GridHandler _grid;

	private bool _startDragging;

	void Start()
	{
		_grid = GameObject.Find ("GameHandler").GetComponent<GridHandler> ();

		//Spin
		if (_speed > 0) {
			//GetComponent<Animator> ().enabled = true;
		}
		//Dont spin
		else {
			//GetComponent<Animator> ().enabled = false;
		}
	}

	void Update()
	{
	}

	//Disappear on dragging and changing pos
	void OnMouseDown()
	{
		gameObject.GetComponent<Renderer> ().enabled = false;
		gameObject.GetComponent<LineRenderer> ().enabled = false;
	}

	void OnMouseDrag()
	{
		/*gameObject.GetComponent<Renderer> ().enabled = false;
		gameObject.GetComponent<LineRenderer> ().enabled = false;*/
		ConstantHandler.Instance.ComponentDragged = true;

	}

	//Change position of sender
	void OnMouseUp()
	{
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit;

		if( Physics.Raycast( ray, out hit) )
		{
			if (hit.transform.gameObject.tag == "Grid") {
				IVector3 posNew = new IVector3 ((int)(hit.transform.position.x * 2), (int)(hit.transform.position.y * 2), 0);
				if (_grid.GetTileType (posNew.x, posNew.y) != GridHandler.Gear.ORANGE_CHECK &&
				    _grid.GetTileType (posNew.x, posNew.y) != GridHandler.Gear.GREEN_CHECK &&
				    _grid.GetTileType (posNew.x, posNew.y) != GridHandler.Gear.BLUE_CHECK) {
					_grid.SetTileToType (posNew.x, posNew.y, _type);
					_sHandler.SetGearTeethValue (posNew.x, posNew.y, _type);
					_grid.AttachItem (posNew.x, posNew.y);

					_grid.SetTileToType (_position.x, _position.y, GridHandler.Gear.EMPTY);
					_sHandler.SetGearTeethValue (_position.x, _position.y, GridHandler.Gear.EMPTY);
					_sHandler.CalculateSpeed ();
					//_sHandler.CalculateSenderSpeeds ();
					_grid.AttachItem (_position.x, _position.y);
				} else {
					gameObject.GetComponent<Renderer> ().enabled = true;
					gameObject.GetComponent<LineRenderer> ().enabled = true;
				}
			} else {
				gameObject.GetComponent<Renderer> ().enabled = true;
				gameObject.GetComponent<LineRenderer> ().enabled = true;
			}
		}
		gameObject.GetComponent<Renderer> ().enabled = true;
		gameObject.GetComponent<LineRenderer> ().enabled = true;

	}

	//Calculate speed and create line connection from sender to receiver
	public void RecalculateSpeed()
	{
		_line = gameObject.GetComponent<LineRenderer> ();
		_speed = _sHandler.GetSpeedOfGearAtPos (_position.x, _position.y - 1);
		GameObject _receiver = SendScoreToReciever ();
		Color red = new Color (1, 0, 0, 0.5f);
		Color green = new Color (0, 1, 0, 0.5f);
		Color blue = new Color (0, 0, 1, 0.5f);
		switch(_type)
		{
		case GridHandler.Gear.ORANGE_CHECK:
			_line.SetColors(red, red);
			break;
		case GridHandler.Gear.BLUE_CHECK:
			_line.SetColors(blue, blue);
			break;
		case GridHandler.Gear.GREEN_CHECK:
			_line.SetColors(green, green);
			break;
		}
		Material mat = new Material(Shader.Find("Particles/Alpha Blended"));
		_line.material = mat;
		_line.startWidth = 0.04f;
		_line.SetPosition (0, transform.position-new Vector3(0,0,0.05f));
		_line.SetPosition (1, _receiver.transform.position-new Vector3(0,0,0.05f));
		if (_receiver!=null) {
			_receiver.GetComponent<Receiver> ().CookFood ();
		}
		else
			Debug.Log ("No receiver of type " + _type);
	}

	//Send speed calculated to receiver of same type as receiver
	public GameObject SendScoreToReciever()
	{
		foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) {
			if (obj.GetComponent<Receiver> ()) {
				if (obj.GetComponent<Receiver> ()._type == _type) {
					obj.GetComponent<Receiver> ()._speed = _speed;
					return obj;
				}
			}
		}
		return null;
	}

	//Get receiver of same time(Helper function)
	public GameObject GetReceiver()
	{
		foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) {
			if (obj.GetComponent<Receiver> ()) {
				if (obj.GetComponent<Receiver> ()._type == _type) {
					return obj;
				}
			}
		}
		return null;
	}
}
