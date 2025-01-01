using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Interactive : MonoBehaviour
{
    enum Character { Orc, Mage, Warrior }

    private Character playerChoice;
    private Character computerChoice;

    // Посилання на об'єкти персонажів
    public GameObject playerOrc, playerMage, playerWarrior;
    public GameObject computerOrc, computerMage, computerWarrior;

    public TMP_Text resultText;

    void Start()
    {
        //ResetCharacters();
        resultText.text = "Оберіть персонажа!";
    }

    // Метод для вибору гравця
    public void PlayerChoice(string choice)
    {
        ResetCharacters();

        switch (choice)
        {
            case "Orc":
                playerChoice = Character.Orc;
                playerOrc.SetActive(true);
                break;
            case "Mage":
                playerChoice = Character.Mage;
                playerMage.SetActive(true);
                break;
            case "Warrior":
                playerChoice = Character.Warrior;
                playerWarrior.SetActive(true);
                break;
        }

        ComputerChoice();
        DetermineWinner();
    }

    void ComputerChoice()
    {
        ResetComputerCharacters();
        computerChoice = (Character)Random.Range(0, 3);

        switch (computerChoice)
        {
            case Character.Orc:
                computerOrc.SetActive(true);
                break;
            case Character.Mage:
                computerMage.SetActive(true);
                break;
            case Character.Warrior:
                computerWarrior.SetActive(true);
                break;
        }
    }

    void DetermineWinner()
    {
        if (playerChoice == computerChoice)
        {
            resultText.text = "Нічия!";
        }
        else if (
            (playerChoice == Character.Mage && computerChoice == Character.Orc) ||
            (playerChoice == Character.Orc && computerChoice == Character.Warrior) ||
            (playerChoice == Character.Warrior && computerChoice == Character.Mage)
        )
        {
            resultText.text = "Ви виграли!";
        }
        else
        {
            resultText.text = "Ви програли.";
        }
    }

    void ResetCharacters()
    {
        playerOrc.SetActive(false);
        playerMage.SetActive(false);
        playerWarrior.SetActive(false);
    }

    void ResetComputerCharacters()
    {
        computerOrc.SetActive(false);
        computerMage.SetActive(false);
        computerWarrior.SetActive(false);
    }

    // Update — якщо потрібен у майбутньому
    void Update()
    {

    }
}
