CREATE FUNCTION fn_CalculateOverdueFees
    (@LoanID INT)
RETURNS MONEY
AS
BEGIN
    DECLARE @OverdueDays INT;
    DECLARE @OverdueFees MONEY;

    SELECT @OverdueDays = DATEDIFF(DAY, due_date, GETDATE())
    FROM loans
    WHERE id = @LoanID;

    IF @OverdueDays <= 0
        SET @OverdueFees = 0;
    ELSE IF @OverdueDays <= 30
        SET @OverdueFees = @OverdueDays * 1.0;
    ELSE
        SET @OverdueFees = (30 * 1.0) + ((@OverdueDays - 30) * 2.0);

    RETURN @OverdueFees;
END;

DECLARE @LoanID INT = 123;
SELECT dbo.fn_CalculateOverdueFees(@LoanID) AS OverdueFees;