using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution {
    
    static int size = 8;
    
    static int Check(char[,] b, int kr, int kc, string checkColor){
        char queen = 'q';
        char rook = 'r';
        char bishop = 'b';
        char knight = 'n';
        if (checkColor == "Black"){
            queen = 'Q';
            rook = 'R';
            bishop = 'B';
            knight = 'N';
        }
        //check horizontal & vertical
        int[] xh = new int[4]{-1, 1,  0, 0};        
        int[] yh = new int[4]{0,  0, -1, 1}; 
        for (int i = 0; i < 4; i++){
            int r = kr + xh[i];
            int c = kc + yh[i];
            while (r >= 0 && r < size && c >= 0 && c < size){
                if (b[r,c] != '#'){
                    if (b[r,c] == queen || b[r,c] == rook)
                        return 1;
                    else
                        break;
                }                
                r += xh[i];
                c += yh[i];
            }
        }    
        //check diagonal
        int[] xx = new int[4]{-1, -1,  1, 1};        
        int[] yy = new int[4]{-1,  1, -1, 1}; 
        for (int i = 0; i < 4; i++){
            int r = kr + xx[i];
            int c = kc + yy[i];
            while (r >= 0 && r < size && c >= 0 && c < size){
                if (b[r,c] != '#'){
                    if (b[r,c] == queen || b[r,c] == bishop)
                        return 1;
                    else
                        break;
                }                
                r += xx[i];
                c += yy[i];
            }
        }    
        //check knight            
        int[] x = new int[8]{-2, -2,  2, 2, -1, -1,  1, 1};        
        int[] y = new int[8]{-1,  1, -1, 1, -2,  2, -2, 2}; 
        for (int i = 0; i < 8; i++){
            int r = kr + x[i];
            int c = kc + y[i];
            if (r >= 0 && r < size && c >= 0 && c < size && b[r,c] == knight)
                return 1;
        }
        return 0;
    }
    static char[,] Move(char[,] b, char type, int r, int c, int exr, int exc){
        char[,] copy = new char[size,size];
        for (int i = 0; i < size; i++){
            for (int j = 0; j < size; j++){
                copy[i,j] = b[i,j];
            }
        }
        if (exr != -1 && exc != -1){
            copy[exr,exc] = '#';
        }        
        copy[r,c] = type;
        return copy;
    }    
   
    static bool MoveKing(char[,] b, int r, int c, int or, int oc){
        bool result = true;
        int[] x = new int[8]{-1, 1,  0, 0, -1, -1,  1, 1};        
        int[] y = new int[8]{0,  0, -1, 1, -1,  1, -1, 1};         
        for (int i = 0; i < x.Length; i++){
            int xx = r + x[i];
            int yy = c + y[i];
            if (xx >= 0 && xx < size && yy >= 0 && yy < size){
                char[,] newb = Move(b, 'k', xx, yy, r, c);  
                if (Check(newb, xx, yy, "Black") == 0 || Check(newb, or, oc, "Black") == 0)
                    return false; 
            }
        }
        return result;
    }
    
    static void checkmate(int x1, int y1, int x2, int y2) {
        char[,] board = new char[size,size];
        for (int i = 0; i < size; i++){
            for (int j = 0; j < size; j++){
                board[i,j] = '#';
            }
        }
        board[x1,y1] = 'k';
        board[x2,y2] = 'k';       
        char[,] not = new char[size,size];
        not[x1,y1] = 'X';
        not[x2,y2] = 'X';        
        char[,] not1 = new char[size,size];
        char[,] not2 = new char[size,size];
        char[,] not3 = new char[size,size];
        char[,] not4 = new char[size,size];
        for (int i = 0; i < size; i++){
            for (int j = 0; j < size; j++){
                not1[i,j] = not[i,j];
                not2[i,j] = not[i,j];
                not3[i,j] = not[i,j];
                not4[i,j] = not[i,j];
            }
        }                
                
        bool found = false;
        //Try one queen
        for (int i = 0; !found && i < size; i++){
            for (int j = 0; j < size; j++){
                if (not1[i,j] != 'X'){
                    char[,] newb = Move(board, 'Q', i, j, -1, -1);                             
                    if (Check(newb, x1, y1, "Black") == 1 && Check(newb, x2, y2, "Black") == 1){                                  
                        if (MoveKing(newb, x1, y1, x2, y2) && MoveKing(newb, x2, y2, x1, y1)){                              
                            Console.WriteLine(1);                       
                            Console.WriteLine("Q" + " " + i + " " + j); 
                            found = true;
                            break;
                        }
                    }
                }
            }
        }
        //Try two queens
        for (int i = 0; !found && i < size; i++){
            for (int j = 0; !found && j < size; j++){
                if (not2[i,j] == 'X')
                    continue;
                for (int m = 0; !found && m < size; m++){
                    for (int n = 0; !found && n < size; n++){
                        if (not2[m,n] == 'X')                            
                            continue;  
                        char[,] newb = Move(board, 'Q', i, j, -1, -1);                            
                        char[,] newb2 = Move(newb, 'Q', m, n, -1, -1);                                                     
                        if (Check(newb2, x1, y1, "Black") == 1 && Check(newb2, x2, y2, "Black") == 1){                               
                            if (MoveKing(newb2, x1, y1, x2, y2) && MoveKing(newb2, x2, y2, x1, y1)){                                 
                                Console.WriteLine(2);                                
                                Console.WriteLine("Q" + " " + i + " " + j);                                
                                Console.WriteLine("Q" + " " + m + " " + n);                                
                                found = true;                                
                                break;                                
                            }                            
                        }                     
                    }
                }
            }
        }  
        //Try three queens
        for (int i = 0; !found && i < size; i++){
            for (int j = 0; !found && j < size; j++){
                if (not3[i,j] == 'X')
                    continue;                 
                for (int m = 0; !found && m < size; m++){
                    for (int n = 0; !found && n < size; n++){
                        if (not3[m,n] == 'X')
                            continue;                    
                        for (int p = 0; !found && p < size; p++){
                            for (int q = 0; !found && q < size; q++){ 
                                if (not3[p,q] == 'X')
                                    continue;                                
                                char[,] newb = Move(board, 'Q', i, j, -1, -1);                                
                                char[,] newb2 = Move(newb, 'Q', m, n, -1, -1);                                
                                char[,] newb3 = Move(newb2,'Q', p, q, -1, -1);                                
                                if (Check(newb3, x1, y1, "Black") == 1 && Check(newb3, x2, y2, "Black") == 1){         
                                    if (MoveKing(newb3, x1, y1, x2, y2) && MoveKing(newb3, x2, y2, x1, y1)){                         
                                        Console.WriteLine(3);                                        
                                        Console.WriteLine("Q" + " " + i + " " + j);                                       
                                        Console.WriteLine("Q" + " " + m + " " + n);                                       
                                        Console.WriteLine("Q" + " " + p + " " + q);                                       
                                        found = true;                                        
                                        break;                                        
                                    }                                                              
                                }                                
                            }
                        }                     
                    }
                }
            }
        } 
    }
    static void Main(String[] args) {
        int t = Convert.ToInt32(Console.ReadLine());
        for (int t_i = 0; t_i < t; t_i++) {                
            string[] x1y1x2y2 = Console.ReadLine().Split(' ');            
            int x1 = Convert.ToInt32(x1y1x2y2[0]);            
            int y1 = Convert.ToInt32(x1y1x2y2[1]);            
            int x2 = Convert.ToInt32(x1y1x2y2[2]);            
            int y2 = Convert.ToInt32(x1y1x2y2[3]);            
            checkmate(x1, y1, x2, y2);
        }
    }
}
