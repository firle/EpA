import java.lang.Math;
import java.time.*;

public class matmul {
    public static void main(String[] args) {
        int n = 100;
        double[][] A = new double[n][n];
        double[][] B = new double[n][n];
        double[][] C = new double[n][n];

        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                A[i][j] = Math.random();
                // System.out.println(A[i][j]);
                B[i][j] = Math.random();
            }
        }
        long startTime = System.currentTimeMillis();
        double ckl = 0.0;
        for (int k = 0; k < n; k++) {
            for (int l = 0; l < n; l++) {
                ckl = 0.0;
                for (int m = 0; m < n; m++) {
                    ckl = ckl + A[k][m] * B[m][l];
                }
                C[k][l] = ckl;
                // System.out.println(C[k][l]);
            }
        }
        long stopTime = System.currentTimeMillis();
        long elapsedTime = stopTime - startTime;
        System.out.println("n= " + n + ", " + (elapsedTime) + " ms");
    }

}