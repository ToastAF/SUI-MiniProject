using UnityEngine;
using System.Collections;
using Mono.Cecil.Cil;

public class Respawn : MonoBehaviour
{
    public GameObject player, vignetteObject;
    public Transform spawnPos1, spawnPos2, spawnPos3;

    AudioSource huhSound;
    Material vignetteMat;
    Color vignetteCol;
    public float vignetteAlpha;

    public bool fadeToBlack = false;
    public float fadeSpeed = 2f;

    private void Start()
    {
        vignetteMat = vignetteObject.GetComponent<MeshRenderer>().materials[0];
        huhSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (fadeToBlack && vignetteAlpha < 1)
        {
            vignetteAlpha += Time.deltaTime * fadeSpeed;
        }
        else if(!fadeToBlack && vignetteAlpha > 0)
        {
            vignetteAlpha -= Time.deltaTime * fadeSpeed;
        }

        vignetteCol = new Color(0, 0, 0, vignetteAlpha);

        vignetteMat.color = vignetteCol;
    }

    public void MoveTo(Transform inputPos)
    {
        player.transform.position = inputPos.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("goo"))
        {
            StartCoroutine(FadeInOutAndRespawn(1.5f, spawnPos1));
        }

        if (other.gameObject.CompareTag("goo2"))
        {
            StartCoroutine(FadeInOutAndRespawn(1.5f, spawnPos2));
        }

        if (other.gameObject.CompareTag("goo3"))
        {
            StartCoroutine(FadeInOutAndRespawn(1.5f, spawnPos3));
        }
    }

    IEnumerator FadeInOutAndRespawn(float interval, Transform pos)
    {
        Debug.Log("You Died! Respawning...");

        //Fade to black
        fadeToBlack = true;
        yield return new WaitForSeconds(interval);
        //Teleport and fade back
        huhSound.Play();
        MoveTo(pos);
        fadeToBlack = false;

    }
}
