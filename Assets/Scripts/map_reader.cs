using UnityEngine;

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

class map_reader : MonoBehaviour
{

    [HideInInspector]
    public Dictionary<ulong, osm_node> nodes;

    [HideInInspector]
    public List<osm_way> ways;

    [HideInInspector]
    public osm_bounds bounds; 

    [Tooltip("The resource file that contains the OSM map data.")]
    public List<string> resource_file;

    public static Vector3 centre;
    public static bool centre_found = false;

    public bool is_ready {get; private set;}
    
    private IEnumerator coroutine;
    




    ///Boundaries 
    private double top_long = 0.0;
    private double bottom_long = 0.0;
    private double top_lat = 0.0;
    private double bottom_lat = 0.0;
    public string used_file;

    //Starting Code for map_reader
    void Start()
    {  
        //Calls setup() -- I'm using this method so it can be called again in the future.
        setup();
        //Debug.Log("Setup Called!");
    }

    public void setup() {

        //Keeps the chart_file script from running.
        is_ready = false;


        //Retrieve ResourceFile
        if (Directory.Exists (Application.persistentDataPath))
        {
            string data_folder = Application.persistentDataPath;
            DirectoryInfo d = new DirectoryInfo(data_folder);

            //Loop over every file and add it to resource_file list.
            foreach (var file in d.GetFiles("*.txt"))
            {
                //Save File In Resource Path
                resource_file.Add(file.ToString());
                
            }
             
            //Loop through the code to select the proper file.
            //Choose the proper data file.

            //Debug.Log("Current Location Latitude: " + Input.location.lastData.latitude);
            //Debug.Log("Current Location Longitude: " + Input.location.lastData.longitude);


            for (int j = 0; j < resource_file.Count; j++) 
            {
                

                int latFrom = resource_file[j].IndexOf("key.") + "key.".Length;
                int latTo = resource_file[j].LastIndexOf(",,");
                double upperLat = Convert.ToDouble(resource_file[j].Substring(latFrom, latTo - latFrom));    
                double lowerLat = upperLat - 0.01;
                    
                int longFrom = resource_file[j].IndexOf(",,") + ",,".Length;
                int longTo = resource_file[j].LastIndexOf(".txt");
                double upperLong = Convert.ToDouble(resource_file[j].Substring(longFrom, longTo - longFrom));    
                double lowerLong = upperLong - 0.01;
                
                //Debug.Log("###: " + j + " UpperLat: " + upperLat + " | UpperLong: " + upperLong);
                //Debug.Log(j + "-Upper Lat: " + upperLat + " | Lower Lat: " + lowerLat);
                //Debug.Log(j + "Upper Long: " + upperLong + " | Lower Long: " + lowerLong);



                //Check if player is in map quadrant 
                if (
                    //Latitude Check
                    (lowerLat < Input.location.lastData.latitude) && (Input.location.lastData.latitude < upperLat)
                    &&
                    (lowerLong < Input.location.lastData.longitude) && (Input.location.lastData.longitude < upperLong)
                    || (centre == Vector3.zero)

                ){
                    //Once found call chart_file
                    var txt_asset = System.IO.File.ReadAllText(resource_file[j]);

                    if (txt_asset.Length == 0)
                    {
                        reset();
                    }


                    used_file = resource_file[j];
                    //Debug.Log(txt_asset);
                    chart_file(txt_asset);
                    
                    top_long = upperLong;
                    bottom_long = lowerLong;
                    top_lat = upperLat;
                    bottom_lat = lowerLat;
                    
                 //Begin the File Charting
                    is_ready = true;
                }
                    

                    //Debug.Log("True Center File: " + resource_file[j]);
                    //Debug.Log("Current Location Latitude: " + Input.location.lastData.latitude);
                    //Debug.Log("Current Location Longitude: " + Input.location.lastData.longitude);

                    /* ---NINE SQUARE CODE --- 
                    //Then add each adjacent file.
                    for (int i = 0; i < resource_file.Count; i++) {
                        
                        

                        int tempLatFrom = resource_file[i].IndexOf("key.") + "key.".Length;
                        int tempLatTo = resource_file[i].LastIndexOf(",,");
                        double tempUpperLat = Convert.ToDouble(resource_file[i].Substring(tempLatFrom, tempLatTo - tempLatFrom));    
                        
                        int tempLongFrom = resource_file[i].IndexOf(",,") + ",,".Length;
                        int tempLongTo = resource_file[i].LastIndexOf(".txt");
                        double tempUpperLong = Convert.ToDouble(resource_file[i].Substring(tempLongFrom, tempLongTo - tempLongFrom));   

                        //Debug.Log("UpperLat: " + upperLat);
                        //Debug.Log("UpperLong: " + upperLong);
                        //Debug.Log("#: " + i + " TempUpperLat: " + tempUpperLat + " | TempUpperLong: " + tempUpperLong);

                        //Top Left
                        if (tempUpperLat.Equals(Math.Round(upperLat + 0.01, 3)) && tempUpperLong.Equals(Math.Round(upperLong - 0.01, 3))) {
                            txt_asset = System.IO.File.ReadAllText(resource_file[i]);
                            chart_file(txt_asset);
                            

                            //Debug.Log("Top Left: " + resource_file[i]);
                            //Debug.Log("TempUpperLat: " + tempUpperLat + " | TempUpperLong: " + tempUpperLong);
                        }

                        
                        if (tempUpperLat.Equals(Math.Round(upperLat + 0.01, 3)) && tempUpperLong.Equals(upperLong)) {
                            txt_asset = System.IO.File.ReadAllText(resource_file[i]);
                            //chart_file(txt_asset);
                            

                            //Debug.Log("Top Middle: " + resource_file[i]);
                            //Debug.Log("TempUpperLat: " + tempUpperLat + " | TempUpperLong: " + tempUpperLong);
                        }

                        //Top Right
                        if (tempUpperLat.Equals(Math.Round(upperLat + 0.01, 3)) && tempUpperLong.Equals(Math.Round(upperLong + 0.01, 3))) {
                            txt_asset = System.IO.File.ReadAllText(resource_file[i]);
                            //chart_file(txt_asset);
                            

                            //Debug.Log("Top Right: " + resource_file[i]);
                            //Debug.Log("TempUpperLat: " + tempUpperLat + " | TempUpperLong: " + tempUpperLong);
                        }

                        //Middle Left
                        if (tempUpperLat.Equals(upperLat) && tempUpperLong.Equals(Math.Round(upperLong - 0.01, 3))) {
                            txt_asset = System.IO.File.ReadAllText(resource_file[i]);
                            //chart_file(txt_asset);
                            

                            //Debug.Log("Middle Left: " + resource_file[i]);
                            //Debug.Log("TempUpperLat: " + tempUpperLat + " | TempUpperLong: " + tempUpperLong);
                        }

                        //Middle Right
                        if (tempUpperLat.Equals(upperLat) && tempUpperLong.Equals(Math.Round(upperLong + 0.01, 3))) {
                            txt_asset = System.IO.File.ReadAllText(resource_file[i]);
                            //chart_file(txt_asset);
                           

                            //Debug.Log("Middle Right: " + resource_file[i]);
                            //Debug.Log("TempUpperLat: " + tempUpperLat + " | TempUpperLong: " + tempUpperLong);
                        }
                        

                        //Bottom Left
                        if (tempUpperLat.Equals(Math.Round(upperLat - 0.01, 3)) && tempUpperLong.Equals(Math.Round(upperLong - 0.01, 3))) {
                            txt_asset = System.IO.File.ReadAllText(resource_file[i]);
                            //chart_file(txt_asset);


                            //Debug.Log("Bottom Left: " + resource_file[i]);
                            //Debug.Log("TempUpperLat: " + tempUpperLat + " | TempUpperLong: " + tempUpperLong);
                        }
                        

                        //Bottom Middle
                        if (tempUpperLat.Equals(Math.Round(upperLat - 0.01, 3)) && tempUpperLong.Equals(upperLong)) {
                            var testing_txt_asset = System.IO.File.ReadAllText(resource_file[i]);
                            //chart_file(txt_asset);

                            
                            //Debug.Log("Bottom Middle: " + resource_file[i]);
                            //Debug.Log("TempUpperLat: " + tempUpperLat + " | TempUpperLong: " + tempUpperLong);
                        }
                        

                        //Bottom Right
                        if (tempUpperLat.Equals(Math.Round(upperLat - 0.01, 3)) && tempUpperLong.Equals(Math.Round(upperLong + 0.01, 3)))  {
                            //txt_asset = System.IO.File.ReadAllText(resource_file[i]);
                            //chart_file(txt_asset);

                        
                            //Debug.Log("Bottom Right: " + resource_file[i]);
                            //Debug.Log("TempUpperLat: " + tempUpperLat + " | TempUpperLong: " + tempUpperLong);
                        } 
                    } */        
            }

            if (is_ready == false) {
                //No File Found - Hopefully stops that one graphical glitch.
                reset();
            }
            
        } else {
            
            Debug.Log("Failed to Load.");
        }

    }

    private void chart_file(string file)
    {

        //Load Data
        nodes = new Dictionary<ulong, osm_node>();
        ways = new List<osm_way>();
            
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(file);
        
        SetBounds(doc.SelectSingleNode("/osm/bounds"));
        get_nodes(doc.SelectNodes("/osm/node"));
        get_ways(doc.SelectNodes("/osm/way"));
        
    }


    void Update() {

        if(Input.anyKey) {
            //reset_world();
        }

        if (is_ready == true) {

            if (centre == Vector3.zero)
            {
                 reset();
            }

            //Update Game World if walking out of map.
            if 
            (((bottom_lat + 0.002)< Input.location.lastData.latitude) && (Input.location.lastData.latitude < (top_lat - 0.002))
            &&
            ((bottom_long + 0.002) < Input.location.lastData.longitude) && (Input.location.lastData.longitude < (top_long - 0.002))) {
            
            //Do Nothing
            } else {
                reset_world(); //I.E. Load in new area.
            }
        }
    }

    private void reset() {

        Debug.Log("Reseting World.");
        //End the GPS
        Input.location.Stop();

        Debug.Log(used_file);
        File.Delete(used_file);

        Debug.Log(map_reader.centre);
        map_reader.centre = Vector3.zero;
        Debug.Log(map_reader.centre);

        //Go back to the loading screen.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        
    }

    private void reset_world() {
        reset();
    }

    /*
    void OnApplicationPause()
    {
        reset();
    }

    
    void OnApplicationQuit()
    {
        Debug.Log("Application End.");
        Input.location.Stop();
        map_reader.centre = Vector3.zero;
        File.Delete(used_file);
    }
    */

    /*
    void Update()
    { 
        
        //Make sure all files have been added, then continue.
        if (preped_files == 9) {
            Debug.Log("Ready to start printing files!");
            foreach (osm_way w in ways)
            {   
                if (w.visible)
                {
                    //Red for buildings
                    Color color = Color.red;  

                    //yellow for roads           
                    if (!w.is_boundary) 
                    {
                        color = Color.yellow;
                    }


                    for (int i = 1; i < w.node_ids.Count; i++)
                    {
                        osm_node p1 = nodes[w.node_ids[i - 1]];
                        osm_node p2 = nodes[w.node_ids[i]];

                        Vector3 v1 = p1 - bounds.centre;
                        Vector3 v2 = p2 - bounds.centre;

                        Debug.DrawLine(v1, v2, color);
                    }
                }
            }
        } 
    }
    */


    private void get_ways(XmlNodeList xml_node_list)
    {

        foreach(XmlNode node in xml_node_list)
        {
            osm_way way = new osm_way(node);
            ways.Add(way);
        }

    }

    private void get_nodes(XmlNodeList xml_node_list)
    {
        foreach (XmlNode n in xml_node_list)
        {
            osm_node node = new osm_node(n);
            nodes[node.id] = node; 
        }
        
    }

    private void SetBounds (XmlNode xml_node)
    {
        bounds = new osm_bounds(xml_node);
        centre = bounds.centre; 
    }
}