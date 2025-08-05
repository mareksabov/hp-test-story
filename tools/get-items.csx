#r "nuget: Newtonsoft.Json, 13.0.3"

using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;

public class ItemContainer
{
    public List<Item> Items { get; set; }
}

public class Item
{
    public string Name { get; set; }
    public bool EnableItemFunctions { get; set; }
    public string SpecialType { get; set; }
    public bool IsDistractableRigidItem { get; set; }
    public bool IsInteractiveItem { get; set; }
    public bool IsActionItem { get; set; }
    public bool IsCharacterInteraction { get; set; }
}

var json = File.ReadAllText("HousePartyContent.hpc");
var container = JsonConvert.DeserializeObject<ItemContainer>(json);

Console.WriteLine($"Loaded {container.Items.Count} items.");

var csvPath = "items-names.csv";

// Hlavička CSV
var lines = new List<string>
{
    "Name,EnableItemFunctions,SpecialType,IsDistractableRigidItem,IsInteractiveItem,IsActionItem,IsCharacterInteraction"
};

// Dáta
foreach (var item in container.Items)
{
    // var line = $"{Escape(item.Name)},{item.EnableItemFunctions},{item.SpecialType},{item.IsDistractableRigidItem},{item.IsInteractiveItem},{item.IsActionItem},{item.IsCharacterInteraction}";
    var line = $"{Escape(item.Name)}";
    lines.Add(line);
}

// Zápis do súboru
File.WriteAllLines(csvPath, lines);
Console.WriteLine($"CSV saved to {csvPath}");

// Pomocná funkcia na escapovanie stringov s čiarkami alebo úvodzovkami
string Escape(string value)
{
    if (string.IsNullOrEmpty(value)) return "";
    if (value.Contains(",") || value.Contains("\""))
    {
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
    return value;
}

