using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
	

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			print("Saiu da caverna");
			LevelController.instance.fromHole = true;
			SceneManager.LoadScene("Map");
		}
	}
}
