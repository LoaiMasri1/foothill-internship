CREATE PROCEDURE sp_BorrowedBooksReport
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT
        b.Title AS BookTitle,
        br.first_name + ' ' + br.last_name AS BorrowerName,
        l.borrow_date
    FROM books b
    JOIN loans l ON b.id = l.book_id
    JOIN borrower br ON l.borrower_id = br.id
    WHERE l.borrow_date BETWEEN @StartDate AND @EndDate;

END;

sp_BorrowedBooksReport '2023-01-01', '2023-08-31';