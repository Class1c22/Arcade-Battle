using UnityEngine;
using TMPro; // Додаємо для TextMeshPro

public enum CardType // Визначаємо типи карт
{
    Mage,  // Маг
    Ogre,  // Огр
    Dwarf  // Гном
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton для легкого доступу до GameManager

    [Header("Поля для карт")]
    public Transform playerField; // Поле, куди гравець викладає карти
    public Transform enemyField;  // Поле, куди противник викладає карти

    [Header("Очки")]
    public TextMeshProUGUI playerScoreText; // Текст для очок гравця (TextMeshProUGUI)
    public TextMeshProUGUI enemyScoreText;  // Текст для очок противника (TextMeshProUGUI)
    public TextMeshProUGUI gameResultText; // Текст для результату гри

    private int playerScore = 0; // Очки гравця
    private int enemyScore = 0;  // Очки противника

    private void Awake()
    {
        // Ініціалізація Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Метод для обробки викладення карти гравцем
    public void PlayerPlayCard(CardBehavior playerCard)
    {
        // Перемістити карту гравця на поле
        playerCard.transform.SetParent(playerField);

        // Зробити хід противника
        EnemyPlayCard(playerCard.cardType);
    }

    // Метод для ходу противника
    private void EnemyPlayCard(CardType playerCardType)
    {
        // Випадковий вибір карти противником
        int randomIndex = Random.Range(0, enemyField.childCount);
        Transform enemyCardTransform = enemyField.GetChild(randomIndex);
        CardBehavior enemyCard = enemyCardTransform.GetComponent<CardBehavior>();

        // Перемістити карту противника на поле
        enemyCard.transform.SetParent(enemyField);

        // Визначити переможця
        DetermineWinner(playerCardType, enemyCard.cardType);
    }

    // Логіка визначення переможця
    private void DetermineWinner(CardType playerType, CardType enemyType)
    {
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

        // Оновити текст очок
        UpdateScore();

        // Перевірка на перемогу
        if (playerScore >= 5)
        {
            gameResultText.text = "Гравець виграв гру!";
            EndGame();
        }
        else if (enemyScore >= 5)
        {
            gameResultText.text = "Противник виграв гру!";
            EndGame();
        }
    }

    // Метод для оновлення тексту очок
    private void UpdateScore()
    {
        playerScoreText.text = "Гравець: " + playerScore;
        enemyScoreText.text = "Противник: " + enemyScore;
    }

    // Метод для завершення гри
    private void EndGame()
    {
        // Логіка завершення гри, можливо скидання очок або показ екрану результату
        Debug.Log("Гра завершена!");
    }
}
