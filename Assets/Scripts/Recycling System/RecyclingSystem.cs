using System.Collections;
using UnityEngine;

namespace PolyAndCode.UI
{
    // Classe abstrata para criar um sistema de reciclagem.
    public abstract class RecyclingSystem
    {
        public IRecyclableScrollRectDataSource DataSource; // Fonte de dados para o Scroll Reciclável

        protected RectTransform Viewport, Content; // Referências para a viewport e o conteúdo
        protected RectTransform PrototypeCell; // Célula protótipo para a reciclagem
        protected bool IsGrid; // Indica se o sistema é uma grade

        protected float MinPoolCoverage = 1.5f; // A área coberta pela pool reciclável deve ser (viewport * _poolCoverage).
        protected int MinPoolSize = 10; // O tamanho mínimo da pool de células
        protected float RecyclingThreshold = .2f; // Limiar para reciclagem acima e abaixo da viewport

        // Método abstrato para inicializar a coroutine
        public abstract IEnumerator InitCoroutine(System.Action onInitialized = null);

        // Método abstrato para lidar com a mudança de valor
        public abstract Vector2 OnValueChangedListener(Vector2 direction);
    }
}
