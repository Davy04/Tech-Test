using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;

// Classe DemoCell que herda de ICell. Necessária para configurar a célula durante a reciclagem.
public class DemoCell : MonoBehaviour, ICell
{
    // Elementos da interface do usuário
    public Text nameLabel;
    public Text genderLabel;
    public Text idLabel;

    // Modelo
    private ContactInfo _contactInfo; // Informações de contato
    private int _cellIndex; // Índice da célula

    private void Start()
    {
        // Adiciona um ouvinte ao botão (também pode ser feito no inspetor)
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    // Método chamado pelo SetCell na fonte de dados
    public void ConfigureCell(ContactInfo contactInfo, int cellIndex)
    {
        _cellIndex = cellIndex; // Define o índice da célula
        _contactInfo = contactInfo; // Define as informações de contato

        // Atualiza os elementos da interface do usuário com as informações de contato
        nameLabel.text = contactInfo.Name;
        genderLabel.text = contactInfo.Teams;
        idLabel.text = contactInfo.id;
    }

    // Método chamado quando o botão é clicado
    private void ButtonListener()
    {
        // Exibe no console o índice, nome e equipe do contato
        Debug.Log("Index : " + _cellIndex + ", Nome : " + _contactInfo.Name + ", Time : " + _contactInfo.Teams);
    }
}
