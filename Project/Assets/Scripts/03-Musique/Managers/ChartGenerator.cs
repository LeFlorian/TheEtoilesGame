using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using System.IO;  
using System.Linq;

/// Generer les notes a partir d'un fichier de configuration
public class ChartGenerator 
{
    public static Data.ChartData UpdateChart(DefaultAsset chartTxt){
        Data.ChartData chart = new Data.ChartData();
        // Lire le .chart
        TextAsset textAsset = new TextAsset(File.ReadAllText(AssetDatabase.GetAssetPath(chartTxt)));
        List<string> strings = ChartParsor.splitChartBlocks(textAsset);
        // Get Values
        chart.songName = ChartParsor.GetValue(strings[0], "MusicStream");
		chart.resolution = int.Parse(ChartParsor.GetValue(strings[0], "Resolution"));
		chart.bpm = (int)(float.Parse(ChartParsor.GetValue(strings[1], "B")) / 1000f);
		chart.sections = ChartParsor.GetSections(strings[2]);
		chart.notes = ChartParsor.GetNotes(strings[3]);
		chart.tick = ChartParsor.GetTick(chart.bpm, chart.resolution);

        return chart;
    }
}



public class ChartParsor 
{

    public static List<string> splitChartBlocks(TextAsset textAsset){

        List<string> strings = new List<string>();
        // Separer les diffrentes parties
        string pattern = "\\{([^}]*)\\}"; // tout entre {}
        MatchCollection matchCollection = Regex.Matches(textAsset.text, pattern);

		strings.Add(matchCollection[0].Groups[1].Value);  // [Song]
		strings.Add(matchCollection[1].Groups[1].Value);  // [SyncTrack] / Parameter
		strings.Add( matchCollection[2].Groups[1].Value); // [Events] / Section
		strings.Add(matchCollection[3].Groups[1].Value);  // [...] / note 

        return strings;
    }

    public static string GetValue(string textinput, string pattern){
        var patternRegExp = new Regex(pattern+".+");
        Match m = patternRegExp.Match(textinput);
        string value = m.ToString();
        value = cleanString(value, pattern);
        return value;
    } 

	public static List<Data.Note> GetNotes(string notesField)
	{   
        var patternRegExp = new Regex("[^\\s].+"); // toutes les lignes
        char[] separator = new char[2] { ' ', '\t' };
		List<Data.Note> list = new List<Data.Note>();
        MatchCollection matchCollection = patternRegExp.Matches(notesField);
        foreach (Match line in matchCollection)
        {
            // Debug.Log(line.Groups[0].Value);
            string text = line.Groups[0].Value;
            string[] array2 = text.Split(separator);
            int noteID = int.Parse(array2[5]);
            int num = int.Parse(array2[0]);
            int noteCorridorID = int.Parse(array2[3]);
            int num2 = int.Parse(array2[4]);
            string noteTypeID = array2[2];
            int noteColorID = int.Parse(array2[3]);
            Data.Note item = new Data.Note(noteID, num, noteCorridorID, num2, noteTypeID, noteColorID);
            list.Add(item);
        }
		return list;
	}

    public static List<Data.Section> GetSections(string sectionField)
	{
        var patternRegExp = new Regex("[^\\s].+"); // toutes les lignes
        char separator = '=';
		List<Data.Section> list = new List<Data.Section>();
        MatchCollection matchCollection = patternRegExp.Matches(sectionField);
        foreach (Match line in matchCollection)
        {
            // Debug.Log(line.Groups[0].Value);
            string text = line.Groups[0].Value;
            string[] splits = text.Split(separator);
            int num = int.Parse(cleanString(splits[0]));
            string text2 = cleanString(splits[1],"section");
            
            Data.Section item = new Data.Section(text2, num);
            list.Add(item);
		}
		return list;
	}

    public static string cleanString(string txt,string name = "^$"){
        string txt2 = txt;
        if (Regex.IsMatch(txt,"\"[^\"]+\"")){
            int num2 = txt2.IndexOf('"') + 1;
            int num3 = txt2.LastIndexOf('"');
            txt2 = txt.Substring(num2, num3 - num2);
        }
        if (Regex.IsMatch(txt,"=")){
            int num2 = txt2.IndexOf('=') + 1;
            txt2 = txt2.Substring(num2);
        }
        if (Regex.IsMatch(txt2,name)){
            // Debug.Log(Regex.IsMatch(txt2,name));
            txt2 = txt2.Replace(name, "");
        }
        txt2 = txt2.Trim();
        return txt2;
    }


    public static float GetTick(int bpm, int resolution)
	{
		return 60000f / ((float)bpm * (float)resolution) / 1000f;
	}



}