using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {
static int mod = 1000000007;
    static void Main(String[] args) {
        string[] tokens_n = Console.ReadLine().Split(' ');
        int n = Convert.ToInt32(tokens_n[0]);
        int k = Convert.ToInt32(tokens_n[1]);
        int x = Convert.ToInt32(tokens_n[2]);
        
        long prev1 = 1;
        long prev2 = 0;
        long cur1 = 1;
        long cur2 = 0;        
        for (int i = 1; i < n; i++){
            cur1 = (k - 1) * prev2;
            cur1 %= mod;
            cur2 = (k - 2) * prev2 + prev1;
            cur2 %= mod;
            prev1 = cur1;
            prev2 = cur2;
        }
        long answer = 0;
        if (x == 1)
            answer = cur1;
        else
            answer = cur2;
        Console.WriteLine(answer);
    }
}
