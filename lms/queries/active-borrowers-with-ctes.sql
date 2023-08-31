insert into loans (id, book_id, borrower_id, borrow_date, due_date, returned_date) values (1004, 255, 1000, '1/26/2023', '1/21/2023', NULL);
insert into loans (id, book_id, borrower_id, borrow_date, due_date, returned_date) values (1005, 251, 1000, '7/26/2023', '9/20/2022', NULL);
insert into loans (id, book_id, borrower_id, borrow_date, due_date, returned_date) values (1006, 200, 1000, '5/3/2023','3/10/2023', NULL);

WITH BorrowerLoanCounts AS (
    SELECT
        l.borrower_id,
        COUNT(*) AS BorrowedCount
    FROM loans l
    WHERE l.returned_date IS NULL
    GROUP BY l.borrower_id
)
SELECT b.id, b.first_name, b.last_name, b.email
FROM borrower b
INNER JOIN BorrowerLoanCounts blc ON b.id = blc.borrower_id
WHERE blc.BorrowedCount >= 2;