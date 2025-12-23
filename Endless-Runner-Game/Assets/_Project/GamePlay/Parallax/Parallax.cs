using System;
using NUnit.Framework;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material mat;
    float distance;
    [UnityEngine.Range(0f,1f)]
    public float baseSpeed = .2f;
    public float _currentSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.CurrentState != GameState.inGame)return;
        distance += Time.deltaTime*_currentSpeed;
        mat.SetTextureOffset("_MainTex", Vector2.right*distance);
    }

}
