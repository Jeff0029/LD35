using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    public AudioClip finish;
    bool bFinish = false;
    AudioSource source;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {          
            source.clip = finish;
            source.Play();
            bFinish = true;
            StartCoroutine(WaitForMusic());
        }

    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    IEnumerator WaitForMusic()
    {
        yield return new WaitForSeconds(source.clip.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
