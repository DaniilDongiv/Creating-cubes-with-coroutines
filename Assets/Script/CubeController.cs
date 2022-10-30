using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private GameObject _cube;
    [SerializeField]
    private Transform _startPosition;
    [SerializeField]
    private float _creationTime;
    [SerializeField]
    private float _colorChangeIntervalCube;
    [SerializeField]
    private float _colorChangeTime;
    
    private int _cubesLine = 20;
    private int _cubes = 400;
    private List<GameObject> _allCubes = new List<GameObject>();
    private Color[] _colors = {Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow};
    
    void Start()
    {
        _startPosition = transform;
        StartCoroutine(CreateCubes());
    }

    public void ChangeColors()
    {
        StartCoroutine(ChangingColorCubes());
    }
    
    private IEnumerator CreateCubes()
    {
        var startPositionPosition = _startPosition.position;
        var quantityCubesLine = 0;
        
        for (int i = 0; i < _cubes; i++)
        {
            var cube = Instantiate(_cube);
            cube.transform.position = startPositionPosition;
            quantityCubesLine++;
            startPositionPosition.x += 0.35f;
            _allCubes.Add(cube);
            if (quantityCubesLine==_cubesLine)
            {
                startPositionPosition.z -= 0.35f;
                startPositionPosition.x = _startPosition.position.x;
                quantityCubesLine = 0;
            }
            
            yield return new WaitForSeconds(_creationTime);
        }
    }
    private IEnumerator ChangingColorCubes()
    {
        var quantityCubesLine = 0;
        
        var randomColor = new Random();
        var color = randomColor.Next(0, _colors.Length - 1);
        for (int i = 0; i < _cubes; i++)
        {
            
            var colorCube = _allCubes[i].GetComponent<Renderer>().material.color;
            quantityCubesLine++;
            float currentTime = 0f;
            
            do {
                _allCubes[i].GetComponent<Renderer>().material.color = Color.Lerp (colorCube, _colors[color], currentTime/_colorChangeTime);
                currentTime += Time.deltaTime;
                yield return null;
            } while (currentTime<=_colorChangeTime);
            
            
            if (quantityCubesLine==_cubesLine)
            {
                quantityCubesLine = 0;
            }
            yield return new WaitForSeconds(_colorChangeIntervalCube);
        }
    }
}
