﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Grid
{
    public enum EGridElementState
    {
        Default,
        Occupied,
        Available,
        Unavailable
    }

    public class GridElement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _availableSprite;
        [SerializeField] private Sprite _unavailableSprite;
        [SerializeField] private Sprite _occupiedSprite;

        public MatrixMapCell Cell = null;

        private Sprite _defaultSprite;

        private Action<GridElement, bool> _onHover;
        private Action<GridElement> _onClick;

        private EGridElementState _currentState;

        public Tower PlantedTower { get; private set; }

        public void Init(Action<GridElement, bool> onHover, Action<GridElement> onPlant)
        {
            Cell.Object = this;

            _defaultSprite = _spriteRenderer.sprite;

            _onHover = onHover;
            _onClick = onPlant;
        }

        public void SetPlantedTower(Tower tower)
        {
            PlantedTower = tower;
        }

        private void OnMouseOver()
        {
            _onHover(this, true);
        }

        private void OnMouseExit()
        {
            _onHover(this, false);
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            _onClick(this);
        }

        public void SetAreaActive(EGridElementState state)
        {
            if (_currentState == state)
                return;

            _currentState = state;

            switch (state)
            {
                case EGridElementState.Default:
                    _spriteRenderer.sprite = _defaultSprite;
                    break;
                case EGridElementState.Available:
                    _spriteRenderer.sprite = _availableSprite;
                    break;
                case EGridElementState.Unavailable:
                    _spriteRenderer.sprite = _unavailableSprite;
                    break;
                case EGridElementState.Occupied:
                    _spriteRenderer.sprite = _occupiedSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state", state, null);
            }
        }
    }
}