using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

public class CouplerRot : MonoBehaviour {

	public int _speed;
	private IVector3 _pos;
	public IVector3 Position
	{
		set { _pos = value; }
		get { return _pos; }
	}
	private GridHandler _grid;

	void Start()
	{
		_grid = GameObject.Find ("GameHandler").GetComponent<GridHandler> ();
		PrefabHandler.Instance.GearDetails.text= _speed+" RPM";
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(_pos.x,_pos.y,_pos.z)/2);
		PrefabHandler.Instance.GearDetails.GetComponent<RectTransform> ().anchoredPosition = screenPoint-PrefabHandler.Instance.UI.GetComponent<RectTransform>().sizeDelta/2+new Vector2(80,18);
	}

	//Rotate gears based on speed calculated
	void Update () {
		GetComponent<Animator> ().speed = _speed;
	}

	//Left click get speed of gear, right click delete and recalcuate speed
	void OnMouseOver()
	{
		if (Input.GetMouseButton (1)) {
			_grid.SetTileToType (_pos.x, _pos.y, GridHandler.Gear.EMPTY);
			PrefabHandler.Instance.SHandler.SetGearTeethValue (_pos.x, _pos.y, GridHandler.Gear.EMPTY);
			_grid.SetSpeedOfTile (_pos.x, _pos.y, 0);
			_grid.SetSpeedOfTile (_pos.x-1, _pos.y, 0);
			_grid.SetSpeedOfTile (_pos.x+1, _pos.y, 0);
			_grid.SetSpeedOfTile (_pos.x, _pos.y-1, 0);
			_grid.SetSpeedOfTile (_pos.x, _pos.y+1, 0);
			PrefabHandler.Instance.SHandler.CalculateSpeed ();
			PrefabHandler.Instance.SHandler.CalculateSenderSpeeds ();
			_grid.AttachItem (_pos.x, _pos.y);
		}
		if (Input.GetMouseButton (0)) {
			Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(_pos.x,_pos.y,_pos.z)/2);
			PrefabHandler.Instance.GearDetails.GetComponent<RectTransform> ().anchoredPosition = screenPoint-PrefabHandler.Instance.UI.GetComponent<RectTransform>().sizeDelta/2+new Vector2(80,18);
			PrefabHandler.Instance.GearDetails.text = _speed+" RPM";
		}
	}
}
