﻿using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.Shader
{
    public class DistortionControl : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField, Range(0.01f, 2.0f)] private float _speed;
        private const string PROPERTY_KEY = "_PointOffset";
        private Vector2 _offset;

        private void Update()
        {
            if(_material == null)
                return;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _offset.y += Time.deltaTime * _speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _offset.y -= Time.deltaTime * _speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _offset.x -= Time.deltaTime * _speed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _offset.x += Time.deltaTime * _speed;
            }
            
            _material.SetVector(PROPERTY_KEY, _offset);
        }
    }
}