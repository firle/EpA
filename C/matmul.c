#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int main(){

    srand ( time ( NULL));
    
    double ** A;
    double ** B;
    double ** C;

    int n = 1500;
//    for(int n = 100; n<2000; n+=100)
    {
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
        int ckl = 0.0;

        clock_t t;
        t = clock();

        for (int k = 0; k < n; k++){                         //Matrixmultiplikation
            for (int l = 0; l < n; l++){
                ckl = 0.0;
                for (int m = 0; m < n; m++){
                     ckl= ckl + A[k][m] * B[m][l];
                }
                C[k][l] = ckl;
            }
        }

        t = clock() - t;
        double time_taken = ((double)t)/CLOCKS_PER_SEC; // in seconds
        printf("N=%d: %f s", n, time_taken);

        free(A); free(B); free(C);
    }
	
	return 0;
}
