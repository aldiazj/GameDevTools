using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// This class will act as a container for the information needed to be preserved
/// </summary>
[Serializable]
public class UserData 
{
    public bool booleanValue = false;
    public int intValue = 5;
    public int[] highScores = new int[10];
}
