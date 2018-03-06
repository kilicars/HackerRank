using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {
static int n;
    static int winningHands(int m, int x, int[] a) {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        dict.Add(a[0] % m, 1);
        Dictionary<int, int> newdict = new Dictionary<int, int>(dict);
        for (int i = 1; i < n; i++){           
            foreach(KeyValuePair<int, int> pair in dict){
                long p = (long)a[i] * pair.Key;
                int key = (int)(p % m);
                if (newdict.ContainsKey(key))
                    newdict[key] += pair.Value;
                else
                    newdict.Add(key, pair.Value);                
            }
            int k = a[i] % m;
            if (newdict.ContainsKey(k))
                newdict[k]++;
            else
                newdict.Add(k,1);
            dict = new Dictionary<int, int>(newdict);
        }
        if (dict.ContainsKey(x))
            return dict[x];
        else
            return 0;
    }

    static void Main(String[] args) {
        string[] tokens_n = Console.ReadLine().Split(' ');
        n = Convert.ToInt32(tokens_n[0]);
        int m = Convert.ToInt32(tokens_n[1]);
        int x = Convert.ToInt32(tokens_n[2]);
        string[] a_temp = Console.ReadLine().Split(' ');
        int[] a = Array.ConvertAll(a_temp,Int32.Parse);
        int result = winningHands(m, x, a);
        Console.WriteLine(result);
    }
}
