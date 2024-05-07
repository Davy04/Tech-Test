using System;
using UnityEngine;
using UnityEngine.UI;

namespace PolyAndCode.UI
{
    public class RecyclableScrollRect : ScrollRect
    {
        [HideInInspector]
        public IRecyclableScrollRectDataSource DataSource;

        public bool IsGrid;
        public RectTransform PrototypeCell;
        public bool SelfInitialize = true;

        public enum DirectionType
        {
            Vertical,
            Horizontal
        }

        public DirectionType Direction;

        public int Segments
        {
            set
            {
                _segments = Math.Max(value, 2);
            }
            get
            {
                return _segments;
            }
        }
        [SerializeField]
        private int _segments;

        private RecyclingSystem _recyclingSystem;
        private Vector2 _prevAnchoredPos;

        protected override void Start()
        {
            vertical = true;
            horizontal = false;

            if (!Application.isPlaying)
                return;

            if (SelfInitialize)
                Initialize();
        }

        private void Initialize()
        {
            if (Direction == DirectionType.Vertical)
            {
                _recyclingSystem = new VerticalRecyclingSystem(PrototypeCell, viewport, content, DataSource, IsGrid, Segments);
            }

            vertical = Direction == DirectionType.Vertical;
            horizontal = Direction == DirectionType.Horizontal;

            _prevAnchoredPos = content.anchoredPosition;
            onValueChanged.RemoveListener(OnValueChangedListener);
            StartCoroutine(_recyclingSystem.InitCoroutine(() =>
            onValueChanged.AddListener(OnValueChangedListener)
            ));
        }

        public void Initialize(IRecyclableScrollRectDataSource dataSource)
        {
            DataSource = dataSource;
            Initialize();
        }

        public void OnValueChangedListener(Vector2 normalizedPos)
        {
            Vector2 dir = content.anchoredPosition - _prevAnchoredPos;
            m_ContentStartPosition += _recyclingSystem.OnValueChangedListener(dir);
            _prevAnchoredPos = content.anchoredPosition;
        }

        public void ReloadData()
        {
            ReloadData(DataSource);
        }

        public void ReloadData(IRecyclableScrollRectDataSource dataSource)
        {
            if (_recyclingSystem != null)
            {
                StopMovement();
                onValueChanged.RemoveListener(OnValueChangedListener);
                _recyclingSystem.DataSource = dataSource;
                StartCoroutine(_recyclingSystem.InitCoroutine(() =>
                onValueChanged.AddListener(OnValueChangedListener)
                ));
                _prevAnchoredPos = content.anchoredPosition;
            }
        }
    }
}
