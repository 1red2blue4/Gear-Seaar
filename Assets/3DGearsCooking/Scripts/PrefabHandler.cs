using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;
using UnityEngine.UI;

//Singleton to store prefabs used in the game
public class PrefabHandler :  Singleton<PrefabHandler>{

	[SerializeField]
	private GameObject _emptySphere;
	public GameObject EmptySphere
	{
		get { return _emptySphere; }
	}
	[SerializeField]
	private GameObject _gear;
	public GameObject Gear
	{
		get { return _gear; }
	}
	[SerializeField]
	private Material _gear1;
	public Material Gear1
	{
		get { return _gear1; }
	}
	[SerializeField]
	private Material _gear2;
	public Material Gear2
	{
		get { return _gear2; }
	}
	[SerializeField]
	private Material _gear3;
	public Material Gear3
	{
		get { return _gear3; }
	}
	[SerializeField]
	private Material _gear4;
	public Material Gear4
	{
		get { return _gear4; }
	}
	[SerializeField]
	private Material _gear5;
	public Material Gear5
	{
		get { return _gear5; }
	}
	[SerializeField]
	private Material _gear6;
	public Material Gear6
	{
		get { return _gear6; }
	}
	[SerializeField]
	private GameObject _motor;
	public GameObject Motor
	{
		get { return _motor; }
	}
	[SerializeField]
	private GameObject _orangeCheck;
	public GameObject OrangeCheck
	{
		get { return _orangeCheck; }
	}
	[SerializeField]
	private GameObject _blueCheck;
	public GameObject BlueCheck
	{
		get { return _blueCheck; }
	}
	[SerializeField]
	private GameObject _greenCheck;
	public GameObject GreenCheck
	{
		get { return _greenCheck; }
	}
	[SerializeField]
	private SpeedHandler _sHandler;
	public SpeedHandler SHandler
	{
		get { return _sHandler; }
	}
	[SerializeField]
	private Text _gearDetails;
	public Text GearDetails
	{
		get { return _gearDetails; }
	}
	[SerializeField]
	private Canvas _canvas;
	public Canvas UI
	{
		get { return _canvas; }
	}
	[SerializeField]
	private ParticleSystem _gearPop;
	public ParticleSystem GearPop
	{
		get { return _gearPop; }
	}
	[SerializeField]
	private GameObject spoke;
	public GameObject Spoke
	{
		get { return spoke; }
	}
	[SerializeField]
	private GameObject coupler;
	public GameObject Coupler
	{
		get { return coupler; }
	}
}
