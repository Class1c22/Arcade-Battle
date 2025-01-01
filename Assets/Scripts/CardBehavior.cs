using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    public CardType cardType; // “ип карти (Mage, Ogre, Dwarf)

    [SerializeField] // ÷ей атрибут дозвол€Ї в≥дображати приватне поле в Inspector
    private bool isPlayerCard; // „и це карта гравц€

    // ћетод дл€ ≥н≥ц≥ал≥зац≥њ карти
    public void Initialize(CardType type, bool playerCard)
    {
        cardType = type;         // ¬становлюЇмо тип карти
        isPlayerCard = playerCard; // ¬изначаЇмо, чи це карта гравц€
    }

    // ћетод, що викликаЇтьс€ при натисканн≥ на карту
    public void OnCardClicked()
    {
        if (isPlayerCard) // ѕерев≥р€Їмо, чи це карта гравц€
        {
            GameManager.Instance.PlayerPlayCard(this); // ѕередаЇмо карту в GameManager
        }
    }
}
