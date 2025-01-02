using UnityEngine;

public enum CardType
{
    Mage,  // ���
    Ogre,  // ���
    Dwarf  // ����
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    [Header("���� ��� ����")]
    public Transform PlayerField; // ���� ��� ����� ������ (�����)
    public Transform EnemyField;  // ���� ��� ����� ���������� (�����)

    [Header("����")]
    public Transform PlayerHand;  // ���� ������ (����� ������� ������)
    public Transform EnemyHand;   // ���� ���������� (������ ������� ������)

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
        playerCard.transform.SetParent(PlayerField, false); // false, ��� �������� �������� �������
        playerCard.transform.localPosition = Vector3.zero; // �������� ����� �� ���

        Debug.Log("����� ������ ��������� �� ����!");

        // ������ ���� ����������
        EnemyPlayCard();
    }


    // ����� ��� ���� ����������
    private void EnemyPlayCard()
    {
        // ���������� ���� ����� �����������
        int randomIndex = Random.Range(0, EnemyHand.childCount);
        Transform enemyCardTransform = EnemyHand.GetChild(randomIndex);
        CardBehavior enemyCard = enemyCardTransform.GetComponent<CardBehavior>();

        // ���������� ����� ���������� �� ����
        enemyCard.transform.SetParent(EnemyField);

        // ���������: ��� ����� ������ ����� ���������� ���������
    }
}
