 using UnityEngine;
 using System.Collections;
 
 public class Spiral
 {
     public int x = 0;
     public int y = 0;

     public void Next()
     {
         if(x == 0 && y == 0)
         {
             x = 1;
             return;
         }
         if(Mathf.Abs(x) > Mathf.Abs(y)+0.5f*Mathf.Sign(x) && Mathf.Abs(x) > (-y+0.5f))
             y += (int)Mathf.Sign(x);
         else
             x -= (int)Mathf.Sign(y);
     }

     public Vector2 NextPoint()
     {
         Next();
         return new Vector2(x,y);
     }
     
     public void Reset()
     {
         x = 0;
         y = 0;
     }
 }