CREATE PROCEDURE sp_OverdueBooksReport
AS
BEGIN
    CREATE TABLE #OverdueBorrowers (
        borrower_id INT PRIMARY KEY
    );

    INSERT INTO #OverdueBorrowers (borrower_id)
    SELECT DISTINCT l.borrower_id
    FROM Loans l
    WHERE l.returned_date IS NULL
        AND DATEDIFF(DAY, l.due_date, GETDATE()) > 30;

    SELECT
        ob.borrower_id,
        br.first_name + ' ' + br.last_name AS BorrowerName,
        b.Title AS BookTitle,
        l.borrow_date,
        l.due_date,
        DATEDIFF(DAY, l.due_date, GETDATE()) AS OverdueDays
    FROM #OverdueBorrowers ob
    JOIN loans l ON ob.borrower_id = l.borrower_id
    JOIN books b ON l.book_id = b.id
    JOIN borrower br ON ob.borrower_id = br.id;

    DROP TABLE #OverdueBorrowers;
END;

sp_OverdueBooksReport