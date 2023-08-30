DECLARE @OverdueDays INT=30;

SELECT
    b.id,
    b.title,
    l.borrower_id,
    bl.first_name + ' ' + bl.last_name AS BorrowerName,
    l.borrow_date,
    l.due_date,
    DATEDIFF(DAY, l.due_date, GETDATE()) AS OverdueDays
FROM books b
JOIN loans l ON b.id = l.book_id
JOIN borrower bl ON l.borrower_id = bl.id
WHERE l.returned_date IS NULL
    AND DATEDIFF(DAY, l.due_date, GETDATE()) > 30;