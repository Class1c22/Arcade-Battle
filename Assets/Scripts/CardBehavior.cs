using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    public CardType cardType; // ��� ����� (Mage, Ogre, Dwarf)
    private bool isPlayerCard; // �� �� ����� ������

    // ����� ��� ����������� �����
    public void Initialize(CardType type, bool playerCard)
    {
        cardType = type;
        isPlayerCard = playerCard;
    }

    // �����, �� ����������� ��� ��������� �� �����
    public void OnCardClicked()
    {
        Debug.Log("����� ���� ���������!"); // ������ ����������� � Console
        if (isPlayerCard)
        {
            GameManager.Instance.PlayerPlayCard(this);
        }
    }

}
