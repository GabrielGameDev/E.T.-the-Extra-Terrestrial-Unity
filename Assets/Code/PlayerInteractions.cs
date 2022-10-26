using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{

	public delegate void PlayerAction();
	public static event PlayerAction OnTelephoneZone;
	public static event PlayerAction OnChangeScreen;

	public GameObject[] cameras;
	public Transform[] spawnPoints;

	public AudioClip eatSound, actionSound, telephoneSound, modemSound;

	public GameObject spaceShip;
	public GameObject telephone;

	ActionZoneProps actionZone;
	AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();

		LevelController.instance.SetScoreText(0);

		if (LevelController.instance.fromHole)
		{
			actionZone = LevelController.instance.actionZone;
			actionZone.nextCamera = cameras[actionZone.index];
			actionZone.nextSpawnPoint = spawnPoints[actionZone.index];
			if (LevelController.instance.TelephoneComplete())
				telephone.SetActive(true);
			NextScreen();
			LevelController.instance.fromHole = false;
		}
	}

	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if(actionZone != null)
			{
				if(actionZone.action == Action.changeScene)
				{
					audioSource.PlayOneShot(actionSound);
					UpdateScore(-100);
					NextScreen();
				}
				else if(actionZone.action == Action.telephone)
				{
					if(OnTelephoneZone != null)
					{
						audioSource.PlayOneShot(actionSound);
						UpdateScore(-100);
						OnTelephoneZone();
					}
				}
				else if(actionZone.action == Action.eat)
				{
					if(LevelController.instance.candyAmount > 0)
					{
						audioSource.PlayOneShot(eatSound);
						UpdateScore(200);
						LevelController.instance.candyAmount--;
					}
				}
				else if(actionZone.action == Action.callSpaceship)
				{
					audioSource.PlayOneShot(modemSound);
					audioSource.PlayOneShot(actionSound);
					UpdateScore(500);
					spaceShip.SetActive(true);
				}
			}
		}
	}

	private void NextScreen()
	{
		LevelController.instance.actionZone = actionZone;
		for (int i = 0; i < cameras.Length; i++)
		{
			cameras[i].SetActive(false);
		}
		transform.position = actionZone.nextSpawnPoint.position;
		actionZone.nextCamera.SetActive(true);

		if (OnChangeScreen != null)
			OnChangeScreen();
		actionZone = null;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Telephone"))
		{
			audioSource.PlayOneShot(telephoneSound);
			Caverna caverna = FindObjectOfType<Caverna>();
			LevelController.instance.TakeTelephone(caverna.currentIndex);
			Destroy(other.gameObject);
		}
		else if (other.CompareTag("Ship"))
		{
			SceneManager.LoadScene("Win");
		}
		
		

		ActionZone newActionZone = other.GetComponent<ActionZone>();
		if(newActionZone != null)
		{
			actionZone = newActionZone.props;
			LevelController.instance.SetAction(actionZone.action, actionZone.sprite);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		ActionZone newActionZone = other.GetComponent<ActionZone>();
		if (newActionZone != null)
		{
			LevelController.instance.SetAction(Action.noAction, null);
			actionZone = null;
		}
	}

	public void UpdateScore(int amount)
	{
		LevelController.instance.SetScoreText(amount);
	}



}
