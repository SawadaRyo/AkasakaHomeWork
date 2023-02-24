using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] int _summarySize = 10;
    [SerializeField] DigButton _cellsPrefab = default;
    [SerializeField] Transform _panelObject = default;
    [SerializeField,Range(0.01f,0.1f)] float _mineMagnification = 0.1f;

    bool _playing = false;
    int _digCounter = 0;
    DigButton[,] _cellDigButton = default;
    DigButton[] _mineButton = default;

    public int DigCounter { get => _digCounter; set => _digCounter = value; }
    public bool Playing => _playing;

    public void InstanseMap()
    {
        if (_summarySize > 20)
        {
            Debug.LogError("‘½‚·‚¬‚¶‚á");
            return;
        }
        if (_summarySize < 7)
        {
            Debug.LogError("­‚È‚·‚¬‚¶‚á");
            return;
        }

        _cellDigButton = new DigButton[_summarySize, _summarySize];
        var mineCounter = _summarySize *_summarySize * _mineMagnification;
        _mineButton = new DigButton[(int)mineCounter];
        _panelObject.GetComponent<GridLayoutGroup>().constraintCount = _summarySize;
        for (int i = 0; i < _summarySize; i++)
        {
            for (int j = 0; j < _summarySize; j++)
            {
                var cellobj = Instantiate(_cellsPrefab, _panelObject.position, Quaternion.identity);
                cellobj.transform.SetParent(_panelObject);
                _cellDigButton[i, j] = cellobj.GetComponent<DigButton>();
                _cellDigButton[i, j].SetUp();
                _cellDigButton[i, j].ThisV = i;
                _cellDigButton[i, j].ThisH = j;
            }
        }

        for (int k = 0; k < (int)mineCounter; k++)
        {
            SetMine(k);
        }
        _cellDigButton[_summarySize / 2, _summarySize / 2].GetComponent<Button>().Select();
        _playing = true;
    }
    public void SetMine(int mineNum)
    {
        int h = UnityEngine.Random.Range(0, _summarySize - 1);
        int v = UnityEngine.Random.Range(0, _summarySize - 1);
        var mine = _cellDigButton[v, h];

        if (SeachMine(mine.ThisV, mine.ThisH) == 0)
        {
            mine.MineCell = true;
            _mineButton[mineNum] = mine;
            return;
        }
        else
        {
            SetMine(mineNum);
        }
    }
    public void SetMine(int v,int h)
    {
        int h2 = UnityEngine.Random.Range(0, _summarySize - 1);
        int v2 = UnityEngine.Random.Range(0, _summarySize - 1);
        var mine = _cellDigButton[v2, h2];

        if (SeachMine(mine.ThisV, mine.ThisH) == 0 && mine != _cellDigButton[v,h])
        {
            mine.MineCell = true;
            int index = Array.IndexOf(_mineButton, _cellDigButton[v,h]);
            Debug.Log(index);
            _mineButton[index] = mine;
            return;
        }
        else
        {
            SetMine(v,h);
        }
    }
    public int SeachMine(int v, int h)
    {
        var mineQuantity = 0;
        for (int i = v - 1; i <= v + 1; i++)
        {
            for (int j = h - 1; j <= h + 1; j++)
            {
                if (i < 0 || i >= _summarySize ||
                    j < 0 || j >= _summarySize)
                {
                    continue;
                }

                var cells = _cellDigButton[i, j];
                if (cells.MineCell)
                {
                    mineQuantity++;
                }
            }
        }

        if (mineQuantity == 0 && _playing)
        {
            ExprotionCell(v, h);
        }
        return mineQuantity;
    }
    public void ExprotionCell(int v, int h)
    {
        for (int i = v - 1; i <= v + 1; i++)
        {
            for (int j = h - 1; j <= h + 1; j++)
            {
                if (i < 0 || i >= _summarySize ||
                    j < 0 || j >= _summarySize ||
                    !_cellDigButton[i, j].IsActive)
                {
                    continue;
                }
                var cells = _cellDigButton[i, j];
                cells.LandmineDetection();
            }
        }
    }
    public void GameOver()
    {
        foreach (var allMineCell in _mineButton)
        {
            if(allMineCell.FlgImage.enabled)
            {
                allMineCell.FlgImage.enabled = false;
            }
            allMineCell.BombImage.enabled = true;
        }
        _playing = false;
    }
}
