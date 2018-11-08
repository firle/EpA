#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int main(){
	int n = 600;

    srand ( time ( NULL));
    
    double ** A;
    double ** B;
    double ** C;
    
    A = malloc(n * sizeof(double *));
    B = malloc(n * sizeof(double *));
    C = malloc(n * sizeof(double *));
    for(int a = 0; a < n; a++){
		A[a] = malloc(n * sizeof(double));
		B[a] = malloc(n * sizeof(double));
		C[a] = malloc(n * sizeof(double));
	}
    
    for (int i = 0; i < n; i++){                         //Matrizen mit Zufallszahlen füllen, Ergebnismatrix mit 0
		for (int j = 0; j < n; j++){
			A[i][j] = (double) rand()/(double) RAND_MAX;
			B[i][j] = (double) rand()/(double) RAND_MAX;
			//printf("%f\n", B[i][j]);
			C[i][j] = 0;
		}
	}
	
	for (int k = 0; k < n; k++){                         //Matrixmultiplikation
		for (int l = 0; l < n; l++){
			for (int m = 0; m < n; m++){
				C[k][l] = C[k][l] + A[k][m] * B[m][l];
			}
			printf("%f ", C[k][l]);
		}
	}
	
	return 0;
	}
