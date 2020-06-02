using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;
using UnityEngine.SceneManagement;

public class GridHandler : MonoBehaviour {

	private PuzzletPosition[,] _puzzlets=new PuzzletPosition[10,10];
	private Dictionary<GameObject, Vector3> _puzzletsPos=new Dictionary<GameObject, Vector3>();
	//Create gridspaces for the gears to be put in
	void Start()
	{
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 5; j++) {
					Vector3 pos = new Vector3 ((float)i / 2, (float)j / 2, 0);
					IVector3 ipos = new IVector3 (i, j, 0); 
					GameObject obj = Instantiate (PrefabHandler.Instance.EmptySphere, pos, Quaternion.identity) as GameObject;
					if (i == 0 && j == 0) {
					//Do NOTHING
					} else {
						obj.AddComponent<AddItem> ();
						obj.GetComponent<AddItem> ().Position = ipos;
					}
					_puzzlets [i, j] = new PuzzletPosition (obj,ipos);
					_puzzletsPos.Add (obj, pos);
					if (j == 0) {
					if (i > 0) {
						obj.GetComponent<Renderer> ().enabled = false;
					}
					}
				}
			}
		SetTileToType (0, 0, Gear.MOTOR);
		gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (0, 0, Gear.MOTOR);
		AttachItem (0, 0);
		//Set default motor in level
		if(GameObject.Find("GreenReceiver")){
			SetTileToType (0, 4, Gear.GREEN_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (0, 4, Gear.GREEN_CHECK);
			AttachItem (0, 4);
		}if(GameObject.Find("BlueReceiver")){
			SetTileToType (9, 4, Gear.BLUE_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (9, 4, Gear.BLUE_CHECK);
			AttachItem (9, 4);
		}if(GameObject.Find("OrangeReceiver")){
			SetTileToType (4, 4, Gear.ORANGE_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (4, 4, Gear.ORANGE_CHECK);
			AttachItem (4, 4);
		}/*
		switch (SceneManager.GetActiveScene ().buildIndex) {
		case 0:
			SetTileToType (0, 4, Gear.GREEN_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (0, 4, Gear.GREEN_CHECK);
			AttachItem (0, 4);
			/*SetTileToType (9, 4, Gear.BLUE_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (9, 4, Gear.BLUE_CHECK);
			AttachItem (9, 4);
			break;
		case 1:
			SetTileToType (0, 4, Gear.GREEN_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (0, 4, Gear.GREEN_CHECK);
			AttachItem (0, 4);
			SetTileToType (9, 4, Gear.BLUE_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (9, 4, Gear.BLUE_CHECK);
			AttachItem (9, 4);
			break;
		case 2:
			SetTileToType (0, 4, Gear.GREEN_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (0, 4, Gear.GREEN_CHECK);
			AttachItem (0, 4);
			SetTileToType (9, 4, Gear.BLUE_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (9, 4, Gear.BLUE_CHECK);
			AttachItem (9, 4);
			SetTileToType (4, 4, Gear.ORANGE_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (4, 4, Gear.ORANGE_CHECK);
			AttachItem (4, 4);
			break;
		default:
			if(GameObject.Find("GreenReceiver")){
			SetTileToType (0, 4, Gear.GREEN_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (0, 4, Gear.GREEN_CHECK);
			AttachItem (0, 4);
			}if(GameObject.Find("BlueReceiver")){
			SetTileToType (9, 4, Gear.BLUE_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (9, 4, Gear.BLUE_CHECK);
			AttachItem (9, 4);
			}if(GameObject.Find("OrangeReceiver")){
			SetTileToType (4, 4, Gear.ORANGE_CHECK);
			gameObject.GetComponent<SpeedHandler> ().SetGearTeethValue (4, 4, Gear.ORANGE_CHECK);
			AttachItem (4, 4);
			}
			break;
		}*/
	}

	//Helper functions
	public bool isOnGrid(int X, int Y)
	{
		return (X>=0 && X<10 && Y>=0 && Y<5);
	}

	public void SetTileToType(int X, int Y, Gear type)
	{
		if (isOnGrid (X, Y))
			_puzzlets [X, Y].ComponentType = type;
	}

	public void SetSpeedOfTile(int X, int Y, int _speed)
	{
		if (isOnGrid (X, Y))
			_puzzlets [X, Y].Speed = _speed;
	}

	public Gear GetTileType(int X, int Y)
	{
		if (isOnGrid (X, Y))
			return _puzzlets [X, Y].ComponentType;
		return Gear.EMPTY;
	}

	public void AttachItem(int X, int Y)
	{
		if (isOnGrid (X, Y))
			_puzzlets [X, Y].AttachItem ();
	}

	public GameObject GetGearObject(int X, int Y)
	{
		if (isOnGrid (X, Y))
			return _puzzlets [X, Y].ComponentAttached.transform.GetChild(0).gameObject;
		return null;
	}

	public GameObject GetObj(int X, int Y)
	{
		if (isOnGrid (X, Y))
			return _puzzlets [X, Y].ComponentAttached;
		return null;
	}

	//Gear types
	public enum Gear
	{
		EMPTY,
		MOTOR,
		GREEN_CHECK,
		ORANGE_CHECK,
		BLUE_CHECK,
		//001
		GEAR_13,
		GEAR_19_LVL1,
		GEAR_23,
		GEAR_42,
		//002
		GEAR_14,
		GEAR_17,
		GEAR_25,
		GEAR_31,
		//003
		GEAR_3,
		GEAR_7,
		GEAR_9,
		GEAR_11,
		GEAR_19_LVL3,
		//Sample level
		GEAR_24,
		GEAR_32,
		GEAR_40,
		GEAR_48,
		GEAR_56,
		GEAR_64,
		COUPLER
	}

	//Food types
	public enum Food
	{
		CHICKEN,
		FISH,
		ALLIGATOR,
		DUCK,
		SNAIL,
		ST_PATTY
	}

	//Food temperatures
	public enum FoodTemp
	{
		CHICKEN=235,
		FISH=135,
		ALLIGATOR=300,
		DUCK=163,
		SNAIL=75,
		ST_PATTY=280
	}

	//Struct to store tile position on grid
	public struct PuzzletPosition
	{
		private IVector3 pos;
		public IVector3 Position
		{
			set { pos = value; }
			get { return pos; }
		}

		private int _speed;
		public int Speed
		{
			set { _speed = value; }
			get { return _speed; }
		}

		private GameObject _obj;
		public GameObject Obj
		{
			set { _obj = value; }
			get { return _obj; }
		}

		private GameObject _componentAttached;
		public GameObject ComponentAttached
		{
			set { _componentAttached = value; }
			get { return _componentAttached; }
		}

		private GridHandler.Gear _componentType;
		public GridHandler.Gear ComponentType
		{
			set { _componentType = value; }
			get { return _componentType; }
		}

		public PuzzletPosition(GameObject obj, IVector3 posi)
		{
			_obj = obj;
			_speed=0;
			pos = posi;
			_componentAttached = null;
			_componentType = GridHandler.Gear.EMPTY;
		}

		public void ClearItem()
		{
			if (_componentAttached != null) {
				Destroy (_componentAttached);
				_obj.GetComponent<Renderer> ().enabled = true;
				_obj.GetComponent<Renderer> ().enabled = true;
			}
		}

		public void AttachItem()
		{
			ClearItem ();
			PrefabHandler.Instance.SHandler._cooked = true;
			switch (_componentType) {
			case GridHandler.Gear.EMPTY:
				_obj.GetComponent<Renderer> ().enabled = true;
				_obj.GetComponent<Renderer> ().enabled = true;
				break;
				//Sample level
			case GridHandler.Gear.GEAR_24:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x / 2, (float)pos.y / 2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear1;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear1Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_32:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear2;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear2Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_40:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear3;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear3Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_48:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear4;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear4Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_56:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear5;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear5Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_64:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear6;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear6Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			//001
			case Gear.GEAR_13:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear1;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear1Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_19_LVL1:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear2;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear2Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_23:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear3;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear3Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_42:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear4;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear4Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
				//002
			case Gear.GEAR_14:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear1;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear1Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_17:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear2;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear2Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_25:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear3;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear3Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_31:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear4;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear4Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			//003
			case Gear.GEAR_3:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear1;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear1Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_7:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear2;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear2Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_9:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear3;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear3Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_11:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear4;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear4Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			case GridHandler.Gear.GEAR_19_LVL3:
				_componentAttached = Instantiate (PrefabHandler.Instance.Gear, new Vector3 ((float)pos.x/2, (float)pos.y/2 - 0.25f, 0), Quaternion.identity)
					as GameObject;
				_componentAttached.transform.GetChild(0).gameObject.GetComponent<Renderer> ().material = PrefabHandler.Instance.Gear5;
				_componentAttached.transform.GetChild (0).gameObject.transform.localScale = ConstantHandler.Instance.Gear5Scale;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<GearRot> ();
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ()._speed = _speed;
				_componentAttached.transform.GetChild (0).gameObject.GetComponent<GearRot> ().Position = pos;
				_componentAttached.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ();
				break;
			//Motor and checks
			case GridHandler.Gear.MOTOR:
				_componentAttached = Instantiate (PrefabHandler.Instance.Motor, new Vector3 ((float)pos.x / 2, (float)pos.y / 2, 0), Quaternion.Euler(0,0,180))
					as GameObject;
				break;
			case GridHandler.Gear.COUPLER:
				_componentAttached = Instantiate (PrefabHandler.Instance.Coupler, new Vector3 ((float)pos.x/2+0.025f, (float)pos.y/2f, -0.075f), Quaternion.identity)
					as GameObject;
				_componentAttached.AddComponent<CouplerRot> ();
				_componentAttached.GetComponent<CouplerRot> ()._speed = _speed;
				_componentAttached.GetComponent<CouplerRot> ().Position = pos;
				break;
			case GridHandler.Gear.ORANGE_CHECK:
				_componentAttached = Instantiate (PrefabHandler.Instance.OrangeCheck, new Vector3 ((float)pos.x / 2, (float)pos.y / 2, -0.04f), Quaternion.identity)
					as GameObject;
				_componentAttached.AddComponent<Sender> ();
				_componentAttached.GetComponent<Sender> ().Position = pos;
				_componentAttached.GetComponent<Sender> ().Type = GridHandler.Gear.ORANGE_CHECK;
				_componentAttached.GetComponent<Sender> ()._sHandler = PrefabHandler.Instance.SHandler;
				_componentAttached.GetComponent<Sender> ().RecalculateSpeed ();
				if (PrefabHandler.Instance.SHandler.GetWinState ()) {
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (5).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
				} else if (_componentAttached.GetComponent<Sender> ().GetReceiver ().GetComponent<Receiver> ()._cooked) {
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (5).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
				} else {
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (5).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
				}
				break;
			case GridHandler.Gear.GREEN_CHECK:
				_componentAttached = Instantiate (PrefabHandler.Instance.GreenCheck, new Vector3 ((float)pos.x / 2, (float)pos.y / 2, -0.04f), Quaternion.identity)
					as GameObject;
				_componentAttached.AddComponent<Sender> ();
				_componentAttached.GetComponent<Sender> ().Position = pos;
				_componentAttached.GetComponent<Sender> ().Type = GridHandler.Gear.GREEN_CHECK;
				_componentAttached.GetComponent<Sender> ()._sHandler = PrefabHandler.Instance.SHandler;
				_componentAttached.GetComponent<Sender> ().RecalculateSpeed ();
				if (PrefabHandler.Instance.SHandler.GetWinState ()) {
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (5).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
				} else if (_componentAttached.GetComponent<Sender> ().GetReceiver ().GetComponent<Receiver> ()._cooked) {
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (5).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
				} else {
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (5).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
				}
				break;
			case GridHandler.Gear.BLUE_CHECK:
				_componentAttached = Instantiate (PrefabHandler.Instance.BlueCheck, new Vector3 ((float)pos.x / 2, (float)pos.y / 2, -0.04f), Quaternion.identity)
					as GameObject;
				_componentAttached.AddComponent<Sender> ();
				_componentAttached.GetComponent<Sender> ().Position = pos;
				_componentAttached.GetComponent<Sender> ().Type = GridHandler.Gear.BLUE_CHECK;
				_componentAttached.GetComponent<Sender> ()._sHandler = PrefabHandler.Instance.SHandler;
				_componentAttached.GetComponent<Sender> ().RecalculateSpeed ();
				if (PrefabHandler.Instance.SHandler.GetWinState ()) {
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (5).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
				} else if (_componentAttached.GetComponent<Sender> ().GetReceiver ().GetComponent<Receiver> ()._cooked) {
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (5).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
				} else {
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
					_componentAttached.GetComponent<Sender> ().GetReceiver ()
						.transform.GetChild (5).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
				}
				break;
			}
		}
	}

}
