using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoScrollScript : MonoBehaviour
{
    private float height = 2.5f;
    public float speed;
    private Vector3 cameraShakeOffset;
    [SerializeField] float speedUp;
    [SerializeField] Transform playerPosition;
    [SerializeField] TMP_Text scoreDisplay;
    [SerializeField] TMP_Text multiplierDisplay;
    [SerializeField] Transform multiplierCountdown;
    [SerializeField] Transform multiplierGroup;
    [SerializeField] LeaderboardScript leaderboard;
    public int score = 0;
    public int multiplier = 1;
    public int multiplierClock;
    private int scoreClock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if (multiplierClock > 0)
            {
                multiplierGroup.localScale = Vector3.one; // Ensure the multiplier group is visible

                // Calculate the scale based on the remaining time
                float countdownScale = Mathf.Clamp01((float)multiplierClock / 500);
                multiplierCountdown.localScale = new Vector3(1, countdownScale, 1);

                multiplierClock--;
                multiplierDisplay.text = multiplier.ToString() + "X multiplier";
            }
            else
            {
                multiplier = 1;
                multiplierCountdown.localScale = Vector3.zero; // Hide the countdown
                multiplierGroup.localScale = Vector3.zero; // Hide the multiplier group
            }
        if (playerPosition.position.y > 0)
        {
            if (scoreClock > 0)
            {
                scoreClock--;
            }
            else
            {
                score += 1 * multiplier;
                scoreClock = 25;
            }

            height += speed;
            transform.position = new Vector3(0 + cameraShakeOffset.x, height + cameraShakeOffset.y, -10);
            speed += speedUp;
        }
        if (playerPosition.position.y < Camera.main.ViewportToWorldPoint(Vector2.zero).y - 3)
        {
            StartCoroutine(leaderboard.SubmitScoreRoutine((int)score));
            StartCoroutine(FindObjectOfType<SceneTransitioner>().LoadScene(2));
        }
        scoreDisplay.text = score.ToString();
    }
     public IEnumerator ScreenShake(float xShake, float yShake, float timeBetweenJolts, int numberOfShakes)
    {
        for (int i = numberOfShakes; i > 0; i--)
        {
            cameraShakeOffset.x = Random.Range(-xShake, xShake);
            yield return new WaitForSeconds(timeBetweenJolts);
        }
    }

    public void IncreaseMultiplier()
    {
        multiplier += 1;
        multiplierClock = 500;
    }
}
