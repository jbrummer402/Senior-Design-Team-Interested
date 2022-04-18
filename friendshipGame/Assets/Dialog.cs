using System.Collections.Generic;
// using Newtonsoft.Json;
using System;
using UnityEngine;
public class Dialog
{
    public class Entry
    {
        public class Option
        {
            public string id;
            public string title;
            public string response;
            public string character;
            public string val;
            public string scene;
        }
        public class Input
        {
            public string type;
            public string var;
            public string id;
        }

        public string character;
        public string response;
        public IList<Option> options;
        public Input input;
        public string microgame;
        public string var;
    }

    public int scene;
    public IDictionary<string, Entry> entries;

    public static Dialog CreateFromJSON(string jsonString) {
        // return JsonConvert.DeserializeObject<Dialog>(jsonString);
        return new Dialog();
    }

    public static Dialog CreateFromFile(string scene) {
        TextAsset text = Resources.Load("Dialogs/scene" + scene) as TextAsset;
        return CreateFromJSON(text.text);
    }

    public Boolean HasEntry(string id) {
        return entries.ContainsKey(id);
    }

    public Entry GetEntry(string id) {
        if(!HasEntry(id))
            return null;
        return entries[id];
    }

}
