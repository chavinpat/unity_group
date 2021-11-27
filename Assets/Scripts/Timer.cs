using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float initialTime = 40f;
    private float currentTime;

    private List<CharacterMovement> characters = new List<CharacterMovement>();

    // Start is called before the first frame update
    void Start()
    {
        currentTime = initialTime;
        characters = FindObjectsOfType<CharacterMovement>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentTime"+currentTime);
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            
            TimeSpan span = TimeSpan.FromSeconds(currentTime);
            timerText.text = span.ToString(@"mm\:ss");
            

            return;
        }

        // TODO: Kill all players that have not crossed the finish line
        else
        {
            var charsToRemove = new List<CharacterMovement>();

            foreach (var character in characters)
            {
                if (character.IsInvulnerable == false)
                {
                    charsToRemove.Add(character);
                }
            }

            foreach (var character in charsToRemove)
            {
                characters.Remove(character);
                character.Die();
            }
        }
        Debug.Log("Kill all players!");
    }
}
