using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;

// Estrutura para armazenar informações de contato
public struct ContactInfo
{
    public string Name; 
    public string Teams; 
    public string id; 
}

// Classe principal para demonstrar o uso de um Scroll Reciclável
public class RecyclableScrollerDemo : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect; // Referência para o Scroll Reciclável

    [SerializeField]
    private int _dataLength; // Comprimento dos dados

    private List<ContactInfo> _contactList = new List<ContactInfo>(); // Lista para armazenar informações de contato

    private void Awake()
    {
        InitData(); // Inicializa os dados
        _recyclableScrollRect.DataSource = this; // Define esta classe como a fonte de dados para o Scroll Reciclável
    }

    private void InitData()
    {
        _contactList.Clear(); // Limpa a lista de contatos

        string[] teams = { "Santa Cruz", "Náutico", "Sport" }; // Array de equipes
        for (int i = 0; i < _dataLength; i++)
        {
            ContactInfo obj = new ContactInfo(); // Cria um novo objeto ContactInfo
            obj.Name = i + "_Name"; // Define o nome do contato
            obj.Teams = teams[Random.Range(0, 3)]; // Define a equipe do contato de forma aleatória
            obj.id = "item : " + i; // Define o ID do contato
            _contactList.Add(obj); // Adiciona o contato à lista
        }
    }

    #region DATA-SOURCE

    public int GetItemCount()
    {
        return int.MaxValue; // Retorna o número máximo de itens
    }

    public void SetCell(ICell cell, int index)
    {
        int actualIndex = index % _contactList.Count; // Calcula o índice atual
        var item = cell as DemoCell; // Converte a célula para o tipo DemoCell
        item.ConfigureCell(_contactList[actualIndex], actualIndex); // Configura a célula com as informações do contato
    }

    #endregion
}
