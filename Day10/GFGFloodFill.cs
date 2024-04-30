namespace Day10;
using System;
using System.Collections.Generic;

// Copied, with minimal changes, from:
// https://www.geeksforgeeks.org/flood-fill-algorithm/
// by divyeshrabadiya07

public class GFGFloodFill
{
	// Function that returns true if the given pixel is valid
    static bool isValid(char[,] screen, int m, int n, int x, int y, char prevC, char newC)
    {
        if(x < 0 || x >= m || y < 0 || y >= n || screen[x, y] != prevC
           || screen[x,y]== newC)
            return false;
        return true;
    }

    public static void FloodFill(char[,] screen, int m, int n, int x, int y, char prevC, char newC)
    {
        List<Tuple<int,int>> queue = new List<Tuple<int,int>>();
  
        // Append the position of starting pixel of the component
        queue.Add(new Tuple<int,int>(x, y));
  
        // Color the pixel with the new color
        screen[x,y] = newC;
  
        // Check whether the queue is empty. The queue will still have elements in it if
        // there are still pixels with the previous color (prevC). This code
        // does not check for diagonal adjacency
        while(queue.Count > 0)
        {
            // Dequeue the front node
            Tuple<int,int> currPixel = queue[queue.Count - 1];
            queue.RemoveAt(queue.Count - 1);
  
            int posX = currPixel.Item1;
            int posY = currPixel.Item2;
   
            // Check if the adjacent pixels are valid
            // If valid, color with newC and enqueue
            
            //right
            if(isValid(screen, m, n, posX + 1, posY, prevC, newC))
            {
                screen[posX + 1,posY] = newC;
                queue.Add(new Tuple<int,int>(posX + 1, posY));
            }
            //left
            if(isValid(screen, m, n, posX - 1, posY, prevC, newC))
            {
                screen[posX-1,posY] = newC;
                queue.Add(new Tuple<int,int>(posX - 1, posY));
            }
            //down
            if(isValid(screen, m, n, posX, posY + 1, prevC, newC))
            {
                screen[posX,posY + 1]= newC;
                queue.Add(new Tuple<int,int>(posX, posY + 1));
            }
            //up
            if(isValid(screen, m, n, posX, posY - 1, prevC, newC))
            {
                screen[posX,posY - 1]= newC;
                queue.Add(new Tuple<int,int>(posX, posY - 1));
            }

        }
        
    }
  
}
 

