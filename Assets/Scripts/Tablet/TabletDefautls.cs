using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletDefautls 
{

    public static Tablet Begginer()
    {
        return new Tablet(9, 9, 10);
    }
    public static Tablet Intermediate()
    {
        return new Tablet(16, 16, 40);
    }
    public static Tablet Expert()
    {
        return new Tablet(30, 16, 99);
    }
    public static List<Tablet> tablets()
    {
        List<Tablet> tablets = new List<Tablet>();
        tablets.Add(Begginer());
        tablets.Add(Intermediate());
        tablets.Add(Expert());
        return tablets;
    }
}
