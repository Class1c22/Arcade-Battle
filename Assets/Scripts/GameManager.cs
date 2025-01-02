using UnityEngine;
using TMPro;  // ����� UnityEngine.UI �� TextMeshPro
using System.Collections;

public enum CardType
{
    Mage,
    Ogre,
    Dwarf
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("���� ��� ����")]
    public Transform playerField; // ���� ��� ����� ������
    public Transform enemyField;  // ���� ��� ����� ����������

    [Header("����")]
    public Transform playerHand;  // ���� ������
    public Transform enemyHand;   // ���� ����������

    [Header("����� ��� ����")]
    public TMP_Text playerScoreText;  // ����� ���� ������ (������� �� TMP_Text)
    public TMP_Text enemyScoreText;   // ����� ���� ���������� (������� �� TMP_Text)

    [Header("������ �����")]
    public GameObject cardPrefab; // ������ �����, ���� ����������

    [Header("������� ����")]
    public Sprite mageSprite;  // ������ ��� ����
    public Sprite ogreSprite;  // ������ ��� ����
    public Sprite dwarfSprite; // ������ ��� �����

    private int playerScore = 0;  // ���� ������
    private int enemyScore = 0;   // ���� ����������

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("GameManager ������������.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerPlayCard(CardBehavior playerCard)
    {
        Debug.Log("������� �������� �� �����!");

        // ��������� ���� ����� ������
        if (cardPrefab != null && playerField != null)
        {
            CreateCardCopy(playerCard.cardType, playerField);
            Debug.Log("���� ����� ������ ��������!");
        }
        else
        {
            Debug.LogError("������ ����� ��� PlayerField �� ����'������!");
        }

        // ��������� ��� ����������
        EnemyPlayCard();
    }

    private void EnemyPlayCard()
    {
        Debug.Log("��������� ������ ���...");

        // ����������, �� � ����� � ���� ����������
        if (enemyHand.childCount > 0)
        {
            int randomIndex = Random.Range(0, enemyHand.childCount);
            Transform enemyCardTransform = enemyHand.GetChild(randomIndex);
            CardBehavior enemyCard = enemyCardTransform.GetComponent<CardBehavior>();

            // ��������� ���� ����� ����������
            if (cardPrefab != null && enemyField != null)
            {
                CreateCardCopy(enemyCard.cardType, enemyField);
                Debug.Log($"��������� ������ �����: {enemyCard.cardType}");

                // ��������� ��������� ���� ����, �� ������ ������ ������� �����
                DetermineWinner(playerField.GetChild(0).GetComponent<CardBehavior>().cardType, enemyCard.cardType);

                // ��������� �������� ���� �� ������� ���������� ������
                StartCoroutine(ClearFieldsAfterDelay(1f)); // �������� � 1 ������� (����� �����������)
            }
            else
            {
                Debug.LogError("������ ����� ��� EnemyField �� ����'������!");
            }
        }
        else
        {
            Debug.LogError("� ���� ���������� ���� ����!");
        }
    }

    private void CreateCardCopy(CardType cardType, Transform field)
    {
        Debug.Log($"��������� ��ﳿ �����: {cardType}");

        // ��������� ���� �����
        GameObject cardCopy = Instantiate(cardPrefab, field, false);

        // ���������� �� ���
        CardBehavior cardBehavior = cardCopy.GetComponent<CardBehavior>();
        if (cardBehavior != null)
        {
            cardBehavior.Initialize(cardType, false);

            // ��������� ������ ������� �� ���� �����
            UpdateCardSprite(cardCopy, cardType);
            Debug.Log($"����� ���� {cardType} �������� � {field.name}.");
        }
        else
        {
            Debug.LogError("CardBehavior ������� � ������ �����!");
        }
    }

    private void UpdateCardSprite(GameObject card, CardType cardType)
    {
        // ��������� ��������� Image ��� UI
        var cardImage = card.GetComponentInChildren<UnityEngine.UI.Image>();
        if (cardImage == null)
        {
            Debug.LogError("��������� Image �� �������� � ����!");
            return;
        }

        // ������������ ��������� ������ ������� �� ���� �����
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
                Debug.LogError("�������� ��� �����!");
                break;
        }
    }

    private void DetermineWinner(CardType playerType, CardType enemyType)
    {
        Debug.Log($"������� ������: {playerType}, ��������� ������: {enemyType}");

        if (playerType == enemyType)
        {
            Debug.Log("ͳ���!");
        }
        else if ((playerType == CardType.Mage && enemyType == CardType.Ogre) ||
                 (playerType == CardType.Ogre && enemyType == CardType.Dwarf) ||
                 (playerType == CardType.Dwarf && enemyType == CardType.Mage))
        {
            Debug.Log("������� ������ �����!");
            playerScore++;
        }
        else
        {
            Debug.Log("��������� ������ �����!");
            enemyScore++;
        }

        UpdateScores();
    }

    private void UpdateScores()
    {
        playerScoreText.text = playerScore.ToString();  // ������������ ���� �� �����
        enemyScoreText.text = enemyScore.ToString();    // ������������ ���� �� �����
    }

    private IEnumerator ClearFieldsAfterDelay(float delay)
    {
        // �������� ����� ���������
        yield return new WaitForSeconds(delay);

        Debug.Log("�������� ����...");

        // ������� ��� ����� ���������� ����� ����
        RemoveAllCardsFromField(playerField);
        RemoveAllCardsFromField(enemyField);
    }

    private void RemoveAllCardsFromField(Transform field)
    {
        // ��������� �� ����� � ����
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        Debug.Log($"���� {field.name} �������.");
    }
}
