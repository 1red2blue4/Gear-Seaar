using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main class which handles speed calculation and setting of teeth value from level read
public class SpeedHandler : MonoBehaviour {
	private int[,] _gearSpeed=new int[10,5];
	private int[,] _gearTeeth=new int[10,5];
	private GridHandler _grid;
	public int _startingSpeed;
	public bool _gameBreak;
	public bool _cooked=true;

	//If true all dishes are cooked perfectly else not
	public bool GetWinState()
	{
		foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) {
			if (obj.GetComponent<Receiver> ()) {
				_cooked = _cooked & obj.GetComponent<Receiver> ()._cooked;
			}
		}
		return _cooked;
	}

	void Start()
	{
		_grid = gameObject.GetComponent<GridHandler> ();
		PrefabHandler.Instance.GearDetails.text= _startingSpeed+" RPM";
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(0,0,0));
		//PrefabHandler.Instance.GearDetails.GetComponent<RectTransform> ().anchoredPosition = screenPoint-PrefabHandler.Instance.UI.GetComponent<RectTransform>().sizeDelta/2+new Vector2(80,18);
	}

	//Incoming gear and X, Y in grid-> Set gear teeth value
	public void SetGearTeethValue(int X, int Y, GridHandler.Gear _gear)
	{
		switch (_gear) {
		case GridHandler.Gear.EMPTY:
			_gearTeeth [X, Y] = 0;
			break;
		//001
		case GridHandler.Gear.GEAR_13:
			_gearTeeth [X,Y] = 13;
			break;
		case GridHandler.Gear.GEAR_19_LVL1:
			_gearTeeth [X,Y] = 19;
			break;
		case GridHandler.Gear.GEAR_23:
			_gearTeeth [X,Y] = 23;
			break;
		case GridHandler.Gear.GEAR_42:
			_gearTeeth [X,Y] = 42;
			break;
		//002
		case GridHandler.Gear.GEAR_14:
			_gearTeeth [X,Y] = 14;
			break;
		case GridHandler.Gear.GEAR_17:
			_gearTeeth [X,Y] = 17;
			break;
		case GridHandler.Gear.GEAR_25:
			_gearTeeth [X,Y] = 25;
			break;
		case GridHandler.Gear.GEAR_31:
			_gearTeeth [X,Y] = 31;
			break;
		//003
		case GridHandler.Gear.GEAR_3:
			_gearTeeth [X,Y] = 3;
			break;
		case GridHandler.Gear.GEAR_7:
			_gearTeeth [X,Y] = 7;
			break;
		case GridHandler.Gear.GEAR_9:
			_gearTeeth [X,Y] = 9;
			break;
		case GridHandler.Gear.GEAR_11:
			_gearTeeth [X,Y] = 11;
			break;
		case GridHandler.Gear.GEAR_19_LVL3:
			_gearTeeth [X,Y] = 19;
			break;
		//Sample level
		case GridHandler.Gear.GEAR_24:
			_gearTeeth [X,Y] = 24;
			break;
		case GridHandler.Gear.GEAR_32:
			_gearTeeth [X,Y] = 32;
			break;
		case GridHandler.Gear.GEAR_40:
			_gearTeeth [X,Y] = 40;
			break;
		case GridHandler.Gear.GEAR_48:
			_gearTeeth [X,Y] = 48;
			break;
		case GridHandler.Gear.GEAR_56:
			_gearTeeth [X,Y] = 56;
			break;
		case GridHandler.Gear.GEAR_64:
			_gearTeeth [X,Y] = 64;
			break;
		case GridHandler.Gear.MOTOR:
			_gearTeeth [X,Y] = 1;
			break;
		case GridHandler.Gear.ORANGE_CHECK:
			_gearTeeth [X,Y] = -1;
			break;
		case GridHandler.Gear.BLUE_CHECK:
			_gearTeeth [X,Y] = -1;
			break;
		case GridHandler.Gear.GREEN_CHECK:
			_gearTeeth [X,Y] = -1;
			break;
		case GridHandler.Gear.COUPLER:
			_gearTeeth [X,Y] = 1;
			break;
		}
	}

	//Calculate speed for all gears in level
	public void CalculateSpeed()
	{
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 5; j++) {
				_gearSpeed [i,j] = 0;
			}
		}
		_gearSpeed [0,0] = _startingSpeed;
		CalculateSpeedForAllGears (0,0);
		for (int i = 0; i < 10; i++) {
			for (int j = 1; j < 5; j++) {
					_grid.AttachItem (i, j);
				}
			}
		for (int i = 9; i >=0; i--) {
			for (int j = 4; j >=0; j--) {
				if(!_gameBreak)
					CheckIfGameBreaker (i, j);
			}
		}
		//CalculateSenderSpeeds ();
	}

	//Pop off invalid gear if invalid gear is placed and recalculate speed
	IEnumerator PopOffInvalidGear(int X, int Y)
	{
		float elapsedTime=0;
		float t = 0;
		float x = X;
		float y = 0;
		Vector3 start = _grid.GetGearObject (X, Y).transform.position;
		Vector3 final = Vector3.left * 5;
		while (t < 2.0f) {
			ParticleSystem p = Instantiate (PrefabHandler.Instance.GearPop, new Vector3 ((float)X/2, (float)Y/2, 0), Quaternion.Euler(-90,0,0)) as ParticleSystem;
			p.Play ();
			elapsedTime += Time.deltaTime;
			t = elapsedTime / 2.0f;
			x+= t/100;
			y-= (.5f * 10 * (t * t))/100;
			_grid.GetGearObject (X, Y).transform.position = new Vector3 (x, 0, y);
			yield return null;
		}
		_grid.SetTileToType (X, Y, GridHandler.Gear.EMPTY);
		SetGearTeethValue (X, Y, GridHandler.Gear.EMPTY);
		_grid.SetSpeedOfTile (X, Y, 0);
		_grid.AttachItem (X, Y);
		CalculateSpeed ();
	}

	//If invalid gear
	private void CheckIfGameBreaker(int X, int Y)
	{
		if (_gearTeeth [X, Y] > 1) {
			if (X < 9 && _gearTeeth [X + 1, Y] > 1) {
				if (Y > 0 && _gearTeeth [X, Y - 1] > 1) {
					if (_gearSpeed [X + 1, Y] != _gearSpeed [X, Y - 1] && _gearTeeth[X+1,Y-1]>1) {
						Debug.Log ("Game break" + X+ "," + Y);
						_gameBreak = true;
						StartCoroutine(PopOffInvalidGear(X,Y));
					}
				}
				if (Y < 4 && _gearTeeth [X, Y + 1] > 1) {
					if (_gearSpeed [X + 1, Y] != _gearSpeed [X, Y + 1] && _gearTeeth[X+1,Y+1]>1) {
						Debug.Log ("Game break" + X+ "," + Y);
						_gameBreak = true;
						StartCoroutine(PopOffInvalidGear(X,Y));
					}
				}
			} else if (X > 0 && _gearTeeth [X - 1, Y] > 1) {
				if (Y > 0 && _gearTeeth [X, Y - 1] > 1) {
					if (_gearSpeed [X - 1, Y] != _gearSpeed [X, Y - 1] && _gearTeeth[X-1,Y-1]>1) {
						Debug.Log ("Game break" + X+ "," + Y);
						_gameBreak = true;
						StartCoroutine(PopOffInvalidGear(X,Y));
					}
				}
				if (Y < 4 && _gearTeeth [X, Y + 1] > 1) {
					if (_gearSpeed [X - 1, Y] != _gearSpeed [X, Y + 1] && _gearTeeth[X-1,Y+1]>1) {
						Debug.Log ("Game break" + X + "," + Y);
						_gameBreak = true;
						StartCoroutine(PopOffInvalidGear(X,Y));
					}
				}
			}
		}
	}

	//Recursive function to calculate speed for gears in the grid
	private void CalculateSpeedForAllGears(int X, int Y)
	{
		if (_gearTeeth [X, Y] >= 1) {
			_grid.SetSpeedOfTile (X, Y, _gearSpeed [X, Y]);
		}
		if (X < 9 && _gearTeeth [X + 1, Y] > 1 && _gearSpeed [X + 1, Y] == 0) {
			_gearSpeed [X + 1, Y] = (int)(_gearSpeed [X, Y] * _gearTeeth [X, Y] / _gearTeeth [X + 1, Y]);
			CalculateSpeedForAllGears (X + 1, Y);
		}
		if (X>0 && _gearTeeth [X - 1, Y] > 1 && _gearSpeed [X - 1, Y] == 0) {
			_gearSpeed [X - 1, Y] = (int)(_gearSpeed [X, Y] * _gearTeeth [X, Y] / _gearTeeth [X - 1, Y]);
			CalculateSpeedForAllGears (X - 1, Y);
		}
		if (Y<4 && _gearTeeth [X, Y + 1] >= 1 && _gearSpeed [X, Y + 1] == 0) {
			_gearSpeed [X, Y + 1] = _gearSpeed [X, Y];
			float y = (float)Y;
			float x = (float)X;
			if (_grid.GetTileType (X, Y + 1) != GridHandler.Gear.COUPLER && _grid.GetTileType (X, Y) != GridHandler.Gear.COUPLER) {
				GameObject b = Instantiate (PrefabHandler.Instance.Spoke, new Vector3 (x / 2 + 0.025f, y / 2 + 0.235f, -0.08f), Quaternion.Euler (0, 0, 90));
			}
			CalculateSpeedForAllGears (X, Y + 1);
		}
		if (Y>0 && _gearTeeth [X, Y - 1] >= 1 && _gearSpeed [X, Y - 1] == 0) {
			_gearSpeed [X, Y - 1] = _gearSpeed [X, Y];
			float y = (float)Y;
			float x = (float)X;
			if (_grid.GetTileType (X, Y - 1) != GridHandler.Gear.COUPLER && _grid.GetTileType (X, Y) != GridHandler.Gear.COUPLER) {
				GameObject b=Instantiate (PrefabHandler.Instance.Spoke, new Vector3 (x / 2 + 0.025f, y / 2 - 0.235f, -0.08f), Quaternion.Euler (0, 0, 90));

			}
			CalculateSpeedForAllGears (X, Y - 1);
		}
	}

	//Helper function to get speed at X,Y
	public int GetSpeedOfGearAtPos(int X, int Y)
	{
		if (X >= 0 && X < 10 && Y >= 0 && Y < 5)
			return _gearSpeed [X, Y];
		else
			return 0;
	}

	//Set speed in sender using appropriate gear speeds
	public void CalculateSenderSpeeds()
	{
		foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) {
			if (obj.GetComponent<Sender> ()) {
				obj.GetComponent<Sender> ().RecalculateSpeed ();
			}
		}
	}
}
