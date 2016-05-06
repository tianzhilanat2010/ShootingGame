using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface GetDataId
{
   int GetId();
}

public class DataItem
{
    public int RowCount;
    public string Tag;
    public string Value;
}

public class RowDataArray
{
    public int RowIndex;
    public List<DataItem> DataItemArray = new List<DataItem>();

    public RowDataArray(int rowIndex)
    {
        RowIndex = rowIndex;
    }

    public void AddItem(string tag, string value)
    {
        DataItem item = new DataItem();
        item.RowCount = RowIndex;
        item.Tag = tag;
        item.Value = value.Equals(string.Empty) ? "0" : value;
        DataItemArray.Add(item);
    }

    public string GetValue(string tag)
    {
        if (DataItemArray.Count > 0)
        {
            for (int i = 0; i < DataItemArray.Count; ++i)
            {
                if (DataItemArray[i].Tag.Equals(tag.Trim()))
                    return DataItemArray[i].Value;
            }
        }
        return string.Empty;
    }
}

public class DataArray
{
    public List<RowDataArray> DataItemArray;

    public DataArray()
    {
        DataItemArray = new List<RowDataArray>();
        DataItemArray.Clear();
    }

    public RowDataArray GetRowData(int rowIndex)
    {
        if (DataItemArray.Count > 0)
        {
            for (int i = 0; i < DataItemArray.Count; ++i)
            {
                if (DataItemArray[i].RowIndex == rowIndex)
                {
                    return DataItemArray[i];
                }
            }
        }
        return null;
    }
}

public class Database : Singleton<Database>
{
	private const string Path = "Table/Client/";

    public DataArray LoadTxt(string tableName)
    {
        TextAsset binAsset = Resources.Load(Path + tableName, typeof(TextAsset)) as TextAsset;
   
		string newTxt = binAsset.text.Replace("\r", string.Empty);

        string[] lineArrays = newTxt.Split("\n"[0]);
        string[] title = lineArrays[0].Split(","[0]);

        DataArray csv = new DataArray();

        for (int rowIndex = 1; rowIndex < lineArrays.Length; ++rowIndex)
        {
            string txt = lineArrays[rowIndex].Trim();
            if (txt.Length <= 0)
                break;

            RowDataArray rowData = new RowDataArray(rowIndex - 1);

            string[] line = lineArrays[rowIndex].Split(","[0]);
            for (int columnIndex = 0; columnIndex < line.Length; ++columnIndex)
            {
                if (line[columnIndex].Length > 0 && columnIndex < title.Length)
                {
                    rowData.AddItem(title[columnIndex].Trim(), line[columnIndex].Trim());
                }
            }

            if (rowData.DataItemArray.Count > 0)
            {
                csv.DataItemArray.Add(rowData);
            }
            
        }
        return csv;
    }
}
