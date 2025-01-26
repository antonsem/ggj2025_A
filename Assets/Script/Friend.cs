using UnityEngine;

public class Friend : MonoBehaviour
{
	public enum FriendState { Grounded, Bubble }

	private PlayerType friendTeam => GameData.Instance.PlayerTwoFriends.Contains(gameObject) ? PlayerType.PlayerTwo :
		GameData.Instance.PlayerOneFriends.Contains(gameObject) ? PlayerType.PlayerOne : PlayerType.None;


    [SerializeField] private FriendState friendState;
	

	private string[] funnyNames = {
		"Quackmire",
		"Doctor Beany Bubble",
		"BubbleButt",
		"Captain Floater",
		"Duck Norris",
		"Lord Bobba",
		"Floatimus Prime",
		"Bubbly McGee",
		"Splashy",
		"Professor Quack",
		"Waddlesworth",
		"Bubblebeard",
		"Michael Bubble",
		"Bubbly Bubbleson",
		"Suds McBubb",
		"Bubble Troublemaker",
		"Bobba Vader",
		"Floaty Friend",
		"Sir Bubblesworth"
	};

    public string friendName;

    private bool _isMoving = false;
	private Vector3 neutralAreaCenter;
	private float neutralAreaRadius = 10f;
	private float moveSpeed = 2f;
	private float bubbleFloatSpeed = 2f;
	private float bubbleSinAmplitude = 0.5f;
	private float bubbleSinFrequency = 2f;
    private float transitionSpeed = 1.5f;
	private float minBubbleFloatingHeight = 10f;
	private float maxBubbleFloatingHeight = 20f;


    private void Awake()
	{
		friendName = funnyNames[Random.Range(0, funnyNames.Length)];
        friendState = Random.Range(0, 2) == 0 ? FriendState.Grounded : FriendState.Bubble;

		neutralAreaCenter = transform.position;
	}

	private void Update()
	{
        ApplyOrRemoveBubble();


        if (friendState == FriendState.Bubble && transform.position.y < minBubbleFloatingHeight)
        {
            StartCoroutine(TransitionToHeight(Random.Range(minBubbleFloatingHeight, maxBubbleFloatingHeight)));
        }

        if (friendState == FriendState.Grounded && transform.position.y > 1f)
        {
            StartCoroutine(TransitionToHeight(1f));
        }

        if (friendState == FriendState.Bubble)
		{
			MoveInBubble();
			return;
		}

        if (friendTeam == PlayerType.None)
        {
            MoveAround();
        }

        if (EnemyPlayerIsClose(out GameObject enemyPlayer))
        {
            RunAway(enemyPlayer);
            return;
        }
	}

	private void MoveAround()
	{
		// Move randomly within the neutral area
		if (!_isMoving)
		{
			Vector3 randomTarget = neutralAreaCenter + new Vector3(
				Random.Range(-neutralAreaRadius, neutralAreaRadius),
				0, 
				Random.Range(-neutralAreaRadius, neutralAreaRadius)
			);

			StartCoroutine(MoveTo(randomTarget));
		}
	}

	private void MoveInBubble()
	{
		Vector3 newPosition = transform.position;
		newPosition.y += Mathf.Sin(Time.time * bubbleSinFrequency) * bubbleSinAmplitude * Time.deltaTime;
		newPosition.x += bubbleFloatSpeed * Time.deltaTime;
		transform.position = newPosition;
	}

	private System.Collections.IEnumerator MoveTo(Vector3 targetPosition)
	{
		_isMoving = true;
		while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
			yield return null;
		}
		_isMoving = false;
	}

	public void SetGrounded()
	{
		friendState = FriendState.Grounded;
	}

	private bool HasTeam()
	{
		return friendTeam != PlayerType.None;
	}

 private bool EnemyPlayerIsClose(out GameObject enemyPlayer)
    {
        enemyPlayer = null;
        if (!HasTeam()) return false;

        PlayerType enemyTeam = friendTeam == PlayerType.PlayerOne ? PlayerType.PlayerTwo : PlayerType.PlayerOne;
        GameObject enemy = GameResources.Instance.GetPlayer(enemyTeam);

        if (Vector3.Distance(transform.position, enemy.transform.position) < neutralAreaRadius)
        {
            enemyPlayer = enemy;
            return true;
        }

        return false;
    }

    private void RunAway(GameObject enemyPlayer)
    {
        Vector3 runDirection = (transform.position - enemyPlayer.transform.position).normalized;
        Vector3 targetPosition = transform.position + runDirection * neutralAreaRadius;

        StopAllCoroutines();
        StartCoroutine(MoveTo(targetPosition));
		neutralAreaCenter = targetPosition;
    }

    private System.Collections.IEnumerator TransitionToHeight(float targetHeight)
    {
        
        Vector3 targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);

        while (Mathf.Abs(transform.position.y - targetHeight) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
            yield return null;
        }
    }

	private void ApplyOrRemoveBubble()
	{
		GetComponent<MeshRenderer>().enabled = friendState == FriendState.Bubble ? true : false;
	}
}
