/// <summary>
/// Day night cycle.
/// 
/// This function is attached to the empty GameObject "DayCycler"
/// The purpose of the attachment is because DayCycler gameObject will
/// not be influenced in any way by any other scripts
/// 
/// The purpose of the script is to add a simple day/night feature for the pet's environment
/// thereby making the agent aka the pet, dependent on the environment
/// </summary>
/// 
//the next 2 lines are default unity engine imports for these scripts to work
using UnityEngine;
using System.Collections;
//using this to track the system's time mainly to change the 'nightTime' variable
using System;

//next line is standard Unity Script stuff
public class DayNightCycle : MonoBehaviour {
	//TODO: work on this stuff
	//TODO: play with horse

	//NOTICE: ONLY PUBLIC OR SERIALIZABLE VARIABLES CAN BE PLAYED WITH IN THE UNITY INTERFACE :)

	//use this to check to see if the sky should be set to day mode or night mode
	//for instance if night is true, the pet should be sleeping
	//therefore the pet would not be hungry or feeling any emotion besides sleep
	public static bool nightTime = false;
	//the above variable is 'static' so that it can be used with other scripts
	//to reference a static variable in another script say: 
	//			DayNightCycle.nightTime = true; if it is night
	//if nightTime == false or (!nightTime) then it is day time by default

	//initialize datetime so that the hour can be checked 
	//this will mainly be used to change the 'nightTime' variable
	int landHour = DateTime.Now.Hour;
	//it is an integer because 'hours' are normally integers :P

	//the following material will be used for the skybox during the day
	//declared public so that custom artwork can be used for the material
	public Material skyDay;
	//the following material will be the skybox for night time
	//public so that custom artwork can be used for material
	public Material skyNight;

	//the GameObject for the directional light hanging above the lands
	//public so it can be changed in the interface
	public GameObject sunLight;

	// Use this for initialization <-important
	void Start () {
		//do not delete the line below until project is done
		//Debug.Log is the equivalent to System.out.println in Java
		//outputs the current time and date
		Debug.Log ("CURRENT HOUR OF DAY: " + landHour);
	}
	
	// Update is called once per frame <-very important
	void Update () {
		CycleDay ();
	}

	/// <summary>
	/// Cycles the day. Function will be placed in Update() and will continuously 
	/// check and take appropriate action based on the hour of the day
	/// </summary>
	private void CycleDay()
	{
		//first make changes to nightTime variable by checking day and time
		//do it here so that all functionality involving object changing 
		//for day/night can be done here
		//evaluate the landHour variable and if the hourly time is between:
		//16:00 and 6:00 it is night time
		if(landHour >= 19 || landHour <= 6)
		{
			//make it night
			nightTime = true;
		}
		else
		{
			//make it day
			nightTime = false;
		}




		//check to see if it is day time
		//need to make sure that it is day time or else animal will be awake 24/7
		if(!nightTime)
		{
			//Debug.Log("day!~");
			//because it's day, make the ambience more gray-ish
			RenderSettings.ambientLight = Color.gray;

			//turn the sunLight on
			sunLight.SetActive(true);
			//setActive(true) turns object on, false turns it off

			//rendersettings is where the skybox can be changed
			RenderSettings.skybox = skyDay;
			//change it to skyDay for daytime sky

			RenderSettings.fog = false;

		}
		// **MAKE ONE FOR EVENING + MORNING WITH YELLOW AMBIENT LIGHT! :O**

		//if night time
		else
		{
			//Debug.Log("night!");

			//if it's night, change the ambientLight of game to Color.Black
			RenderSettings.ambientLight = Color.black;
			//it's night and pitch dark would be more appropriate but
			//alternative light sources would be present at night

			//turn sunLight off
			sunLight.SetActive(false);
			//SetActive(false) turns object off but DOES NOT delete it

			//Render Settings -> skybox
			RenderSettings.skybox = skyNight;
			//change it to skyNight because it would be night

			//change fog to higher!
			RenderSettings.fogColor = Color.black;
			RenderSettings.fogDensity = 0.04f;
			RenderSettings.fog = true;

		}

	}

	public void Weather()
	{

	}
}
