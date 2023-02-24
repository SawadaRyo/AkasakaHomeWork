using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigButton : MonoBehaviour
{
    [SerializeField] Image m_thisButton = default;
    [SerializeField] Image m_bombImage = default;
    [SerializeField] Image m_flgImage = default;
    [SerializeField] Text m_thisText = default;

    bool m_mineCell = false;
    bool m_isActive = true;
    bool m_setFlag = false;
    int m_thisH = 0;
    int m_thisV = 0;

    public int ThisH { get => m_thisH; set => m_thisH = value; }
    public int ThisV { get => m_thisV; set => m_thisV = value; }
    public bool MineCell { get => m_mineCell; set => m_mineCell = value; }
    public bool IsActive => m_isActive;
    public Image BombImage { get => m_bombImage; set => m_bombImage = value; }
    public Image FlgImage { get => m_flgImage; set => m_flgImage = value; }

    public void SetUp()
    {
        m_thisText.enabled = false;
        m_bombImage.enabled = false;
        m_flgImage.enabled = false;
    }
    public void LandmineDetection()
    {
        if (m_isActive && !m_setFlag && GameManager.Instance.Playing)　//まだ押されておらず地雷旗が立っていない場合以下の処理を行う
        {
            if (!m_mineCell)　//通常ボタン
            {
                m_isActive = false;
                m_thisButton.color -= new Color(0f, 0f, 0f, 0.5f);
                var valueOfMine = GameManager.Instance.SeachMine(m_thisV, m_thisH);
                if(valueOfMine != 0)
                {
                    m_thisText.enabled = true;
                    m_thisText.text = string.Format("{0}", valueOfMine);
                }
                
                GameManager.Instance.DigCounter++;
            }
            else　//地雷ボタン
            {
                if(GameManager.Instance.DigCounter == 0)//初手で地雷を引いてしまった場合再度抽選する
                {
                    m_mineCell = false;
                    GameManager.Instance.SetMine(ThisV, m_thisH);
                    LandmineDetection();
                }
                else
                {
                    GameManager.Instance.GameOver();
                }
            }
        }
    }

    public void RaiseFlag()
    {
        m_setFlag = !m_setFlag;
        m_flgImage.enabled = m_setFlag;
    }

}
