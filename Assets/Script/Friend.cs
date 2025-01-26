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
	private float bubbleFloatSpeed = 0.5f;
	private float bubbleSinAmplitude = 2f;
	private float bubbleSinFrequency = 2f;
    private float transitionSpeed = 1.5f;
	private float minBubbleFloatingHeight = 3f;
	private float maxBubbleFloatingHeight = 10f;


    private void Awake()
	{
		friendName = funnyNames[Random.Range(0, funnyNames.Length)];
        friendState = Random.Range(0, 2) == 0 ? FriendState.Grounded : FriendState.Bubble;

		neutralAreaCenter = transform.parent.position;
	}

	private void Update()
	{
        ApplyOrRemoveBubble();


        if (friendState == FriendState.Bubble && transform.parent.position.y < minBubbleFloatingHeight)
        {
            StartCoroutine(TransitionToHeight(Random.Range(minBubbleFloatingHeight, maxBubbleFloatingHeight)));
        }

        if (friendState == FriendState.Grounded && transform.parent.position.y > 0f)
        {
            StartCoroutine(TransitionToHeight(0f));
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
		Vector3 newPosition = transform.parent.position;
		newPosition.y += Mathf.Sin(Time.time * bubbleSinFrequency) * bubbleSinAmplitude * Time.deltaTime;
		//newPosition.x += bubbleFloatSpeed * Time.deltaTime;
		transform.parent.position = newPosition;
	}

	private System.Collections.IEnumerator MoveTo(Vector3 targetPosition)
	{
		_isMoving = true;
		while (Vector3.Distance(transform.parent.position, targetPosition) > 0.1f)
		{
			transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, moveSpeed * Time.deltaTime);
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

        if (Vector3.Distance(transform.parent.position, enemy.transform.parent.position) < neutralAreaRadius)
        {
            enemyPlayer = enemy;
            return true;
        }

        return false;
    }

    private void RunAway(GameObject enemyPlayer)
    {
        Vector3 runDirection = (transform.parent.position - enemyPlayer.transform.parent.position).normalized;
        Vector3 targetPosition = transform.parent.position + runDirection * neutralAreaRadius;

        StopAllCoroutines();
        StartCoroutine(MoveTo(targetPosition));
		neutralAreaCenter = targetPosition;
    }

    private System.Collections.IEnumerator TransitionToHeight(float targetHeight)
    {
        
        Vector3 targetPosition = new Vector3(transform.parent.position.x, targetHeight, transform.parent.position.z);

        while (Mathf.Abs(transform.parent.position.y - targetHeight) > 0.1f)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, targetPosition, transitionSpeed * Time.deltaTime);
            yield return null;
        }

		if(friendState == FriendState.Grounded)
		{
			transform.parent.position = targetPosition;
			Destroy(this);
		}
    }

	private void ApplyOrRemoveBubble()
	{
		GetComponent<MeshRenderer>().enabled = friendState == FriendState.Bubble ? true : false;
	}
}
