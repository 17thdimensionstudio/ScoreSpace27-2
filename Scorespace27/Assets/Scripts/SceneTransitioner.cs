using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        DontDestroyOnLoad(transform.parent.gameObject);
        SceneManager.LoadScene(1);
    }

    public IEnumerator LoadScene(int id)
    {
        anim.SetBool("Loading", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(id + 1);
        anim.SetBool("Loading", false);
    }
}
