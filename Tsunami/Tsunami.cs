using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solution {
    class Solution {
    
        struct node{
            public int index;
            public int location;
            public int height;
        }
    
        static int[] tree;
        static node[] nodes;

        static void build(int nodei, int start, int end){
            if (start == end){
                // Leaf node will have a single element
                tree[nodei] = nodes[start].height;
            }
            else{
                int left = 2 * nodei + 1;
                int right = 2 * nodei + 2;
                int mid = (start + end) / 2;
                // Recurse on the left child
                build(left, start, mid);
                // Recurse on the right child
                build(right, mid + 1, end);
                // Internal node will be the maximum of its children
                tree[nodei] = Math.Max(tree[left], tree[right]);
            }
        }
        static void update(int nodei, int start, int end, int idx, int val){
            if (start == end){
                // Leaf node
                tree[nodei] = val;
            }
            else {
                int left = 2 * nodei + 1;
                int right = 2 * nodei + 2;
                int mid = (start + end) / 2;
                if (start <= idx && idx <= mid){
                    // If idx is in the left child, recurse on the left child
                    update(left, start, mid, idx, val);
                }
                else{
                    // if idx is in the right child, recurse on the right child
                    update(right, mid + 1, end, idx, val);
                }
                // Internal node will be the maximum of its children
                tree[nodei] = Math.Max(tree[left], tree[right]);
            }
        }
        
        //first look at the left child as it is closer to the evacuated island
        //if there is not any greater length (i.e returns -1) then look at the right child
        static int query(int nodei, int start, int end, int l, int r, int val) {
            if (tree[nodei] <= val){
                return -1;
            }
            if (r < start || end < l){
                // range represented by a node is completely outside the given range
                return -1;
            }
            int mid = 0, left = 0, right = 0, p1 = 0;
            if (l <= start && end <= r){
                // range represented by a node is completely inside the given range
                if (start == end){
                    return nodes[start].location;
                }
                mid = (start + end) / 2;
                left = 2 * nodei + 1;
                right = 2 * nodei + 2;
                p1 = query(left, start, mid, l, r, val);
                if (p1 != -1)
                    return p1;
                else
                    return query(right, mid + 1, end, l, r, val);
            }
            // range represented by a node is partially inside and partially outside the given range
            mid = (start + end) / 2;
            left = 2 * nodei + 1;
            right = 2 * nodei + 2;       
            p1 = query(left, start, mid, l, r, val);
            if (p1 != -1)           
                return p1;               
            else            
                return query(right, mid + 1, end, l, r, val);
        }  
       
    
        static void Main(string[] args) {
        
            Dictionary<int,int> dict = new Dictionary<int,int>();
            Dictionary<int,int> dict2 = new Dictionary<int,int>();

            int n = Convert.ToInt32(Console.ReadLine());
            bool[] d = new bool[n];
            int[] lo = new int[n];
            nodes = new node[n];
            for (int s = 0; s < n; s++){
                string[] line = Console.ReadLine().Split(' ');
                int loc = Convert.ToInt32(line[0]);
                int h = Convert.ToInt32(line[1]);
                dict.Add(loc, s);
                nodes[s].index = s;
                nodes[s].location = loc;
                nodes[s].height = h;
                lo[s] = loc;
            }
            Array.Sort(nodes, (node1,node2) => node1.location.CompareTo(node2.location));
            for (int i = 0; i < n; i++){
                dict2.Add(nodes[i].index, i);
            }
            
            // Allocate memory for segment tree           
            int ht = (int)(Math.Ceiling(Math.Log(n) / Math.Log(2)));//Height of segment tree           
            int max_size = 2 * (int)Math.Pow(2, ht);//Maximum size of segment tree
            tree = new int[max_size]; // Memory allocation   
            //Build segment tree
            build(0, 0, n - 1);
            
            int q = Convert.ToInt32(Console.ReadLine());
            for (int s = 0; s < q; s++){
                string[] line = Console.ReadLine().Split(' ');
                int x = Convert.ToInt32(line[1]);
                if (line[0] == "e"){
                    x--;
                    if (d[x]){
                        Console.WriteLine("DROWNED");
                    }
                    else{
                        int index = dict2[x];
                        int r = query(0, 0, n - 1, index + 1, n - 1, nodes[index].height);
                        if (r == -1){
                            Console.WriteLine("IMPOSSIBLE");
                        }
                        else{
                            Console.WriteLine(r.ToString());
                        }
                    }
                }
                else{
                    int index = dict[x];
                    d[index] = true;
                    //update segment tree
                    update(0, 0, n - 1, dict2[index], -1);
                }           
            }        
        }    
    }
}
