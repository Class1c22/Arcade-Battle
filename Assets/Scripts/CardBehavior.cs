using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    public CardType cardType; // ��� ����� (Mage, Ogre, Dwarf)

    [SerializeField] // ��� ������� �������� ���������� �������� ���� � Inspector
    private bool isPlayerCard; // �� �� ����� ������

    // ����� ��� ����������� �����
    public void Initialize(CardType type, bool playerCard)
    {
        cardType = type;         // ������������ ��� �����
        isPlayerCard = playerCard; // ���������, �� �� ����� ������
    }

    // �����, �� ����������� ��� ��������� �� �����
    public void OnCardClicked()
    {
        if (isPlayerCard) // ����������, �� �� ����� ������
        {
            GameManager.Instance.PlayerPlayCard(this); // �������� ����� � GameManager
        }
    }
}
