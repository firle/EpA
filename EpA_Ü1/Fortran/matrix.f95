    module procedures

    contains
        subroutine mymatmul(A,B,C,n)
            REAL(kind=8) :: cik
            REAL(kind=8), DIMENSION(:,:), intent(in) :: A,B
            REAL(kind=8), DIMENSION(:,:), intent(out) :: C
            INTEGER, intent(in) :: n


            do i = 1, n
                do k = 1, n
                    cik = 0
                    do j = 1, n
                        cik = cik + A(i,j) * B(j,k)
                    end do
                    C(i,k) = cik
                end do
            end do
        end subroutine mymatmul
    end module

    program test_random_number
        use procedures

        INTEGER :: n
        REAL(kind=8) , dimension(:,:), allocatable :: A
        REAL(kind=8) , dimension(:,:), allocatable :: B
        REAL(kind=8) , dimension(:,:), allocatable :: C
        real :: start_time, stop_time
        integer :: status

        allocate(A(2000,2000), stat=status)
        allocate(B(2000,2000), stat=status)
        allocate(C(2000,2000), stat=status)

        do n=100, 2000 , 100



            CALL RANDOM_NUMBER(A)
            CALL RANDOM_NUMBER(B)

            call cpu_time(start_time)

            !C = matmul(A, B)
            call mymatmul(A,B,C,n)


            call cpu_time(stop_time)

            write(*,*) "n = ",n
            
            write(*,*) "time:",  stop_time - start_time, "seconds"

        end do

        !write(*,*) "Matrix A ="
        !do i=1, n
        !write(*,*) A(i, :)
        !enddo

        !write(*,*) "Matrix B ="
        !do i=1, n
        !write(*,*) B(i, :)
        !enddo

        !write(*,*) "Matrix C ="
        !do i=1, n
        !write(*,*) C(i, :)
        !enddo

    end program


