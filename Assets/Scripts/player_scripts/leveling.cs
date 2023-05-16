using UnityEngine;

using System.IO; //Used for File Management
using Newtonsoft.Json.Linq;


using TMPro;

//using System.Text.Json;
//using System.Text.Json.Serialization;

public class leveling : MonoBehaviour
{
    public GameObject level_up_box; 
    public TextMeshProUGUI level_ui;

    public TextMeshProUGUI empire_name_1;
    public TextMeshProUGUI empire_name_2;
    public TextMeshProUGUI empire_name_3;
    public TextMeshProUGUI empire_name_4;
    public TextMeshProUGUI empire_name_5;
    public TextMeshProUGUI empire_name_6;
    public TextMeshProUGUI empire_name_7;
    public TextMeshProUGUI empire_name_8;
    public TextMeshProUGUI empire_name_9;
    public TextMeshProUGUI empire_name_10;
    

    // Start is called before the first frame update
    void Start()
    {
        //If Level == 1 open up tutorial UI.
        string user_save_data = Application.persistentDataPath + "/user_data.json";
        string user_data_string = File.ReadAllText(user_save_data);
        JObject data_file = JObject.Parse(user_data_string);

        int level = (int)data_file["user_level"];
        JArray building_array = (JArray)data_file["building_obj"]; 
        
        string name = (string)data_file["empire_name"];

        empire_name_1.text = empire_name_2.text = empire_name_3.text = empire_name_4.text = empire_name_5.text = name.ToString();
        empire_name_6.text = empire_name_7.text = empire_name_8.text = empire_name_9.text = empire_name_10.text = name.ToString();

    }



    // Update is called once per frame
    void Update()
    {
        
        //Updating JSON file
        string user_save_data = Application.persistentDataPath + "/user_data.json";
        string user_data_string = File.ReadAllText(user_save_data);
        JObject data_file = JObject.Parse(user_data_string);

        int level = (int)data_file["user_level"];
        JArray building_array = (JArray)data_file["building_obj"]; 
        
       
        int buildings = building_array.Count;
        
        int farms = 0;
        int mines = 0;
        int colonies = 0;
        int factories = 0;
        
        switch(level)
        {
            case 1:
                
                //Requirement: Build a Settlement.
                
                
                for (int j = 0; j < buildings; j++) {

                    string current_building = building_array[j]["building_type"].ToString();

                    //Debug.Log(current_building);

                    if (current_building.Equals("city")) {

                        //Level Up!
                        level += 1;
                        level_ui.text = level.ToString();
                        data_file["user_level"] = level;
                        

                        level_up_box.SetActive(true);

                        //Write Updated Values
                        string account_string = data_file.ToString();
                        System.IO.File.WriteAllText(user_save_data, account_string);

                        break;
                    }
                }
                break;


            case 2:

                //Requirement: Build Three Farms.
                farms = 0;

                for (int j = 0; j < buildings; j++) {

                    string current_building = building_array[j]["building_type"].ToString();

                    if (current_building.Equals("farm")) {
                        
                        farms += 1;
                    }

                    if (farms > 2) {

                        //Level Up!
                        level += 1;
                        level_ui.text = level.ToString();
                        data_file["user_level"] = level;
                        

                        level_up_box.SetActive(true);

                        //Write Updated Values
                        string account_string = data_file.ToString();
                        System.IO.File.WriteAllText(user_save_data, account_string);

                        break;
                    }
                }

                break;
            case 3:

                //Requirement: Build Two Coal-Mines.
                mines = 0;

                for (int j = 0; j < buildings; j++) {

                    string current_building = building_array[j]["building_type"].ToString();

                    if (current_building.Equals("mine")) {
                        
                        mines += 1;
                    }

                    if (mines > 1) {

                        //Level Up!
                        level += 1;
                        level_ui.text = level.ToString();
                        data_file["user_level"] = level;
                        

                        level_up_box.SetActive(true);

                        //Write Updated Values
                        string account_string = data_file.ToString();
                        System.IO.File.WriteAllText(user_save_data, account_string);
                        break;
                    }
                }


                break;
            case 4:
                
                //Requirement: Build One Factory.
                factories = 0;

                for (int j = 0; j < buildings; j++) {

                    string current_building = building_array[j]["building_type"].ToString();

                    if (current_building.Equals("factory")) {
                        
                        factories += 1;
                    }

                    if (factories > 0) {

                        //Level Up!
                        level += 1;
                        level_ui.text = level.ToString();
                        data_file["user_level"] = level;
                        

                        level_up_box.SetActive(true);

                        //Write Updated Values
                        string account_string = data_file.ToString();
                        System.IO.File.WriteAllText(user_save_data, account_string);
                        break;
                    }
                }
                
                break;
            case 5:
                
                //Requirement: 80 Pawns, 350 Food, 1000 Coal, 20 Gears
                
                
                //For Each Resource
                var a = collect_resources.pawns_stored;      
                var b = collect_resources.food_stored;
                var c = collect_resources.coal_stored;
                var d = collect_resources.gears_stored;

                
                if ((a > 79) && (b > 349) && (c > 999) && (d > 19)) {
                    //Level Up!
                    level += 1;
                    level_ui.text = level.ToString();
                    data_file["user_level"] = level;
                        

                    level_up_box.SetActive(true);

                    //Write Updated Values
                    string account_string = data_file.ToString();
                    System.IO.File.WriteAllText(user_save_data, account_string);
                    break;
                }
                
                break;
            case 6:

                //Requirement: Build One Factory.
                colonies = 0;

                for (int j = 0; j < buildings; j++) {

                    string current_building = building_array[j]["building_type"].ToString();

                    if (current_building.Equals("city")) {
                        
                        colonies += 1;
                    }

                    if (colonies > 1) {

                        //Level Up!
                        level += 1;
                        level_ui.text = level.ToString();
                        data_file["user_level"] = level;
                        

                        level_up_box.SetActive(true);

                        //Write Updated Values
                        string account_string = data_file.ToString();
                        System.IO.File.WriteAllText(user_save_data, account_string);
                        break;
                    }
                }
                
                break;
            case 7:

                //Requirement: Build 12 farms & 8 coal mines.
                farms = 0;
                mines = 0;

                for (int j = 0; j < buildings; j++) {

                    string current_building = building_array[j]["building_type"].ToString();

                    if (current_building.Equals("farm")) {
                        
                        farms += 1;
                    }

                    if (current_building.Equals("mine")) {
                        
                        mines += 1;
                    }

                    if ((farms > 11) && (mines > 7)) {

                        //Level Up!
                        level += 1;
                        level_ui.text = level.ToString();
                        data_file["user_level"] = level;
                        

                        level_up_box.SetActive(true);

                        //Write Updated Values
                        string account_string = data_file.ToString();
                        System.IO.File.WriteAllText(user_save_data, account_string);
                        break;
                    }
                }
                
                break;

            case 8:

                //Requirement: Build 5 Factories
                factories = 0;

                for (int j = 0; j < buildings; j++) {

                    string current_building = building_array[j]["building_type"].ToString();

                    if (current_building.Equals("factory")) {
                        
                        factories += 1;
                    }

                    if (factories > 4) {

                        //Level Up!
                        level += 1;
                        level_ui.text = level.ToString();
                        data_file["user_level"] = level;
                        

                        level_up_box.SetActive(true);

                        //Write Updated Values
                        string account_string = data_file.ToString();
                        System.IO.File.WriteAllText(user_save_data, account_string);
                        break;
                    }
                }
                
                break;

            case 9:

                //Requirement: Build 6 Cities
                colonies = 0;

                for (int j = 0; j < buildings; j++) {

                    string current_building = building_array[j]["building_type"].ToString();

                    if (current_building.Equals("city")) {
                        
                        colonies += 1;
                    }

                    if (colonies > 5) {

                        //Level Up!
                        level += 1;
                        level_ui.text = level.ToString();
                        data_file["user_level"] = level;
                    
                        level_up_box.SetActive(true);

                        //Write Updated Values
                        string account_string = data_file.ToString();
                        System.IO.File.WriteAllText(user_save_data, account_string);
                        break;
                    }
                }

                break;
            case 10:

                //Do Nothing!
                
                break;
                
        }   
            



    }
}
