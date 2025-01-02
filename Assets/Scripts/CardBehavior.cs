using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    public CardType cardType; // Тип карти (Mage, Ogre, Dwarf)
    private bool isPlayerCard; // Чи це карта гравця

    // Метод для ініціалізації карти
    public void Initialize(CardType type, bool playerCard)
    {
        cardType = type;
        isPlayerCard = playerCard;
    }

    // Метод, що викликається при натисканні на карту
    public void OnCardClicked()
    {
        Debug.Log("Карта була натиснута!"); // Виведе повідомлення в Console
        if (isPlayerCard)
        {
            GameManager.Instance.PlayerPlayCard(this);
        }
    }

}
