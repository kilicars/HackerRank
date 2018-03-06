using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {

    static int mod = 1000000007;
    static long tileStackingProblem(int n, int m, int k) {
        if (k > n)
            k = n;
        long[,] dp = new long[m + 1,n + 1];
        dp[0,0] = 1;
        for (int i = 0; i <= k; i++){
            dp[1,i] = 1;
        }
        long sum = 0;
        for (int i = 1; i < m; i++){
            sum = 0;
            for (int j = 0; j <= k; j++){
                sum += dp[i,j];
                dp[i+1,j] = sum % mod;
            }
        }
        for (int i = 2; i <= m; i++){
            for (int j = k + 1; j <= n; j++){
                long val = dp[i,j-1] - dp[i-1, j-k-1] + dp[i-1, j];
                val %= mod;
                while (val < 0)
                    val += mod;
                dp[i,j] = val;   
            }
        }
        return dp[m,n];
    }

    static void Main(String[] args) {
        string[] tokens_n = Console.ReadLine().Split(' ');
        int n = Convert.ToInt32(tokens_n[0]);
        int m = Convert.ToInt32(tokens_n[1]);
        int k = Convert.ToInt32(tokens_n[2]);
        long result = tileStackingProblem(n, m, k);
        Console.WriteLine(result);
    }
}
