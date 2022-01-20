using System.Collections.Generic;
using Code.Ball;
using Code.Timer;
using UnityEngine;

namespace Code.Arrow
{
    internal class ArrowColor
    {
        private readonly Renderer[] _renderers;
        private readonly Color[] _colorStart;
        private readonly Material[] _colorsNew;
        private ITimeRemaining _timeRemaining;
        private readonly float _arrowChangeColorSpeed;
        private int _arrowPart;

        public ArrowColor(Transform arrow, IForceModel speedModel,
            List<Material> colors)
        {
            _renderers = arrow.GetComponentsInChildren<Renderer>();
            _arrowChangeColorSpeed = speedModel.ColorRiseFactor / _renderers.Length;
            _colorStart = new Color[_renderers.Length];
            _colorsNew = colors.ToArray();

            for (int i = 0; i < _renderers.Length; i++)
            {
                _colorStart[i] = _renderers[i].material.color;
            }
        }

        public void RunningColorChanges()
        {
            _arrowPart = _renderers.Length - 1;
            AddTimer();
        }

        public void RemoveColorChanges()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color = _colorStart[i];
            }

            _timeRemaining.RemoveTimeRemaining();
        }

        private void ChangeColor()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                if (i >= _arrowPart)
                {
                    _renderers[i].material = _colorsNew[i];
                }
            }
        }

        private void AddTimer()
        {
            _timeRemaining = new TimeRemaining(ChangeColorInNextArrowPart, _arrowChangeColorSpeed, true);
            _timeRemaining.AddTimeRemaining();
        }

        private void ChangeColorInNextArrowPart()
        {
            if (_arrowPart > 0)
            {
                _arrowPart--;
                ChangeColor();
            }
            else
            {
                _timeRemaining.RemoveTimeRemaining();
            }
        }
    }
}
