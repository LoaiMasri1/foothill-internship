
CREATE PROCEDURE sp_AddNewBorrower4
	@Id INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @DateOfBirth DATE,
    @MembershipDate DATE
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM borrower WHERE email = @Email)
    BEGIN
        INSERT INTO borrower (id,first_name, last_name, email, date_of_birth, membership_date)
        VALUES (@Id,@FirstName, @LastName, @Email, @DateOfBirth, @MembershipDate);
        SELECT SCOPE_IDENTITY() AS NewBorrowerID;
    END
    ELSE
    BEGIN
        SELECT 'Email already exists' AS ErrorMessage;
    END
END;


sp_AddNewBorrower4 1001,'John', 'Doe', 'john.doe@example.com', '1990-01-01', '2023-08-01';
sp_AddNewBorrower4 1001,'John', 'Doe', 'john.doe@example.com', '1990-01-01', '2023-08-01';
-- will return 'Email already exists'