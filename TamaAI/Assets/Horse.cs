using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class Horse : MonoBehaviour {

	private List<string[]> NeuralNetwork = new List<string[]> ();
	//private List<string> tmpList = new List<string>();

	//use list matching!
	private List<string> neuron0 = new List<string> ();
	private List<string> neuron1 = new List<string> ();

	private Dictionary<string, string> TrainingSet = new Dictionary<string, string> ();

	string Process(string userInput)
	{
	
		List<int> matches = new List<int>();


		for(int i=0; i<neuron0.Count; ++i)
		{
			Debug.Log("neuron 0 = " + neuron0[i] + " user input = " + userInput);
			if(neuron0[i] == userInput)
			{
				matches.Add(i);
			}
		}
		Debug.Log("matches = " + matches.Count);
		string phrase = "roger_rules";

		if(matches.Count > 0)
		{
			phrase = neuron1 [Random.Range ( matches[0], matches[matches.Count-1] )]; //change to matches[something]
		}

		return phrase;
	}


	void SetTraining()
	{

		StreamReader readFile = new StreamReader ("Assets/trainingSet.txt");
		string line;

		while( (line = readFile.ReadLine()) != null)
		{
			//create a new neuron from text file
			string[] neuron = line.Split('*');
			//debug statement to make sure that the neuron is properly read from the .txt file!
			//Debug.Log(neuron[0] + " " + neuron[1]);

			neuron0.Add(neuron[0]);
			neuron1.Add(neuron[1]);
		}

		foreach(string a in neuron1)
		{
			wordBank.Add(a.Split(' '));
		}
		Debug.Log ("size of word bank = " + wordBank.Count); //debug!
	}
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

	private bool walking;

	public string[] starter = {"hi", "how", "ok","whoa","I","You"};
	public string[] finisher = {"haha", "no", "yes", "you","really"};
	public string[] punct = {"!", ".", "?","!!!!","!?",".",".","."};

	public List<string> starters = new List<string>();
	public List<string> finishers = new List<string>();
	public List<string> puncts = new List<string>();

	//this will be the main word bank
	private List<string[]> wordBank = new List<string[]>();



	public GameObject bounds;

	private int direction = -1;

	// Use this for initialization
	void Start () {
		SetTraining ();
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
		walking = true;


	}
	
	// Update is called once per frame
	void Update () {
		frameCount++;
		Sleeping ();
		UserLight ();
		CheckWalking ();
	
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
				walking = false;
				GUI.Box(new Rect(90,140,400,50), intro);

				process = GUI.TextField(new Rect(90,90,400,50), process, 512);

				if(GUI.Button(new Rect(90,190,400,25), "process"))
				{
					intro = ProcessString(process);
				}
			}
			else 
			{
				walking = true;
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
		string[] userWords = response.Split (' ');

		//add items to the word bank word bank is 2 dimentional list of strings
		wordBank.Add (userWords);

		string sentence = "";


		//for(int j=0; j<2; ++j)
		for(int j=0; j<Random.Range(1, wordBank.Count); ++j)
		{
			sentence += " ";
			sentence += wordBank [j] [Random.Range (0, wordBank [j].Length - 1)];
			Debug.Log("**" + sentence);
		}



		//string words to process
		string roger = Process (response);
		Debug.Log (roger);
		if(roger == "roger_rules")
		{
			roger = sentence;


		}
		return roger;


		/*
		int startRandom = Random.Range (0, starters.Count - 1);
		int finishRandom = Random.Range (0, finishers.Count - 1);
		int punRandom = Random.Range (0, puncts.Count - 1);

	
		int userRandom = Random.Range (0, userWords.Length - 1);
		string newRes = starters [startRandom] + " " + userWords [userRandom] + " " + finishers [finishRandom];
		starters.Add (userWords [0]);

		finishers.Add (userWords [userWords.Length - 1]);

		return newRes;
		*/
	}

	void UserLight()
	{
		if(Input.GetKeyUp(KeyCode.L))
		{
			ptLight.SetActive(!ptLight.activeSelf);
		}
	}

	void CheckWalking()
	{
		if(walking)
		{

			transform.Translate(0.1f * Vector3.forward);
			GameObject[] allTrees = GameObject.FindGameObjectsWithTag("Sample");
			foreach(GameObject obj in allTrees)
			{
				if(DistanceMag(obj, gameObject) < 4.0f)
				{
					//Debug.Log("this works!_!_!!_!_");
					transform.Rotate(0,0.3f,0);

				}
			}



		}
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "SampleCube")
		{
			Debug.Log("this works tooo!");
		}

	}
}
