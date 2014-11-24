using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Horse : MonoBehaviour {
	//Gameobject with horse when it's awake
	public GameObject horseAwake;
	//Gameobject for horse when it's sleeping
	public GameObject horseSleep;
	//Gameobject for player for checking distance magnitude
	public GameObject userPlayer;
	//frame count for the horse ai
	public int frameCount;

	public GameObject ptLight;

	private GameObject frontHorse;


	public GameObject idle;
	public GameObject horseWalking;

	public bool walking;

	public string[] starter = {"hi", "how", "ok","whoa","I","You"};
	public string[] finisher = {"haha", "no", "yes", "you","really"};
	public string[] punct = {"!", ".", "?","!!!!","!?",".",".","."};

	public List<string> starters = new List<string>();
	public List<string> finishers = new List<string>();
	public List<string> puncts = new List<string>();


	// Use this for initialization
	void Start () {

		frontHorse = GameObject.Find ("tama");
		walking = false;

		foreach(string s in starter)
		{
			starters.Add(s);
		}
		foreach(string s in finisher)
		{
			finishers.Add(s);
		}
		foreach(string s in punct)
		{
			puncts.Add(s);
		}

	}
	
	// Update is called once per frame
	void Update () {
		frameCount++;
		Sleeping ();
		UserLight ();


		if(walking)
		{
			idle.SetActive(false);
			horseWalking.SetActive(true);
		}
		else
		{
			horseWalking.SetActive(false);
			idle.SetActive(true);
		}

		//Debug.Log (starters.Count + " " + finishers.Count);
	
	}

	/// <summary>
	/// Makes horse fall asleep based on real time of day.
	/// </summary>
	void Sleeping()
	{
		//check if it is not night time
		if(!DayNightCycle.nightTime)
		{
			//make the horse awake
			horseAwake.SetActive(true);
			//make the horse sleeping
			horseSleep.SetActive(false);
		}
		else
		{
			horseSleep.SetActive(true);
			horseAwake.SetActive(false);
		}
	}

	/// <summary>
	/// Finds the distance between 2 game objects
	/// </summary>
	/// <returns>The mag.</returns>
	/// <param name="g1">GameObject 1.</param>
	/// <param name="g2">GameObject 2.</param>
	float DistanceMag(GameObject g1, GameObject g2)
	{
		Vector3 diff = (g1.transform.position - g2.transform.position);

		return diff.magnitude;
	}
	public string process = "";
	public string intro = "nehhh nehhh I'm Tama Chan :D";



	void OnGUI()
	{
		float distanceHorse = DistanceMag (userPlayer, frontHorse);

		if(!DayNightCycle.nightTime)
		{
			//Debug.Log (distanceHorse);
			if(distanceHorse < 6.0f && distanceHorse > 1.3f)
			{
				GUI.Box(new Rect(90,140,200,50), intro);

				process = GUI.TextField(new Rect(90,90,200,50), process, 512);

				if(GUI.Button(new Rect(90,190,200,25), "process"))
				{
					intro = ProcessString(process);
				}
			}
			if(distanceHorse <= 1.3f)
			{
				GUI.Box(new Rect(90,140,200,50), "I don't like humans :(");
			}
		}
		else
		{
			if(distanceHorse <= 1.3f)
			{
				GUI.Box(new Rect(90,140,200,50), "fooood");
			}
			if(distanceHorse < 6.0f && distanceHorse > 1.3f)
			{
				GUI.Box(new Rect(90,140,200,50), "ZZZZ zzzz ZZZ ^_^");
			}
		}
	}

	string ProcessString(string response)
	{
		int startRandom = Random.Range (0, starters.Count - 1);
		int finishRandom = Random.Range (0, finishers.Count - 1);
		int punRandom = Random.Range (0, puncts.Count - 1);

		string[] userWords = response.Split (' ');
		int userRandom = Random.Range (0, userWords.Length - 1);
		string newRes = starters [startRandom] + " " + userWords [userRandom] + " " + finishers [finishRandom];
		starters.Add (userWords [0]);

		finishers.Add (userWords [userWords.Length - 1]);

		return newRes;
	}

	void UserLight()
	{
		if(Input.GetKeyUp(KeyCode.L))
		{
			ptLight.SetActive(!ptLight.activeSelf);
		}
	}


}
