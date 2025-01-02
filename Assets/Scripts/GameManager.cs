using UnityEngine;
using UnityEngine.UI;
using TMPro;



public enum CardType
{
    Mage,
    Ogre,
    Dwarf
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Поля для карт")]
    public Transform playerField; // Поле для карти гравця
    public Transform enemyField;  // Поле для карти противника

    [Header("Руки")]
    public Transform playerHand;  // Рука гравця
    public Transform enemyHand;   // Рука противника

    [Header("Текст для очок")]
    public Text playerScoreText;  // Текст очок гравця
    public Text enemyScoreText;   // Текст очок противника

    [Header("Префаб карти")]
    public GameObject cardPrefab; // Префаб карти, який дублюється

    [Header("Спрайти карт")]
    public Sprite mageSprite;  // Спрайт для Мага
    public Sprite ogreSprite;  // Спрайт для Огру
    public Sprite dwarfSprite; // Спрайт для Гнома

    private int playerScore = 0;  // Очки гравця
    private int enemyScore = 0;   // Очки противника

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("GameManager ініціалізовано.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerPlayCard(CardBehavior playerCard)
    {
        Debug.Log("Гравець натиснув на карту!");

        // Створюємо копію карти гравця
        if (cardPrefab != null && playerField != null)
        {
            CreateCardCopy(playerCard.cardType, playerField);
            Debug.Log("Копія карти гравця створена!");
        }
        else
        {
            Debug.LogError("Префаб карти або PlayerField не прив'язаний!");
        }

        // Викликаємо хід противника
        EnemyPlayCard();
    }

    private void EnemyPlayCard()
    {
        Debug.Log("Противник виконує хід...");

        // Перевіряємо, чи є карти у руці противника
        if (enemyHand.childCount > 0)
        {
            int randomIndex = Random.Range(0, enemyHand.childCount);
            Transform enemyCardTransform = enemyHand.GetChild(randomIndex);
            CardBehavior enemyCard = enemyCardTransform.GetComponent<CardBehavior>();

            // Створюємо копію карти противника
            if (cardPrefab != null && enemyField != null)
            {
                CreateCardCopy(enemyCard.cardType, enemyField);
                Debug.Log($"Противник виклав карту: {enemyCard.cardType}");

                // Визначаємо переможця
                DetermineWinner(playerField.GetChild(0).GetComponent<CardBehavior>().cardType, enemyCard.cardType);
            }
            else
            {
                Debug.LogError("Префаб карти або EnemyField не прив'язаний!");
            }
        }
        else
        {
            Debug.LogError("У руці противника немає карт!");
        }
    }

    private void CreateCardCopy(CardType cardType, Transform field)
    {
        Debug.Log($"Створення копії карти: {cardType}");

        // Створюємо копію карти
        GameObject cardCopy = Instantiate(cardPrefab, field, false);

        // Ініціалізуємо її тип
        CardBehavior cardBehavior = cardCopy.GetComponent<CardBehavior>();
        if (cardBehavior != null)
        {
            cardBehavior.Initialize(cardType, false);

            // Оновлюємо спрайт залежно від типу карти
            UpdateCardSprite(cardCopy, cardType);
            Debug.Log($"Карта типу {cardType} створена в {field.name}.");
        }
        else
        {
            Debug.LogError("CardBehavior відсутній у префабі карти!");
        }
    }

    private void UpdateCardSprite(GameObject card, CardType cardType)
    {
        // Знаходимо компонент Image для UI
        var cardImage = card.GetComponentInChildren<UnityEngine.UI.Image>();
        if (cardImage == null)
        {
            Debug.LogError("Компонент Image не знайдено у карті!");
            return;
        }

        // Встановлюємо відповідний спрайт залежно від типу карти
        switch (cardType)
        {
            case CardType.Mage:
                cardImage.sprite = mageSprite;
                break;
            case CardType.Ogre:
                cardImage.sprite = ogreSprite;
                break;
            case CardType.Dwarf:
                cardImage.sprite = dwarfSprite;
                break;
            default:
                Debug.LogError("Невідомий тип карти!");
                break;
        }
    }

    private void DetermineWinner(CardType playerType, CardType enemyType)
    {
        Debug.Log($"Гравець виклав: {playerType}, Противник виклав: {enemyType}");

        if (playerType == enemyType)
        {
            Debug.Log("Нічия!");
        }
        else if ((playerType == CardType.Mage && enemyType == CardType.Ogre) ||
                 (playerType == CardType.Ogre && enemyType == CardType.Dwarf) ||
                 (playerType == CardType.Dwarf && enemyType == CardType.Mage))
        {
            Debug.Log("Гравець виграв раунд!");
            playerScore++;
        }
        else
        {
            Debug.Log("Противник виграв раунд!");
            enemyScore++;
        }

        UpdateScores();
        ClearField();
    }

    private void UpdateScores()
    {
        playerScoreText.text = $"Гравець: {playerScore}";
        enemyScoreText.text = $"Противник: {enemyScore}";
    }

    private void ClearField()
    {
        Debug.Log("Очищення полів...");

        foreach (Transform child in playerField)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in enemyField)
        {
            Destroy(child.gameObject);
        }
    }
}
