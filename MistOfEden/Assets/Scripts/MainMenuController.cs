using Api;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField]
	SceneAsset  SingleScene;
	[SerializeField]
	SceneAsset  MultiScene;
	[SerializeField]
	GameObject MainMenu;
	[SerializeField]
	GameObject HeroSelectionMenu;
	[SerializeField]
	GameObject CreateHeroMenu;
	[SerializeField]
	GameObject HeroesPanel;
	[SerializeField]
	GameObject HeroButtonPrefab;

	public void Start()
	{
		ConfigurationManager.HeroAdded += OnHeroAdded;
	}

	public void OnSingle()
	{
		SceneManager.LoadScene(SingleScene.name);
	}

	public void OnMulti()
	{
		SceneManager.LoadScene(MultiScene.name);
	}

	public void OnCreateNewHero()
	{
		MainMenu.SetActive(false);
		HeroSelectionMenu.SetActive(false);
		CreateHeroMenu.SetActive(true);
	}

	void OnHeroAdded(Hero hero)
	{
		// TODO: BUG ???
		var gridLayoutGroup = HeroesPanel.GetComponent<GridLayoutGroup>();
		var heroButton = Instantiate(HeroButtonPrefab) as GameObject;
		heroButton.transform.SetParent(gridLayoutGroup.transform, false);
	}
}
