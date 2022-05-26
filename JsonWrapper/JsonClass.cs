using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace JsonWrapper
{
    public class JsonClass
    {
        private string _jsonFileName = string.Empty;
        /// <summary>
        /// Updates the filename to load and save the json file.
        /// May not be null or empty
        /// </summary>
        [JsonIgnore]
        public string JsonFileName
        {
            get
            {
                return _jsonFileName;
            }
            set
            {
                if (value != null && value != string.Empty)
                {
                    _jsonFileName = value;
                }
            }
        }

        /// <summary>
        /// Constructor with filename parameter.
        /// </summary>
        /// <param name="fileName">Contains the filename / filepath to the json file</param>
        public JsonClass(string fileName) : this()
        {
            JsonFileName = fileName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonClass()
        {
            //
        }

        /// <summary>
        /// Creates a path from multiple strings. Environment variables are supported
        /// </summary>
        /// <param name="path">Contains an array or multiple strings to build the path</param>
        /// <returns>Returns the final path</returns>
        public string CreatePath(params string[] path)
        {
            for (int i = 0; i < path.Length; i++)
            {
                path[i] = Environment.GetEnvironmentVariable(path[i].Replace("%", "")) ?? path[i];
            }

            return Path.Combine(path);
        }

        /// <summary>
        /// Converts the current class to a json string
        /// </summary>
        /// <param name="uglyFormat">Decides to make the json format as easy readable or not</param>
        /// <returns>Returns the created json string</returns>
        public string ToJsonString(bool uglyFormat)
        {
            string json = JsonConvert.SerializeObject(this, uglyFormat ? Formatting.None : Formatting.Indented);

            return json;
        }

        /// <summary>
        /// Converts the current class to a json string
        /// </summary>
        /// <returns>Returns the created json string</returns>
        public string ToJsonString()
        {
            return ToJsonString(false);
        }

        /// <summary>
        /// Converts the given string to a JsonClass
        /// </summary>
        /// <typeparam name="T">Needs the type of the json class (not JsonClass)</typeparam>
        /// <param name="json">Json content for the convert action</param>
        /// <returns>Returns a new instance of JsonClass</returns>
        public T ToJsonClass<T>(string json) where T : JsonClass
        {
            T jsonClass = JsonConvert.DeserializeObject<T>(json);
            jsonClass.JsonFileName = this.JsonFileName;

            return jsonClass;
        }

        /// <summary>
        /// Saves the current class to a json file
        /// </summary>
        /// <param name="encoding">Sets the encoding to write the file</param>
        /// <param name="uglyFormat">Decides to make the json format as easy readable or not</param>
        public virtual void Save(Encoding encoding, bool uglyFormat)
        {
            string json = this.ToJsonString(uglyFormat);

            File.WriteAllText(JsonFileName, json, encoding);
        }

        /// <summary>
        /// Saves the current class to a json file
        /// </summary>
        /// <param name="encoding">Sets the encoding to write the file</param>
        public void Save(Encoding encoding)
        {
            Save(encoding, false);
        }

        /// <summary>
        /// Saves the current class to a json file
        /// </summary>
        public void Save()
        {
            Save(Encoding.UTF8);
        }

        /// <summary>
        /// Loads the content of a json file
        /// </summary>
        /// <typeparam name="T">Needs the type of the json class (not JsonClass)</typeparam>
        /// <param name="encoding">Sets the encoding to read the file</param>
        /// <returns>Returns a new JsonClass instance</returns>
        public virtual T FromFile<T>(Encoding encoding) where T : JsonClass
        {
            string json = File.ReadAllText(JsonFileName, encoding);

            T jsonClass = this.ToJsonClass<T>(json);

            return jsonClass;
        }

        /// <summary>
        /// Loads the content of a json file
        /// </summary>
        /// <typeparam name="T">Needs the type of the json class (not JsonClass)</typeparam>
        /// <returns>Returns a new JsonClass instance</returns>
        public T FromFile<T>() where T : JsonClass
        {
            return FromFile<T>(Encoding.UTF8);
        }
    }
}
