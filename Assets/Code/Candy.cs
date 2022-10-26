using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    public int index;

	public AudioClip collectSound;


    // Start is called before the first frame update
    void Start()
    {		

		if (LevelController.instance.candyTake[index])
		{
            gameObject.SetActive(false);
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position);
			LevelController.instance.candyTake[index] = true;
			LevelController.instance.candyAmount++;
			gameObject.SetActive(false);
		}
	}
}
