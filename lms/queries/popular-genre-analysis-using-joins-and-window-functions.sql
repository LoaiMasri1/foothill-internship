DECLARE @borrowMonth AS INT=8;

WITH MonthlyGenreCounts AS (
    SELECT
        DATEPART(YEAR, l.borrow_date) AS borrow_year,
        DATEPART(MONTH, l.borrow_date) AS borrow_month,
        b.genre,
        COUNT(*) AS genre_count,
        RANK() OVER (PARTITION BY DATEPART(YEAR, l.borrow_date), DATEPART(MONTH, l.borrow_date) ORDER BY COUNT(*) DESC) AS genre_rank
    FROM loans l
    JOIN books b ON l.book_id = b.id
    GROUP BY DATEPART(YEAR, l.borrow_date), DATEPART(MONTH, l.borrow_date), b.genre
)
SELECT
    borrow_year,
    borrow_month,
    genre,
    genre_count
FROM MonthlyGenreCounts
WHERE genre_rank = 1 AND borrow_month = @borrowMonth;