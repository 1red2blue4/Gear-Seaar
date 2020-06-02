using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CustomTools;

public class DragItem : MonoBehaviour,IDragHandler,IEndDragHandler {

	private GridHandler _grid;
	private SpeedHandler _sHandler;
	public GridHandler.Gear _itemType;
	GameObject _previewPiece;
	void Start()
	{
		_grid = GameObject.Find ("GameHandler").GetComponent<GridHandler> ();
		_sHandler = GameObject.Find ("GameHandler").GetComponent<SpeedHandler> ();

	}

	//Create preview piece on dragging
	public void OnDrag(PointerEventData data)
	{
		ConstantHandler.Instance.ComponentDragged = true;
		if (ConstantHandler.Instance.ComponentAdded) {
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			Destroy (_previewPiece);
			switch (_itemType) {
			//001
			case GridHandler.Gear.GEAR_13:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear1;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear1Scale;
				break;
			case GridHandler.Gear.GEAR_19_LVL1:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear2;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear2Scale;
				break;
			case GridHandler.Gear.GEAR_23:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear3;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear3Scale;
				break;
			case GridHandler.Gear.GEAR_42:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear4;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear4Scale;
				break;
			//002
			case GridHandler.Gear.GEAR_14:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear1;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear1Scale;
				break;
			case GridHandler.Gear.GEAR_17:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear2;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear2Scale;
				break;
			case GridHandler.Gear.GEAR_25:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear3;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear3Scale;
				break;
			case GridHandler.Gear.GEAR_31:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear4;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear4Scale;
				break;
			//003
			case GridHandler.Gear.GEAR_3:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear1;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear1Scale;
				break;
			case GridHandler.Gear.GEAR_7:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear2;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear2Scale;
				break;
			case GridHandler.Gear.GEAR_9:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear3;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear3Scale;
				break;
			case GridHandler.Gear.GEAR_11:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear4;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear4Scale;
				break;
			case GridHandler.Gear.GEAR_19_LVL3:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear5;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear5Scale;
				break;
			//Sample level
			case GridHandler.Gear.GEAR_24:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear1;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear1Scale;
				break;
			case GridHandler.Gear.GEAR_32:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear2;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear2Scale;
				break;
			case GridHandler.Gear.GEAR_40:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear3;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear3Scale;
				break;
			case GridHandler.Gear.GEAR_48:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear4;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear4Scale;
				break;
			case GridHandler.Gear.GEAR_56:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear5;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear5Scale;
				break;
			case GridHandler.Gear.GEAR_64:
				_previewPiece = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_previewPiece.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear6;
				_previewPiece.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear6Scale;
				break;
			case GridHandler.Gear.ORANGE_CHECK:
				_previewPiece = Instantiate (PrefabHandler.Instance.OrangeCheck, new Vector3 ((float)pos.x/2, (float)pos.y/2, -0.04f), Quaternion.identity)
					as GameObject;
				_previewPiece.GetComponent<Animator> ().enabled = false;
				break;
			case GridHandler.Gear.GREEN_CHECK:
				_previewPiece = Instantiate (PrefabHandler.Instance.GreenCheck, new Vector3 ((float)pos.x/2, (float)pos.y/2, -0.04f), Quaternion.identity)
					as GameObject;
				_previewPiece.GetComponent<Animator> ().enabled = false;
				break;
			case GridHandler.Gear.BLUE_CHECK:
				_previewPiece = Instantiate (PrefabHandler.Instance.BlueCheck, new Vector3 ((float)pos.x / 2, (float)pos.y / 2, -0.04f), Quaternion.identity)
					as GameObject;
				_previewPiece.GetComponent<Animator> ().enabled = false;
				break;
			case GridHandler.Gear.MOTOR:
				_previewPiece = Instantiate (PrefabHandler.Instance.Motor, new Vector3 ((float)pos.x / 2, (float)pos.y / 2, 0), Quaternion.identity)
					as GameObject;
				break;
			case GridHandler.Gear.COUPLER:
				_previewPiece = Instantiate (PrefabHandler.Instance.Coupler, new Vector3 ((float)pos.x / 2+0.025f, (float)pos.y / 2, -0.075f), Quaternion.identity)
					as GameObject;
				break;
			}
		}
	}

	//APply gear/component on gridspot
	public void OnEndDrag(PointerEventData data)
	{
		if(_previewPiece!=null)
			Destroy (_previewPiece);
		ConstantHandler.Instance.ComponentDragged = false;
		if (ConstantHandler.Instance.ComponentAdded) {
			IVector3 pos = ConstantHandler.Instance.PositionAdded;
			_grid.SetTileToType (pos.x, pos.y, _itemType);
			_sHandler.SetGearTeethValue (pos.x, pos.y, _itemType);
			_sHandler.CalculateSpeed ();
			_grid.AttachItem (pos.x, pos.y);
		}
	}
}
