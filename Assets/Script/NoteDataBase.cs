using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class NoteDataBase 
{
    Dictionary<string, List<string>> NoteDatas = new Dictionary<string , List<string>>();
   
    public NoteDataBase(TextAsset NoteDataTable)
    {

        int CardStatusIndex = CSVReader.Read(NoteDataTable).Count;

        for (int i = 0; i < CardStatusIndex; i++)
        {
            string key = "Line" + (i + 1).ToString();
            NoteDatas.Add(key, new List<string>());

            for (int j = 1; j <= 10; j++)
            {
                NoteDatas[key].Add(CSVReader.Read(NoteDataTable)[i][j.ToString()].ToString());
            }
         
        }
    }

    public string RandomData(string key)
    {
        return NoteDatas[key][Random.Range(0, 10)];
    }
   
}
