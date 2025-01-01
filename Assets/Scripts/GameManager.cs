using UnityEngine;
using TMPro; // ������ ��� TextMeshPro

public enum CardType // ��������� ���� ����
{
    Mage,  // ���
    Ogre,  // ���
    Dwarf  // ����
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton ��� ������� ������� �� GameManager

    [Header("���� ��� ����")]
    public Transform playerField; // ����, ���� ������� ������� �����
    public Transform enemyField;  // ����, ���� ��������� ������� �����

    [Header("����")]
    public TextMeshProUGUI playerScoreText; // ����� ��� ���� ������ (TextMeshProUGUI)
    public TextMeshProUGUI enemyScoreText;  // ����� ��� ���� ���������� (TextMeshProUGUI)
    public TextMeshProUGUI gameResultText; // ����� ��� ���������� ���

    private int playerScore = 0; // ���� ������
    private int enemyScore = 0;  // ���� ����������

    private void Awake()
    {
        // ����������� Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ����� ��� ������� ���������� ����� �������
    public void PlayerPlayCard(CardBehavior playerCard)
    {
        // ���������� ����� ������ �� ����
        playerCard.transform.SetParent(playerField);

        // ������� ��� ����������
        EnemyPlayCard(playerCard.cardType);
    }

    // ����� ��� ���� ����������
    private void EnemyPlayCard(CardType playerCardType)
    {
        // ���������� ���� ����� �����������
        int randomIndex = Random.Range(0, enemyField.childCount);
        Transform enemyCardTransform = enemyField.GetChild(randomIndex);
        CardBehavior enemyCard = enemyCardTransform.GetComponent<CardBehavior>();

        // ���������� ����� ���������� �� ����
        enemyCard.transform.SetParent(enemyField);

        // ��������� ���������
        DetermineWinner(playerCardType, enemyCard.cardType);
    }

    // ����� ���������� ���������
    private void DetermineWinner(CardType playerType, CardType enemyType)
    {
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

        // ������� ����� ����
        UpdateScore();

        // �������� �� ��������
        if (playerScore >= 5)
        {
            gameResultText.text = "������� ������ ���!";
            EndGame();
        }
        else if (enemyScore >= 5)
        {
            gameResultText.text = "��������� ������ ���!";
            EndGame();
        }
    }

    // ����� ��� ��������� ������ ����
    private void UpdateScore()
    {
        playerScoreText.text = "�������: " + playerScore;
        enemyScoreText.text = "���������: " + enemyScore;
    }

    // ����� ��� ���������� ���
    private void EndGame()
    {
        // ����� ���������� ���, ������� �������� ���� ��� ����� ������ ����������
        Debug.Log("��� ���������!");
    }
}
