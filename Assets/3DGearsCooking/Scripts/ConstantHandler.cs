using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

//Singleton which stores all the constant values in the game
public class ConstantHandler : Singleton<ConstantHandler> {

	private bool _componentAdded;
	public bool ComponentAdded
	{
		set { _componentAdded = value; }
		get { return _componentAdded; }
	}
	private IVector3 _positionAdded;
	public IVector3 PositionAdded
	{
		set { _positionAdded = value; }
		get { return _positionAdded; }
	}
	private bool _isUIdragged;
	public bool ComponentDragged
	{
		set { _isUIdragged = value; }
		get { return _isUIdragged; }
	}
	[SerializeField]
	private Vector3 _gear1Scale;
	public Vector3 Gear1Scale
	{
		get { return _gear1Scale; }
	}
	[SerializeField]
	private Vector3 _gear2Scale;
	public Vector3 Gear2Scale
	{
		get { return _gear2Scale; }
	}
	[SerializeField]
	private Vector3 _gear3Scale;
	public Vector3 Gear3Scale
	{
		get { return _gear3Scale; }
	}
	[SerializeField]
	private Vector3 _gear4Scale;
	public Vector3 Gear4Scale
	{
		get { return _gear4Scale; }
	}
	[SerializeField]
	private Vector3 _gear5Scale;
	public Vector3 Gear5Scale
	{
		get { return _gear5Scale; }
	}
	[SerializeField]
	private Vector3 _gear6Scale;
	public Vector3 Gear6Scale
	{
		get { return _gear6Scale; }
	}
}
