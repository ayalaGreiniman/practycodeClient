using System;
using System.Collections.Generic;

namespace TodoApi;

public partial class Item
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsComplete { get; set; }
    
    public static int count=1;

    public Item(){

    }
    public Item(string name,bool isComplete){
        
        Id=count++;
        Name=name;
        IsComplete=isComplete;
    }
}
