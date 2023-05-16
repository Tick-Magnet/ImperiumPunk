using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.IO; //Used for File Management
using Newtonsoft.Json.Linq;

using UnityEngine.SceneManagement;





public class building_obj 
{
    public string building_type { get; set; }
    public float latitude { get; set; }
    public float longitude { get; set; }
    public int storage { get; set; }
    public System.DateTime date {get; set; }

}

public class achievement_obj
{
    public string ach_name { get; set; }
    public System.DateTime ach_date {get; set; }
}


public class account //JSON File Template
{
    public string user_name { get; set; }
    public string empire_name { get; set; }
    public string capital_name { get; set; }
    public int user_level { get; set; }
    public int buildings { get; set; }
    public int battles { get; set; }
    public int pawns { get; set; }
    public int food { get; set; }
    public int coal { get; set; }
    public int gears { get; set; }
    public bool hacker { get; set; }
    public string last_battle {get; set; }


    public List<building_obj> building_obj { get; set; }
    public List<achievement_obj> achievement_obj { get; set; }
    
}



public class user_data : MonoBehaviour
{
    public TextMeshProUGUI entered_user_name;
    public TextMeshProUGUI entered_empire_name;
    public TextMeshProUGUI entered_capital_name;
    public GameObject set_fields;
    string user_save_data;
    //private AudioSource source;

    //Check for User Data
    void Awake()
    {
        user_save_data = Application.persistentDataPath + "/user_data.json";
        read_file(); //Check if data already exists.
    }
    
    
    void read_file()
    {   

        // Does the file exist?  
        if (File.Exists(@user_save_data)) // Old User
        {
            //Read from File
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        } else { // New User 

            //Do Nothing
        }
    }

    public void submit_data(){
            
        //source = GetComponent<AudioSource>();
        //source.Play();


        var object_to_serialize = new account();
        Debug.Log("Entered User Name: " + entered_user_name.text.Length);
        if ((entered_user_name.text.Length <= 1) || (entered_empire_name.text.Length <= 1) || (entered_capital_name.text.Length <= 1))  {
            
            set_fields.SetActive(true);

        } else {
            var date_string = System.DateTime.Now.ToString("yyyyMMdd");
            object_to_serialize = new account{
                user_name = entered_user_name.text,
                empire_name = entered_empire_name.text,
                capital_name = entered_capital_name.text,
                pawns = 202,
                food = 546,
                coal = 1984,
                gears = 19,
                user_level = 1,
                buildings = 0,
                battles = 0,
                hacker = false,
                last_battle = date_string,
            };
            

            object_to_serialize.achievement_obj = new List<achievement_obj>
            {
                /*
                new achievement_obj 
                {
                    ach_name = "ImperiumPunk",
                },
                new achievement_obj 
                {
                    ach_name = "Scholar",
                },
                new achievement_obj 
                {
                    ach_name = "Farmer",
                },
                new achievement_obj 
                {
                    ach_name = "General",
                },
                new achievement_obj 
                {
                    ach_name = "HackerMan",
                },
                new achievement_obj 
                {
                    ach_name = "Pilot",
                },
                new achievement_obj 
                {
                    ach_name = "Loading...",
                },
                new achievement_obj 
                {
                    ach_name = "Stubborn",
                },
                new achievement_obj 
                {
                    ach_name = "IntegerOverflow",
                },
                new achievement_obj 
                {
                    ach_name = "Engineer",
                }
                */
            };

            object_to_serialize.building_obj = new List<building_obj>
            {
                /*
                // No Initial Buildings
                new building_obj 
                {
                    building_type = "coal_mine",
                    latitude = 37.710640F,
                    longitude = -89.224582F,
                },

                new building_obj 
                {
                    building_type = "coal_mine",
                    latitude = 37.710018F,
                    longitude = -89.223252F,
                }
                */
                
            };

            user_save_data = Application.persistentDataPath + "/user_data.json"; //Just to make sure user_save_data has been initialized.
                
            var account_string = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize); //Serialize Object
                

            //string user_name = (string)user_data["user_name"]; Debug.Log(user_name);
            //string empire_name = (string)user_data["user_empire"]; Debug.Log(empire_name);
                
            System.IO.File.WriteAllText(user_save_data, account_string);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}