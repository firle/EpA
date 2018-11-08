#!/bin/sh

#  matmulTime.sh
#  
#
#  Created by Ephraim on 08.11.18.
#  

gcc matmul.c -o matmul
time ./matmul
