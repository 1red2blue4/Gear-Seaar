using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class is used for checking if corresponding sender has correct speed/temperature
public class Receiver : MonoBehaviour {
	//Incoming speed/temp from corresponding sender
	public int _speed;
	public GridHandler.Gear _type;
	Color color;
	//This is important! During creation of levels, change associated food and type for senders and receivers
	public GridHandler.Food _associatedFood;
	//Get this from GridHandler.FoodTemp
	private float _requiredTemp;
	float t=0.0f;
	bool _perfect;
	Color _perfectEnd=new Color(255,255,255,255);
	float step=0;
	public bool _cooked=false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private enum CookType
	{
		PERFECT, OVERCOOK, UNDERCOOK
	}

	//Change color(overcooked or undercooked)
	IEnumerator ChangeColor(float time, Color final, CookType i)
	{
		float elapsedTime=0;
		float t = 0;
		Color start=new Color(1,1,1,1);
		if(i==CookType.UNDERCOOK)
		transform.GetChild(8).GetComponent<SpriteRenderer>().sprite=transform.GetChild(8).GetComponent<foodSprites>().undercooked;
		if(i==CookType.OVERCOOK)
			transform.GetChild(8).GetComponent<SpriteRenderer>().sprite=transform.GetChild(8).GetComponent<foodSprites>().overcooked;
		if(i==CookType.PERFECT)
			transform.GetChild(8).GetComponent<SpriteRenderer>().sprite=transform.GetChild(8).GetComponent<foodSprites>().cooked;
		final=new Color(1,1,1,0);
		while (t < time) {
			elapsedTime += Time.deltaTime;
			t = elapsedTime / time;
			gameObject.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (start, final, t);
			yield return null;
		}
		/*switch (i) {
		case CookType.OVERCOOK:
			StartCoroutine (ChangeColor (2.0f, new Color (0, 0, 0, 255), CookType.OVERCOOK));
			break;
		}*/
	}

	//Start fire in coils visual
	IEnumerator StartFire()
	{
		float time = 5.0f;
		float elapsedTime=0;
		float t = 0;
		Color startCoil = Color.white;
		Color finalCoil = Color.red;
		while (t < time) {
			elapsedTime += Time.deltaTime;
			t = elapsedTime / time;
			gameObject.transform.GetChild (2).gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (startCoil, finalCoil, t);
			yield return null;
		}
	}

	//Reset to blue again
	public void RestartColor()
	{
		gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = Color.white;
		gameObject.transform.GetChild (2).GetComponent<SpriteRenderer> ().color = Color.white;
	}

	//Cook food is called everytime gear is added/deleted or motor placement is changed
	public void CookFood()
	{
		StopAllCoroutines ();
		RestartColor ();
		switch (_associatedFood) {
		case GridHandler.Food.CHICKEN:
			_requiredTemp = (int)GridHandler.FoodTemp.CHICKEN;
			break;
		case GridHandler.Food.FISH:
			_requiredTemp = (int)GridHandler.FoodTemp.FISH;
			break;
		case GridHandler.Food.ALLIGATOR:
			_requiredTemp = (int)GridHandler.FoodTemp.ALLIGATOR;
			break;
		case GridHandler.Food.DUCK:
			_requiredTemp = (int)GridHandler.FoodTemp.DUCK;
			break;
		case GridHandler.Food.SNAIL:
			_requiredTemp = (int)GridHandler.FoodTemp.SNAIL;
			break;
		case GridHandler.Food.ST_PATTY:
			_requiredTemp = (int)GridHandler.FoodTemp.ST_PATTY;
			break;
		}
		if (_speed > 100) {
			transform.GetChild (3).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
		} else {
			transform.GetChild (3).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
		}
		if (_speed > 0) {
			if (_speed > _requiredTemp + 5) {
				_cooked = false;
				transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
				StartCoroutine (StartFire ());
				StartCoroutine (ChangeColor (2.0f, new Color (1, 0, 0, 255), CookType.OVERCOOK));
			
			} else if (_speed < _requiredTemp - 5) {
				_cooked = false;
				transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
				StartCoroutine (StartFire ());
				StartCoroutine (ChangeColor (4.0f, new Color (_speed / _requiredTemp, _speed / _requiredTemp, 1, 255), CookType.UNDERCOOK));
			} else {
				_cooked = true;
				transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
				StartCoroutine (StartFire ());
				StartCoroutine (ChangeColor (4.0f, new Color (1, 1, 1, 255), CookType.PERFECT));
			}
		}
		if (_speed == 0) {
			_cooked = false;
			transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
		}
}
}
