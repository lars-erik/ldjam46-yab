using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject Halo;

    // Start is called before the first frame update
    void Start()
    {
        var haloMat = Halo.GetComponent<Renderer>().material;
        haloMat.SetColor("_Color", new Color(1, 1, 0.616f, 1));
        haloMat.SetColor("_EmissionColor", new Color(0.74f, 0.55f, 0, .5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
