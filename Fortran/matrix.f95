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
        integer :: status

        n = 2000

        !read(*,*)n


        allocate(A(n,n), stat=status)
        allocate(B(n,n), stat=status)
        allocate(C(n,n), stat=status)


        CALL RANDOM_NUMBER(A)
        CALL RANDOM_NUMBER(B)


        !C = matmul(A, B)
        call mymatmul(A,B,C,n)


        write(*,*) "n = ",n


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


