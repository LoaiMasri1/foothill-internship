SELECT
    b.author,
    COUNT(l.id) AS BorrowingFrequency,
    RANK() OVER (ORDER BY COUNT(l.id) DESC) AS AuthorRank
FROM books b
JOIN loans l ON b.id = l.book_id
GROUP BY b.author
ORDER BY AuthorRank;