using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static float Money;

    public GameObject Heart;
    public IceHeart heartScript;

    public Text healthText;
    public Text moneyText;

    public Text waveText;

    public Text towerInfoText;

    public Tower[] towerList;
    public int currentTower;

    public bool towerSelected;

    // Start is called before the first frame update
    void Start()
    {
        heartScript = Heart.GetComponent<IceHeart>();
        Reset();
    }

    private void Reset()
    {
        Money = 350;
        DeselectTower();
    }
    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + Money.ToString();
        healthText.text = "Lives: " + IceHeart.Health.ToString();
        waveText.text = "Wave: " + Spawner.CurrentWave.ToString() + "/10";
    }

    public Tower GetTower()
    {
        if (towerSelected)
            return towerList[currentTower];

        return null;
    }

    public bool TowerActiveToClick()
    {
        if (GetTower() != null)
        {
            if (Money >= GetTower().BaseCost)
                return towerSelected;
        }

        return false;
    }

    public void SelectBasic()
    {
        currentTower = 0;
        towerSelected = true;
        SetTowerText(GetTower());
    }

    public void SelectArea()
    {
        currentTower = 1;
        towerSelected = true;
        SetTowerText(GetTower());
    }

    public void DeselectTower()
    {
        currentTower = 0;
        towerSelected = false;
        towerInfoText.text = "";
    }

    void SetTowerText(Tower t)
    {
        //Text
        towerInfoText.text = 
            "Cost:\n" + 
            t.BaseCost.ToString() + 
            "\nDamage:\n" + 
            t.Power.ToString() + 
            "\nRange:\n" + 
            t.Range.ToString();
        //End text
    }

}
