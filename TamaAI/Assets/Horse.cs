using UnityEngine;
using System.Collections;

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

	public Animation walking;

	// Use this for initialization
	void Start () {

		frontHorse = GameObject.Find ("tama");
	}
	
	// Update is called once per frame
	void Update () {
		frameCount++;
		Sleeping ();
		UserLight ();

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

	public string[] starters = {"hi", "how", "ok","whoa","I","You"};
	public string[] finishers = {"haha", "oh", "no", "yes", "you"};
	public string[] puncts = {"!", ".", "?","!!!!"};

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
				GUI.Box(new Rect(90,140,200,50), "hi there female tama chan!");
			}
			if(distanceHorse < 6.0f && distanceHorse > 1.3f)
			{
				GUI.Box(new Rect(90,140,200,50), "ZZZZ zzzz ZZZ ^_^");
			}
		}
	}

	string ProcessString(string response)
	{
		int startRandom = Random.Range (0, starters.Length - 1);
		int finishRandom = Random.Range (0, finishers.Length - 1);
		int punRandom = Random.Range (0, puncts.Length - 1);

		string[] userWords = response.Split (' ');
		int userRandom = Random.Range (0, userWords.Length - 1);
		string newRes = starters [startRandom] + " " + userWords [userRandom] + " " + finishers [finishRandom] + puncts [punRandom];
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
