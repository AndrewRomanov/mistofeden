using System.Collections;
using System.Collections.Generic;
using Api;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateHero : MonoBehaviour
{
    [SerializeField]
    GameObject WarriorImage;
    [SerializeField]
    GameObject MageImage;
    [SerializeField]
    GameObject PaladinImage;
    [SerializeField]
    GameObject CurrentMenu;
    [SerializeField]
    GameObject BackMenu;

    static HeroType _selectedHeroType;
    public HeroType SelectedHeroType
    {
        get
        {
            return _selectedHeroType;
        }
        private set
        {
            _selectedHeroType = value;
            WarriorImage.SetActive(false);
            MageImage.SetActive(false);
            PaladinImage.SetActive(false);
            switch (value)
            {
                case HeroType.Warrior:
                    WarriorImage.SetActive(true);
                    break;
                case HeroType.Mage:
                    MageImage.SetActive(true);
                    break;
                case HeroType.Paladin:
                    PaladinImage.SetActive(true);
                    break;
            }
        }
    }

    public void OnWarriorClick() => SelectedHeroType = HeroType.Warrior;
    public void OnMageClick() => SelectedHeroType = HeroType.Mage;
    public void OnPaladinClick() => SelectedHeroType = HeroType.Paladin;
    public void OnCreate()
    {
        ConfigurationManager.AddHero(new Hero { HeroType = SelectedHeroType });
        CurrentMenu.SetActive(false);
        BackMenu.SetActive(true);
	}
}