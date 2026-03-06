using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public GameObject melonPrefab;
	public Transform spawnPoint;
	public TextMeshProUGUI nameText;

	private int generation = 1;

	// Instand Start
	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		SpawnMelon();
	}

	// MelonDied Function - When the Melon Dies this is called
	public void MelonDied()
	{
		generation++;
		Invoke(nameof(SpawnMelon), 1f);
	}

	// Spawn Melon Function - When Spawning the melon, this function is called
	void SpawnMelon()
	{
        GameObject newMelon = Instantiate(melonPrefab, spawnPoint.position, Quaternion.identity);

        nameText.text = "Sir Melonington " + ToRoman(generation);

		CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
		if (cam != null)
		{
			cam.target = newMelon.transform;
		}
	}
	
	// Function to convert int values to Roman Numerals
    string ToRoman(int number)
    {
        string[] thousands = { "", "M", "MM", "MMM" };
        string[] hundreds = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        string[] tens = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        string[] ones = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

        return thousands[number / 1000] +
               hundreds[(number % 1000) / 100] +
               tens[(number % 100) / 10] +
               ones[number % 10];
    }
}
