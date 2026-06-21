using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.Oculus.Input;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class StateFieldManager : MonoBehaviour
{
    [SerializeField] private BossPattern bossPattern;

    [SerializeField] private List<Vector3> fieldLocations;
    private Dictionary<Vector3, PlayerStates> stateInfo = new();
    [SerializeField] private PlusStateField plusStateField;
    [SerializeField] private MinusStateField minusStateField;
    [SerializeField] private GameObject plusStateWarning;
    [SerializeField] private GameObject minusStateWarning;

    [SerializeField] private List<PlusStateField> plusStateFieldList = new();
    [SerializeField] private List<GameObject> plusStateWarningList = new();

    [SerializeField] private List<MinusStateField> minusStateFieldList = new();
    [SerializeField] private List<GameObject> minusStateWarningList = new();

    [SerializeField] private Transform temp;


    private void Awake()
    {
        temp = GameObject.Find("StateField").transform;
        bossPattern = GetComponentInParent<BossPattern>();
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
        for (int i = 0; i < 25; i++)
        {
            PlusStateField tempPlusStateField = Instantiate(plusStateField, Vector3.zero, Quaternion.identity, temp);
            plusStateFieldList.Add(tempPlusStateField);
            plusStateFieldList[i].gameObject.SetActive(false);
            GameObject tempPlusStateWarning = Instantiate(plusStateWarning, Vector3.zero, Quaternion.identity, temp);
            plusStateWarningList.Add(tempPlusStateWarning);
            plusStateWarningList[i].gameObject.SetActive(false);

            MinusStateField tempMinusStateField = Instantiate(minusStateField, Vector3.zero, Quaternion.identity, temp);
            minusStateFieldList.Add(tempMinusStateField);
            minusStateFieldList[i].gameObject.SetActive(false);
            GameObject tempMinusStateWarning = Instantiate(minusStateWarning, Vector3.zero, Quaternion.identity, temp);
            minusStateWarningList.Add(tempMinusStateWarning);
            minusStateWarningList[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator UseStateFieldPattern()
    {
        if (bossPattern.isPatternActive) yield break;
        bossPattern.isPatternActive = true;
        int index = 0;

        foreach (Vector3 location in fieldLocations)
        {
            // 필드 예고 생성
            int fieldStateNum = Random.Range(1, 201);
            if (fieldStateNum <= 100)
            {
                plusStateWarningList[index].transform.position = location;
                plusStateWarningList[index].gameObject.SetActive(true);
                stateInfo[location] = PlayerStates.Plus;
            }
            else if (fieldStateNum > 100)
            {
                minusStateWarningList[index].transform.position = location;
                minusStateWarningList[index].gameObject.SetActive(true);
                stateInfo[location] = PlayerStates.Minus;
            }
            index++;
        }

        yield return new WaitForSeconds(1.5f);
        index = 0;

        for (int i = 0; i < 25; i++)
        {
            plusStateWarningList[i].gameObject.SetActive(false);
            minusStateWarningList[i].gameObject.SetActive(false);
        }

        // 필드 생성
        foreach (Vector3 location in fieldLocations)
        {
            if (!stateInfo.TryGetValue(location, out PlayerStates value)) continue;

            switch (value)
            {
                case PlayerStates.Plus:
                    plusStateFieldList[index].transform.position = location;
                    plusStateFieldList[index].gameObject.SetActive(true);
                    break;
                case PlayerStates.Minus:
                    minusStateFieldList[index].transform.position = location;
                    minusStateFieldList[index].gameObject.SetActive(true);
                    break;
            }
            index++;
        }

        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i < 25; i++)
        {
            plusStateFieldList[i].gameObject.SetActive(false);
            minusStateFieldList[i].gameObject.SetActive(false);
        }


        foreach (Vector3 location in fieldLocations)
        {
            stateInfo.Clear();
        }
        bossPattern.isPatternActive = false;
    }
}