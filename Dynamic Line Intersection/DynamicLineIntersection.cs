using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution {
    
    public static int ConvertToInt(string s) {
        int y = 0;
        foreach (char c in s) {
            y = y * 10 + (c - '0');
        }
        return y;
    }
    
    static void Main(string[] args) {
        int n = ConvertToInt(Console.ReadLine());
        int end = 100000;
        int[] nums = new int[end + 1];
        List<int> list = new List<int>();
        int size = 100;
        int[,] k = new int[size, size];
        for (int s = 0; s < n; s++){
            string[] line = Console.ReadLine().Split(' ');
            if (line[0] == "+"){
                int a = ConvertToInt(line[1]);    
                int b = ConvertToInt(line[2]);
                if (a <= size){
                    k[a - 1, b % a]++;
                }
                else{                  
                    int start = b % a;
                    for (int i = start; i <= end; i += a){
                        nums[i]++;
                    }                    
                }
            }
            else if (line[0] == "-"){
                int a = ConvertToInt(line[1]);
                int b = ConvertToInt(line[2]);  
                if (a <= size){
                    k[a - 1, b % a]--;
                }
                else{                   
                    int start = b % a;              
                    for (int i = start; i <= end; i += a){
                        nums[i]--;
                    }                    
                }                
            }
            else{
                int q = ConvertToInt(line[1]);
                int result = nums[q];
                for (int i = 0; i < size; i++){
                    result += k[i, q % (i + 1)];
                }
                list.Add(result);
            }
        }
        Console.WriteLine(String.Join("\n", list));
    }
}
