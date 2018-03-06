using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {
    
    static int Check(char[][] b, int kr, int kc, string checkColor){
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
            while (r >= 0 && r < 8 && c >= 0 && c < 8){
                if (b[r][c] != '#'){
                    if (b[r][c] == queen || b[r][c] == rook)
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
            while (r >= 0 && r < 8 && c >= 0 && c < 8){
                if (b[r][c] != '#'){
                    if (b[r][c] == queen || b[r][c] == bishop)
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
            if (r >= 0 && r < 8 && c >= 0 && c < 8 && b[r][c] == knight)
                return 1;
        }
        return 0;
    }
    
    static char[][] Promote(char[][] b, char type, int c){
        char[][] copy = new char[8][];
        for (int i = 0; i < 8; i++){
            copy[i] = new char[8];
            for (int j = 0; j < 8; j++){
                copy[i][j] = b[i][j];
            }
        }
        copy[1][c] = '#';
        copy[0][c] = type;
        return copy;
    }

    static int waysToGiveACheck(char[][] b) {
        int total = 0;
        int bkr = -1; //black king's row
        int bkc = -1; //black king's column
        int wkr = -1; //white king's row
        int wkc = -1; //white king's column        
        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                if (b[i][j] == 'k'){
                    bkr = i;
                    bkc = j;
                }
                else if (b[i][j] == 'K'){
                    wkr = i;
                    wkc = j;
                }                
            }
        }  
        for (int i = 0; i < 8; i++){
            char[] p = new char[4]{'B', 'R', 'Q', 'N'};           
            if (b[1][i] == 'P' && b[0][i] == '#'){
                char[][] temp = Promote(b, 'B', i);
                if (Check(temp, wkr, wkc, "White") == 0){
                    for (int j = 0; j < 4; j++){
                        char[][] newb = Promote(b, p[j], i);
                        total += Check(newb, bkr, bkc, "Black");                    
                    }                       
                } 
            }
        }
        return total;
    }

    static void Main(String[] args) {
        int t = Convert.ToInt32(Console.ReadLine());
        for(int a0 = 0; a0 < t; a0++){
            char[][] board = new char[8][];
            for(int i = 0; i < 8; i++){              
                board[i] = Console.ReadLine().ToCharArray();
            }
            int result = waysToGiveACheck(board);
            Console.WriteLine(result);
        }
    }
}
