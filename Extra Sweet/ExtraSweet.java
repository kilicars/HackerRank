import java.io.*;
import java.util.*;
import java.text.*;
import java.math.*;
import java.util.regex.*;

public class Solution {
    
    static class node{
        public int min;
        public int max;
        public node(int mn, int mx){
            min = mn;
            max = mx;
        }
    }
    
    static int BinSearch(ArrayList<node> a, int search){
        int left = 0;
        int right = a.size() - 1;
        while (left <= right){
            int middle = ((left + right) / 2);
            if (a.get(middle).min == search)
                return middle;
            else if (a.get(middle).min < search)
                left = middle + 1;
            else
                right = middle - 1;
        }
        return right;        
    }    

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        int n = in.nextInt();
        int s = in.nextInt();
        ArrayList<node> list = new ArrayList<node>();
        node first = new node(0, n - 1);
        list.add(first);
        long[] t = new long[s];
        for(int a0 = 0; a0 < s; a0++){
            //string[] tokens_l = Console.ReadLine().Split(' ');
            int l = in.nextInt();
            int r = in.nextInt();
            int index = 0;
            int exl = 0;
            int exr = 0;

            index = BinSearch(list, l);
            //Console.WriteLine("index = " + index);                
            boolean onlimit = false;           
            if (list.get(index).min == l)               
                onlimit = true;

            int curmax = list.get(index).max;
            int curmin = list.get(index).min;
              
            if (curmax > r + 1){ //update this            
                exr = r + 1;     
                list.get(index).min = exr + 1;        
            }            
            else if (curmax == r + 1){           
                exr = r + 1;                                                         
                list.remove(index);                                    
            }            
            else{            
                if (index < list.size() - 1){ //update next                
                    exr = list.get(index + 1).min;     
                    int nextmax = list.get(index + 1).max;                       
                    list.remove(index);                                                                           
                                          
                    if (exr < nextmax){    
                        list.get(index).min = exr + 1;
                    }  
                    else{
                        list.remove(index);
                    }
                }                  
                else{                                                
                    list.remove(index);                                                   
                }                                    
            } 
            if (curmin < l - 1){
                exl = l - 1;     
                node leftnode = new node(curmin, exl - 1);  
                if (index > 0){
                    list.add(index, leftnode);
                }
                else{
                    list.add(0, leftnode);
                }
            }
            else{             
                if (onlimit){
                    //update prev (if exits)
                    if (index > 0){
                        exl = list.get(index - 1).max;                         
                        if (exl > list.get(index - 1).min){                            
                            list.get(index - 1).max = exl - 1;
                        }
                        else{
                            list.remove(index - 1);
                        }
                    }                    
                }
                else{
                    exl = l - 1;
                }                                       
            }                                            
            t[a0] = ((long)(r + l) * (long)(r - l + 1) / 2) + exl + exr;
            System.out.println(t[a0]);
        }       
        in.close();
    }
}
