using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject menuGameObject, optionsGameObject;
    public AudioSource click;

    public void TutorialPlay()
    {
        click.Play();
        SceneManager.LoadScene("DialogueScene");
    }

    public void QuickPlay()
    {
        click.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void Options()
    {
        menuGameObject.SetActive(false);
        optionsGameObject.SetActive(true);
        click.Play();
    }

    public void Done()
    {
        menuGameObject.SetActive(true);
        optionsGameObject.SetActive(false);
        click.Play();
    }
}
