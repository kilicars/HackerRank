using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {


    static void Main(String[] args) {       
        int n = Convert.ToInt32(Console.ReadLine());
        string s = Console.ReadLine();
        int[] a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        HashSet<int> rb = new HashSet<int>();
        HashSet<int> gw = new HashSet<int>();
        int result = 1;
        char[] cols = new char[n];         
        
        for (int i = 0; i < s.Length; i++){
            if (s[i] == 'R' || s[i] == 'B')
                rb.Add(a[i]);
            else if (s[i] == 'G' || s[i] == 'W')
                gw.Add(a[i]);                       
        }               
        Array.Sort(a);
        for (int i = 0; i < n; i++){
            if (rb.Contains(a[i]))
                cols[i] = 'R';
            else
                cols[i] = 'G';
        }
        char prev = cols[0];
        int exr = 0, exg = 0;
        for (int i = 1; i < n; i++){
            if (cols[i] == 'G'){
                if (prev == 'R'){
                    prev = 'G';
                    result++;                    
                }
                else
                    exg++;
            }
            else if (cols[i] == 'R'){
                if (prev == 'G'){
                    prev = 'R';
                    result++;                    
                }
                else
                    exr++;
            }           
        }
        if (exr > 0 && exg > 0){
            result += Math.Min(exr, exg);
        }
        Console.WriteLine(result);        
    }
}
