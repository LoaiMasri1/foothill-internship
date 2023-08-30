CREATE FUNCTION fn_BookBorrowingFrequency
    (@BookID INT)
RETURNS INT
AS
BEGIN
    DECLARE @BorrowingCount INT;

    SELECT @BorrowingCount = COUNT(*)
    FROM loans
    WHERE book_id = @BookID;

    RETURN @BorrowingCount;
END;

DECLARE @BookID INT = 1000;
SELECT dbo.fn_BookBorrowingFrequency(@BookID) AS BorrowingCount;