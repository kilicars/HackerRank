using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
class Solution {
    
    static int n;
    static long[] nc;
    static long total = 0;
    static long levels = 0;    
    static List<int>[] graph;
    static bool[] visited;
    
    public static void GrowStack(ThreadStart action, int stackSize = 32 * 1024 * 1024){
        var thread = new Thread(action, stackSize);
        #if __MonoCS__
        const BindingFlags bf = BindingFlags.NonPublic | BindingFlags.Instance;
        var it = typeof(Thread).GetField("internal_thread", bf).GetValue(thread);
        it.GetType().GetField("stack_size", bf).SetValue(it, stackSize);
        #endif
        thread.Start();
        thread.Join();
    }  
    
    static void NumChild(int u, long level){  
        visited[u] = true;
        nc[u] = 1;
        foreach (var v in graph[u]){                  
            if (!visited[v]){                      
                NumChild(v, level + 1);              
                nc[u] += nc[v];
            }           
        } 
        if (u != 0){
            total += level * (n - nc[u]);     
            levels += level;            
        }     
    }
    
    static void Solve(){            
        NumChild(0, 0);     
    }

    static void Main(String[] args) {
        n = Convert.ToInt32(Console.ReadLine());     
        graph = new List<int>[n];           
        for (int i = 0; i < n; i++){        
            graph[i] = new List<int>();          
        }
        nc = new long[n];        
        for(int a0 = 0; a0 < n - 1; a0++){
            string[] tokens_x = Console.ReadLine().Split(' ');
            int x = Convert.ToInt32(tokens_x[0]);
            int y = Convert.ToInt32(tokens_x[1]);
            graph[x - 1].Add(y - 1);
            graph[y - 1].Add(x - 1);
        }            
        visited = new bool[n];         
        ThreadStart start = new ThreadStart(Solve);       
        GrowStack(start);
        double result = (double)total / (double)levels;
        Console.WriteLine(String.Format("{0:F10}", result));           
    }
}
