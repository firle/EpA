//
//  matrix.c
//  
//
//  Created by Ephraim on 01.11.18.
//

#include "matrix.h"


#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int main()
{
    int n = 100;

    double random_value;

    srand ( time ( NULL));

    random_value = (double)rand()/RAND_MAX ;//float in range 0 to 1

    double A[n][n];
    double B[n][n];
    double C[n][n];

    for (int i = 0; i< n ; ++i) {
        for (int j = 0; j< n; ++j) {
            A[i][j] =(double)rand()/RAND_MAX ;
            B[i][j] =(double)rand()/RAND_MAX ;
            C[i][j] = i;
        }
    }

//    mult(&A,&B,&C,n);


    return 0;
}

//void mult(double[][] *A, double[][] *B, double[][] *C, int n)
//{
//    for (int i = 0; i< n; ++i) {
//        for (int k = 0; k< n; ++k) {
//            for (int j=0; j< n; ++j) {
//                (*C)[i][k] = (*C)[i][k] + (*A)[i][j] * (*B)[j][k];
//            }
//        }
//    }
//}

