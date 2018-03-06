using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
class Solution {
    
    static int mod = 1000000009;
    static long[] facts;
    static long[] inverses;
    static long[] inv;
    static int[,] cmb;
    
    static long power(long x, long y, int p){
        long res = 1; // Initialize result
        x = x % p;  // Update x if it is more than or equal to p
        while (y > 0)
        {
            // If y is odd, multiply x with result
            if ((y & 1) > 0)
                res = (res*x) % p;

            // y must be even now
            y = y>>1; // y = y/2
            x = (x*x) % p;  
        }
        return res;
    }     
    
    // Returns modulo inverse of a with respect to m using
    // extended Euclid Algorithm
    // Assumption: a and m are coprimes, i.e., gcd(a, m) = 1
    static int ModInverse(int a, int m){
        int m0 = m, t, q;
        int x0 = 0, x1 = 1;

        if (m == 1)
          return 0;

        while (a > 1){
            // q is quotient
            q = a / m;
            t = m;
            // m is remainder now, process same as Euclid's algo
            m = a % m;
            a = t;
            t = x0;
            x0 = x1 - q * x0;
            x1 = t;
        }
        // Make x1 positive
        if (x1 < 0)
           x1 += m0;
        return x1;
    }
    
    static void Fact(int num){
        facts[0] = 1;
        facts[1] = 1;
        for (int i = 2; i <= num; i++){
            facts[i] = (facts[i - 1] * i) % mod;
        }
    }
    
    static long Comb(long n, long r){      
        return ((((facts[n] * inverses[n - r]) % mod) * inverses[r]) % mod);  
    }
    
    static long highwayConstruction(long n, int k) {
        long[] s = new long[k + 1];
        long begin = n % mod;
        begin *= (n + 1) % mod;
        begin %= mod;
        begin *= inv[2];
        begin %= mod;
        s[1] = begin;
        for (int i = 2; i <= k; i++){
            //calculate s[i]
            long right = 0;
            int d = 2;
            bool dec = false;
            for (int j = i - 1; j >= 1; j--){
                right += (cmb[i + 1, d]  * s[j]) % mod;
                if (right >= mod)
                    right -= mod;
                if (d == i + 1)
                    dec = true;                  
                if (dec)
                    d--;
                else
                    d++;             
            }
            right += n % mod;               
            if (right >= mod)           
                right -= mod;            
            long left = power(n + 1, i + 1, mod) - 1;
            long diff = left - right;
            if (diff < 0)
                diff += mod;
            s[i] = (diff * inv[i + 1]) % mod;
        }
        return s[k];
    }

    static void Main(String[] args) {
        int q = Convert.ToInt32(Console.ReadLine());
        int len = 1010;           
        facts = new long[len + 1];       
        Fact(len);        
        inverses = new long[len + 1];        
        for (int i = 0; i <= len; i++){        
            inverses[i] = ModInverse((int)facts[i], mod);           
        }         
        inv = new long[len + 1];        
        for (int i = 0; i <= len; i++){        
            inv[i] = ModInverse(i, mod);           
        }        
        cmb = new int[len + 1, len + 1];
        for (int i = 0; i < len; i++){
            for (int j = 0; j <= i; j++){
                cmb[i,j] = (int)Comb(i,j);
            }
        }
        long[] res = new long[q];
        for(int a0 = 0; a0 < q; a0++){
            string[] tokens_n = Console.ReadLine().Split(' ');
            long n = Convert.ToInt64(tokens_n[0]);
            int k = Convert.ToInt32(tokens_n[1]);
            if (n <= 2)
                res[a0] = 0;
            else{
                res[a0] =  highwayConstruction(n - 1, k) - 1;               
            }
        }
        Console.WriteLine(String.Join("\n", res));    
    }
}
