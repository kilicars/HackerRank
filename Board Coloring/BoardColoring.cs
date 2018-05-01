using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

class Solution {
    
    static string canColor(int n, int m, int[] a, int[] b) {    
        Array.Sort(a); //n rows
        Array.Sort(b); //m rows
        long[] sumb = new long[m];
        sumb[0] = b[m - 1];
        for (int i = m - 2; i >= 0; i--){
            int j = m - 1 - i;
            sumb[j] = sumb[j - 1] + b[i];
        }
        long sum = 0;
        int len = a[n - 1] + 1;
        int[] c = new int[len];
        int nonzero = 0;
        for (int i = 0; i < n; i++){
            c[a[i]]++;
            if (a[i] > 0){
                nonzero++;
            }
        }
        long add = nonzero;
        for (int i = 1; i < len; i++){
            sum += add;
            long colsum = 0;
            if (i > m){
                colsum = sumb[m - 1];
            }
            else{
                colsum = sumb[i - 1];
            }
            if (sum < colsum){
                return "NO";
            }            
            add -= c[i];
        }
        for (int i = len; i <= m; i++){
            if (sum < sumb[i - 1]){
                return "NO";
            } 
        }      
        return "YES";
    }
    
    public static int ConvertToInt(string s) {
        int y = 0;
        foreach (char c in s) {
            y = y * 10 + (c - '0');
        }
        return y;
    }    

    static void Main(string[] args) {
        int t = ConvertToInt(Console.ReadLine());
        for (int tItr = 0; tItr < t; tItr++) {
            string[] nm = Console.ReadLine().Split(' ');
            int n = ConvertToInt(nm[0]);
            int m = ConvertToInt(nm[1]);
            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), ATemp => ConvertToInt(ATemp)) ;
            int[] B = Array.ConvertAll(Console.ReadLine().Split(' '), BTemp => ConvertToInt(BTemp)) ;                
            string result = canColor(n, m, A, B);                
            Console.WriteLine(result);                
        }
    }
}
