using UnityEngine;

public enum CardType
{
    Mage,  // Маг
    Ogre,  // Огр
    Dwarf  // Гном
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    [Header("Поля для карт")]
    public Transform PlayerField; // Поле для карти гравця (центр)
    public Transform EnemyField;  // Поле для карти противника (центр)

    [Header("Руки")]
    public Transform PlayerHand;  // Рука гравця (нижня частина екрану)
    public Transform EnemyHand;   // Рука противника (верхня частина екрану)

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
        // Переміщення карти гравця на поле
        playerCard.transform.SetParent(PlayerField, false); // false, щоб зберегти локальну позицію
        playerCard.transform.localPosition = Vector3.zero; // Центруємо карту на полі

        Debug.Log("Карта гравця переміщена на поле!");

        // Виклик ходу противника
        EnemyPlayCard();
    }


    // Метод для ходу противника
    private void EnemyPlayCard()
    {
        // Випадковий вибір карти противником
        int randomIndex = Random.Range(0, EnemyHand.childCount);
        Transform enemyCardTransform = EnemyHand.GetChild(randomIndex);
        CardBehavior enemyCard = enemyCardTransform.GetComponent<CardBehavior>();

        // Перемістити карту противника на поле
        enemyCard.transform.SetParent(EnemyField);

        // Додатково: тут можна додати логіку визначення переможця
    }
}
