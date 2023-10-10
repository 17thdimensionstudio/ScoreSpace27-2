using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    [SerializeField] int immunityTicks;
    [SerializeField] LeaderboardScript leaderboard;
    private Transform mainTransform;
    private Color color;
    private int tiksTilNotImune;

    private void Start()
    {
        tiksTilNotImune = immunityTicks;
        mainTransform = transform.parent.transform;
        color = gameObject.GetComponent<SpriteRenderer>().color;
    }
    private void FixedUpdate()
    {
        if (tiksTilNotImune > 0)
        {
            tiksTilNotImune--;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && tiksTilNotImune <= 0)
        {
            health--;
            StartCoroutine(ColorShift(.005f, 40, 20));
            StartCoroutine(ScreenShake(.1f, .1f, .1f, 3));
            tiksTilNotImune = immunityTicks;
        }
        if (health == 0)
        {
            FindObjectOfType<AutoScrollScript>().IncreaseMultiplier();
            Destroy(transform.parent.parent.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FindFirstObjectByType<LeaderboardScript>().SubmitScoreRoutine((int)FindObjectOfType<AutoScrollScript>().score));
            StartCoroutine(FindObjectOfType<SceneTransitioner>().LoadScene(2));
        }
    }
    IEnumerator ScreenShake(float xShake, float yShake, float timeBetweenJolts, int numberOfShakes)
    {
        for (int i = numberOfShakes; i > 0; i--)
        {
            mainTransform.localPosition = new Vector3(Random.Range(-xShake, xShake), Random.Range(-yShake, yShake), 0);
            yield return new WaitForSeconds(timeBetweenJolts);
        }
        mainTransform.localPosition = Vector3.zero;
    }
    IEnumerator ColorShift(float speed, int intensity, int changePer)
    {
        Color originalColor = GetComponent<Renderer>().material.color;

        for (int i = 0; i < intensity; i++)
        {
            float t = i / (float)intensity; // Calculate the interpolation factor between originalColor and red

            // Interpolate between the original color and red based on t
            Color lerpedColor = Color.Lerp(originalColor, Color.red, t);

            // Apply the new color to the GameObject
            GetComponent<Renderer>().material.color = lerpedColor;

            yield return new WaitForSeconds(speed);
        }

        // Ensure the final color is set to red
        GetComponent<Renderer>().material.color = Color.white;
    }

}
