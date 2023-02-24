using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] EventSystem m_eventSystem = default;
    GameObject mouse = default;
    private void Start()
    {
        GameManager.Instance.InstanseMap();
    }
    private void Update()
    {
        if(GameManager.Instance.Playing)
        {
            IsCruosCell();
        }
    }
    void IsCruosCell()
    {
        mouse = m_eventSystem.currentSelectedGameObject;
        if(mouse)
        {
            var cellInfo = mouse.GetComponent<DigButton>();
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R))
            {
                cellInfo.LandmineDetection();
                Debug.Log('A');
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.F))
            {
                cellInfo.RaiseFlag();
            }
        }
    }
}
