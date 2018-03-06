using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {
    
    static long gcd(long a, long b){
        while (b != 0){
            long t = b;
            b = a % b;
            a = t;
        }
        return a;
    }    

    static void Main(String[] args) {
        int t = Convert.ToInt32(Console.ReadLine());       
        for(int a0 = 0; a0 < t; a0++){
            long total = 0;
            int n = Convert.ToInt32(Console.ReadLine());
            string[] a_temp = Console.ReadLine().Split(' ');
            int[] a = Array.ConvertAll(a_temp,Int32.Parse);
            Array.Sort(a);
            long fact = 6;
            if (n == 1){
                total = a[0];
                fact = 1;
            }                
            else if (n == 2){
                total = a[0] + a[1];
                fact = 2;
            }               
            else{
                for (int i = 0; i < n; i++){
                    total += (long)a[i];
                }
                total = total * 2 + a[0] + a[n - 1];                
            }
            long g = gcd(total,fact);
            if (fact / g == 1)
                Console.WriteLine(total / g);
            else
                Console.WriteLine(total / g + "/" + fact / g);
        }
    }
}
