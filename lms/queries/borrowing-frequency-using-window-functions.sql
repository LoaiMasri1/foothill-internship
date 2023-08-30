SELECT
    b.id,
    b.first_name,
    b.last_name,
    COUNT(l.id) AS BorrowingCount,
    RANK() OVER (ORDER BY COUNT(l.id) DESC) AS BorrowingRank
FROM borrower b
LEFT JOIN Loans l ON b.id = l.borrower_id
GROUP BY b.id, b.first_name, b.last_name
ORDER BY BorrowingRank;