using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Delay : MonoBehaviour
{
    public float time = 5f;
    public Animator transition;
    public float TransitionTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        transition.SetTrigger("Start");
    }
}
