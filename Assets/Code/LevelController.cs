using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum Action { noAction, changeScene, eat, telephone, callSpaceship}


public class LevelController : MonoBehaviour
{
    public static LevelController instance;
	public Action action;    
    public Image actionIcon;
	Sprite defaultSprite;

	public TextMeshProUGUI scoreText;

	public List<int> holes;
	public List<int> chosenHoles;
	public ActionZoneProps actionZone;
	public bool fromHole;
	public int currentHole;

	public GameObject[] telephonePartImages;

	public int candyAmount;
	public bool[] candyTake;

	public GameObject title;


	int score = 9999;
	bool started = true;
	private void Awake()
	{
		defaultSprite = actionIcon.sprite;
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else 
		{ 
			Destroy(gameObject);
		}

		for (int i = 0; i < 3; i++)
		{
			int random = Random.Range(0, holes.Count);
			chosenHoles.Add(holes[random]);
			int removeRandom = holes[random];
			holes.Remove(removeRandom);
		}


	}


	private IEnumerator Start()
	{
		yield return new WaitForSeconds(1);
		started = false;
	}

	private void Update()
	{
		if (Input.anyKeyDown && !started)
		{
			actionIcon.transform.parent.gameObject.SetActive(true);
			title.SetActive(false);
			started = true;
			scoreText.text = "Carregando...";
			SceneManager.LoadScene("Map");
		}
	}


	

	public void SetAction(Action action, Sprite sprite)
	{
		this.action = action;
		if (sprite is null)
		{
			actionIcon.sprite = defaultSprite;
		}
		else
		{
			actionIcon.sprite = sprite;
		}		
		
	}	

	
	public void Fall(int index)
	{			
		currentHole = index;
	}

	public void TakeTelephone(int index)
	{
		telephonePartImages[index].SetActive(true);
	}

	public void DisableTelephone()
	{
		for (int i = 0; i < telephonePartImages.Length; i++)
		{
			telephonePartImages[i].SetActive(false);
		}

		FindObjectOfType<PlayerInteractions>().telephone.SetActive(false);
	}

	public int GetTelephoneIndex(int index)
	{
		for (int i = 0; i < telephonePartImages.Length; i++)
		{
			if (telephonePartImages[i].activeInHierarchy)
				return i;
		}

		return 10;
	}

	public bool TelephoneComplete()
	{
		for (int i = 0; i < telephonePartImages.Length; i++)
		{
			if (!telephonePartImages[i].activeInHierarchy)
				return false;
		}

		return true;
	}

	public void SetScoreText(int score)
	{
		this.score += score;
		scoreText.text = this.score.ToString();
		if(this.score <= 0)
		{
			this.score = 0;
			SceneManager.LoadScene("GameOver");
		}
	}

}
