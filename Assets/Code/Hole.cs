using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    public int index;
    SpriteRenderer spriteRenderer;

	private void Start()
	{
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        DisableSprite();
	}
	void OnEnable()
    {
        PlayerInteractions.OnTelephoneZone += CheckIndex;
    }


    void OnDisable()
    {
        PlayerInteractions.OnTelephoneZone -= CheckIndex;
    }

    void CheckIndex()
	{
		if (LevelController.instance.chosenHoles.Contains(index))
		{
            spriteRenderer.enabled = true;
            Invoke("DisableSprite", 0.25f);
		}
	}

    void DisableSprite()
	{
        spriteRenderer.enabled = false;
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			LevelController.instance.Fall(index);
			SceneManager.LoadScene("Caverna");
		}
	}

}
