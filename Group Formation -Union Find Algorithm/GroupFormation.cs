using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution {
    
    public struct Set{
        public int parent;
        public int rank;
        public long count;
        public long grade1;
        public long grade2;
        public long grade3;
    }   
    
    static int FindSet(Set[] sets, int v){
        if (sets[v].parent == v)
            return v;
        else 
            return FindSet(sets, sets[v].parent);
    }
    
    static void UnionSets(Set[] sets, int v1, int v2){
        
        int root1 = FindSet(sets, v1);
        int root2 = FindSet(sets, v2);
        
        if (sets[root1].rank > sets[root2].rank){
            sets[root2].parent = root1;
            sets[root1].count += sets[root2].count;
            sets[root1].grade1 += sets[root2].grade1;
            sets[root1].grade2 += sets[root2].grade2;
            sets[root1].grade3 += sets[root2].grade3;
           
        }           
        else if (sets[root1].rank < sets[root2].rank){
            sets[root1].parent = root2;
            sets[root2].count += sets[root1].count;
            sets[root2].grade1 += sets[root1].grade1;
            sets[root2].grade2 += sets[root1].grade2;
            sets[root2].grade3 += sets[root1].grade3;           
        }            
        else{
            sets[root2].parent = root1;
            sets[root1].rank++;
            sets[root1].count += sets[root2].count;
            sets[root1].grade1 += sets[root2].grade1;
            sets[root1].grade2 += sets[root2].grade2;
            sets[root1].grade3 += sets[root2].grade3;            
        }           
    }    

    static void Main(string[] args) {
        string[] nmabfst = Console.ReadLine().Split(' ');
        int n = Convert.ToInt32(nmabfst[0]);
        int m = Convert.ToInt32(nmabfst[1]);
        int a = Convert.ToInt32(nmabfst[2]);
        int b = Convert.ToInt32(nmabfst[3]);
        int f = Convert.ToInt32(nmabfst[4]);
        int s = Convert.ToInt32(nmabfst[5]);
        int t = Convert.ToInt32(nmabfst[6]);

        Set[] sets = new Set[n];       
        Dictionary<string, int> index = new Dictionary<string, int>();
        string[] names = new string[n];
        for (int i = 0; i < n; i++){
            string[] line = Console.ReadLine().Split(' ').ToArray();
            string name = line[0];
            int grade = Convert.ToInt32(line[1]);
            sets[i].parent = i;
            sets[i].rank = 0;
            sets[i].count = 1;
            sets[i].grade1 = 0;
            sets[i].grade2 = 0;
            sets[i].grade3 = 0;            
            if (grade == 1)
                sets[i].grade1 = 1;
            else if (grade == 2)
                sets[i].grade2 = 1;
            else if (grade == 3)
                sets[i].grade3 = 1;            
            index.Add(name, i);
            names[i] = name;
        }
                    
        long max = 1;
        for (int i = 0; i < m; i++){
            string[] line = Console.ReadLine().Split(' ').ToArray();
            string name1 = line[0];
            string name2 = line[1];
            int first = index[name1];
            int second = index[name2];
            int p1 = FindSet(sets, first);
            int p2 = FindSet(sets, second);

            if (p1 != p2){
                long total = sets[p1].count + sets[p2].count;
                if (total <= b && 
                    sets[p1].grade1 + sets[p2].grade1 <= f &&
                    sets[p1].grade2 + sets[p2].grade2 <= s &&
                    sets[p1].grade3 + sets[p2].grade3 <= t
                   )
                {
                    UnionSets(sets, p1, p2);
                    max = Math.Max(max, total);
                }               
            }            
        }
        if (max < a){
            Console.WriteLine("no groups");         
        }
        else{
            List<string> result = new List<string>();
            for (int i = 0; i < n; i++){               
                int parent = FindSet(sets, i);
                if (sets[parent].count == max){
                    result.Add(names[i]);
                }
            }
            result.Sort(StringComparer.Ordinal);
            Console.WriteLine(String.Join("\n", result));            
        }
        
    }
}
