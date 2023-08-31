DECLARE @borrowerId AS INT=250

SELECT b.title, b.author, l.borrow_date, l.due_date
FROM books b
INNER JOIN loans l ON b.id = l.book_id
WHERE l.borrower_id = @borrowerId;