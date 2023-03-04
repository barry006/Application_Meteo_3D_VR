public static class ClassOpenWeatherMap
{   
    [System.Serializable]
    public class Parent
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
    [System.Serializable]
    public class Coord
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }
    [System.Serializable]
    public class Main
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }
    [System.Serializable]
    public class Wind
    {
        public float speed { get; set; }
        public int deg { get; set; }
    }
    [System.Serializable]
    public class Clouds
    {
        public int all { get; set; }
    }
    [System.Serializable]
    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }
    [System.Serializable]
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    //------------------ 
    /*
  [JsonProperty("coord")] public float _coord { get; set; }
  [JsonProperty("base")] public string _base { get; set; }
  [JsonProperty("visibility")] public int _visibility { get; set; }
  [JsonProperty("dt")] public int _dt { get; set; }
  [JsonProperty("timezone")] public int _timezone { get; set; }
  [JsonProperty("id")] public int _id { get; set; }
  [JsonProperty("name")] public string _name { get; set; }
  [JsonProperty("cod")] public int _cod { get; set; }
  [JsonProperty("lon")] public float _lon { get; set; }
  [JsonProperty("lat")] public float _lat { get; set; }
  [JsonProperty("id")] public int _id2 { get; set; }
  [JsonProperty("main")] public string _main2 { get; set; }
  [JsonProperty("description")] public string _description { get; set; }
  [JsonProperty("icon")] public string _icon { get; set; }
  [JsonProperty("temp")] public float _temp { get; set; }
  [JsonProperty("feels_like")] public float _feelsLike { get; set; }
  [JsonProperty("temp_min")] public float _tempMin { get; set; }
  [JsonProperty("temp_max")] public float _tempMax { get; set; }
  [JsonProperty("pressure")] public int _pressure { get; set; }
  [JsonProperty("humidity")] public int _humidity { get; set; }
  [JsonProperty("sea_level")] public int _seaLevel { get; set; }
  [JsonProperty("grnd_level")] public int _grndLevel { get; set; }
  [JsonProperty("speed")] public float _speed { get; set; }
  [JsonProperty("deg")] public int _deg { get; set; }
  [JsonProperty("gust")] public float _gust { get; set; }
  [JsonProperty("all")] public int _all { get; set; }
  [JsonProperty("type")] public int _type { get; set; }
  [JsonProperty("id")] public int _id3 { get; set; }
  [JsonProperty("country")] public string _country { get; set; }
  [JsonProperty("sunrise")] public int _sunrise { get; set; }
  [JsonProperty("sunset")] public int _sunset { get; set; }

  */
    //-------------------------------------------------------------------------
    /*
    [System.Serializable]
    public class Parent
    {
        [JsonProperty("coord")] private Coord _coord;
        [JsonProperty("weather")] private List<Weather> _weatherList;
        [JsonProperty("base")] private string _base;
        [JsonProperty("main")] private Main _main;
        [JsonProperty("visibility")] private int _visibility;
        [JsonProperty("wind")] private Wind _wind;
        [JsonProperty("clouds")] private Clouds _clouds;
        [JsonProperty("dt")] private int _dt;
        [JsonProperty("sys")] private Sys _sys;
        [JsonProperty("timezone")] private int _timezone;
        [JsonProperty("id")] private int _id;
        [JsonProperty("name")] private string _name;
        [JsonProperty("cod")] private int _cod;

        public Coord Coord
        {
            get { return _coord; }
        }

        public List<Weather> WeatherList
        {
            get { return _weatherList; }
        }

        public string Base
        {
            get { return _base; }
        }

        public Main Main
        {
            get { return _main; }
        }

        public int Visibility
        {
            get { return _visibility; }
        }

        public Wind Wind
        {
            get { return _wind; }
        }

        public Clouds Clouds
        {
            get { return _clouds; }
        }

        public int Dt
        {
            get { return _dt; }
        }

        public Sys Sys
        {
            get { return _sys; }
        }

        public int Timezone
        {
            get { return _timezone; }
        }

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int Cod
        {
            get { return _cod; }
        }
    }

    [System.Serializable]
    public class Coord
    {
        [JsonProperty("lon")] private float _lon;
        [JsonProperty("lat")] private float _lat;

        public float Lon
        {
            get { return _lon; }
        }

        public float Lat
        {
            get { return _lat; }
        }
    }

    [System.Serializable]
    public class Weather
    {
        [JsonProperty("id")] private int _id;
        [JsonProperty("main")] private string _main;
        [JsonProperty("description")] private string _description;
        [JsonProperty("icon")] private string _icon;

        public int Id
        {
            get { return _id; }
        }

        public string Main
        {
            get { return _main; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string Icon
        {
            get { return _icon; }
        }
    }

    [System.Serializable]
    public class Main
    {
        [JsonProperty("temp")] private float _temp;
        [JsonProperty("feels_like")] private float _feelsLike;
        [JsonProperty("temp_min")] private float _tempMin;
        [JsonProperty("temp_max")] private float _tempMax;
        [JsonProperty("pressure")] private int _pressure;
        [JsonProperty("humidity")] private int _humidity;
        [JsonProperty("sea_level")] private int _seaLevel;
        [JsonProperty("grnd_level")] private int _grndLevel;

        public float Temp
        {
            get { return _temp; }
        }

        public float FeelsLike
        {
            get { return _feelsLike; }
        }

        public float TempMin
        {
            get { return _tempMin; }
        }

        public float TempMax
        {
            get { return _tempMax; }
        }

        public int Pressure
        {
            get { return _pressure; }
        }

        public int Humidity
        {
            get { return _humidity; }
        }

        public int SeaLevel
        {
            get { return _seaLevel; }
        }

        public int GrndLevel
        {
            get { return _grndLevel; }
        }
    }

    [System.Serializable]
    public class Wind
    {
        [JsonProperty("speed")] private float _speed;
        [JsonProperty("deg")] private int _deg;
        [JsonProperty("gust")] private float _gust;

        public float Speed
        {
            get { return _speed; }
        }

        public int Deg
        {
            get { return _deg; }
        }

        public float Gust
        {
            get { return _gust; }
        }
    }

    [System.Serializable]
    public class Clouds
    {
        [JsonProperty("all")] private int _all;

        public int All
        {
            get { return _all; }
        }
    }

    [System.Serializable]
    public class Sys
    {
        [JsonProperty("type")] private int _type;
        [JsonProperty("id")] private int _id;
        [JsonProperty("country")] private string _country;
        [JsonProperty("sunrise")] private int _sunrise;
        [JsonProperty("sunset")] private int _sunset;

        public int Type
        {
            get { return _type; }
        }

        public int Id
        {
            get { return _id; }
        }

        public string Country
        {
            get { return _country; }
        }

        public int Sunrise
        {
            get { return _sunrise; }
        }

        public int Sunset
        {
            get { return _sunset; }
        }
    } */
    //-------------------------------
    /*
       [System.Serializable]
       public class Parent
       {

           public Weather[] weather { get; set; }
           public string _base { get; set; }
           public Main main { get; set; }
           public int visibility { get; set; }
           public int dt { get; set; }
           public int timezone { get; set; }
           public int id { get; set; }
           public string name { get; set; }
           public int cod { get; set; }
       }

       [System.Serializable]
       public class Main
       {
           public float temp { get; set; }
           public float feels_like { get; set; }
           public float temp_min { get; set; }
           public float temp_max { get; set; }
           public int pressure { get; set; }
           public int humidity { get; set; }
           public int sea_level { get; set; }
           public int grnd_level { get; set; }
       }


       [System.Serializable]
       public class Weather
       {
           public int id { get; set; }
           public string main { get; set; }
           public string description { get; set; }
           public string icon { get; set; }
       }*/
    //-----------------

}
