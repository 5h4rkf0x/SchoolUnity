using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class StateFieldManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> fieldLocations;
    [SerializeField] private PlusStateField plusStateField;
    [SerializeField] private MinusStateField minusStateField;

    [SerializeField] private Transform temp;


    private void Awake()
    {
        temp = GameObject.Find("Temp").transform;
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector3 tempFieldLocation = new Vector3(-12 + j * 6, 0.1f, 12 - i * 6);
                fieldLocations.Add(tempFieldLocation);
            }
        }
    }

    public IEnumerator UseStateFieldPattern()
    {
        foreach (Vector3 location in fieldLocations)
        {
            // 필드 생성
            int fieldStateNum = Random.Range(1, 201);
            if (fieldStateNum <= 100)
            {
                Instantiate(plusStateField, location, Quaternion.identity, temp);
            }
            else if (fieldStateNum > 100)
            {
                Instantiate(minusStateField, location, Quaternion.identity, temp);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}