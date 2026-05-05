using UnityEngine;
using UnityEngine.U2D;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[ExecuteInEditMode]
public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _spriteShapeController;

    [SerializeField, Range(3f, 100f)] private int _levelLenght = 50;
    [SerializeField, Range(1f, 50f)] private float _xMultiplayer = 2f;
    [SerializeField, Range(1f, 50f)] private float _yMultiplayer = 2f;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;
    [SerializeField] private float _noiseStep = 0.5f;
    [SerializeField] private float _bottom = 10f;
                    
    private Vector3 _lastPos;

    private void OnValidate()
    {
        _spriteShapeController.spline.Clear();

        for (int i = 0; i < _levelLenght; i++)
        {
            _lastPos = transform.position + new Vector3(i * _xMultiplayer, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplayer);
            _spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != _levelLenght - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplayer * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplayer * _curveSmoothness);
            }
        }

        _spriteShapeController.spline.InsertPointAt(_levelLenght, new Vector3(_lastPos.x, transform.position.y - _bottom));

        _spriteShapeController.spline.InsertPointAt(_levelLenght + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
    }
}
