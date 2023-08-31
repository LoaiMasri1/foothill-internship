WITH AgeGroups AS (
    SELECT
        id as borrower_id,
        CASE
            WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 0 AND 10 THEN '0-10'
            WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 11 AND 20 THEN '11-20'
            WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 21 AND 30 THEN '21-30'
            -- Add more age groups here
            ELSE '31+'
        END AS AgeGroup
    FROM borrower
)
SELECT
    ag.AgeGroup,
    b.Genre,
    COUNT(DISTINCT l.borrower_id) AS BorrowerCount
FROM AgeGroups ag
JOIN Loans l ON ag.borrower_id = l.borrower_id
JOIN Books b ON l.book_id = b.id
GROUP BY ag.AgeGroup, b.Genre
HAVING COUNT(DISTINCT l.borrower_id) > 0
ORDER BY ag.AgeGroup, BorrowerCount DESC;
