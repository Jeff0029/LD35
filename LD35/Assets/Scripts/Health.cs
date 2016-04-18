using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class Health : MonoBehaviour {

    float hp = 100;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            if (value < hp)
            {
                StopAllCoroutines();
                hp = value;
                StartCoroutine(RecoverHP());
            } else {
                hp = value;
            }

            if (value <= 0)
            {
                StopAllCoroutines();
                hp = 0;
                Death();
            }
        }

    }
    public float RecoveryPerFrame = 1;
    public float timeBeforeRecovery = 3;
    float startingHP;
    public GameObject tint;
    public GameObject fadeToBlack;
    public AudioClip death;

	// Use this for initialization
	void Start () {
        startingHP = hp;
    }
	
	// Update is called once per frame
	void Update () {
        Color curColor = tint.GetComponent<Renderer>().material.GetColor("_TintColor");
        curColor = new Color(curColor.r, curColor.g, curColor.b, 1 - (hp / startingHP));
        tint.GetComponent<Renderer>().material.SetColor("_TintColor", curColor);

    }

    IEnumerator RecoverHP()
    {
        float tParam = 0;
        float startPoint = hp;
        yield return new WaitForSeconds(timeBeforeRecovery);
        while (hp < startingHP)
        {
            yield return null;
            tParam += RecoveryPerFrame * Time.deltaTime;
            hp = Mathf.Lerp(startPoint, startingHP, tParam);
        }
    }

    void Death()
    {
        GetComponent<FirstPersonController>().enabled = false;
        GetComponent<AudioSource>().clip = death;
        GetComponent<AudioSource>().Play();
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        float tParam = 0;
        Color curColor = fadeToBlack.GetComponent<Renderer>().material.GetColor("_Color");
        Color startingCol = curColor;
        yield return new WaitForSeconds(0.7f);
        while (tParam < 1)
        {
            
            tParam += RecoveryPerFrame * Time.deltaTime;

            curColor = Color.Lerp(startingCol, new Color(0,0,0,1), tParam);
            fadeToBlack.GetComponent<Renderer>().material.SetColor("_Color", curColor);

            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
