CREATE TABLE audit_log (
    id INT PRIMARY KEY IDENTITY,
    book_id INT,
    status_changed NVARCHAR(50),
    change_date DATE,
    FOREIGN KEY (book_id) REFERENCES books(id)
);

CREATE TRIGGER tr_BookStatusAudit
ON books
AFTER UPDATE
AS
BEGIN
    DECLARE @OldStatus NVARCHAR(50);
    DECLARE @NewStatus NVARCHAR(50);
    DECLARE @BookID INT;

    SELECT @OldStatus = status, @BookID = id FROM deleted;
    SELECT @NewStatus = status FROM inserted;

    IF @OldStatus <> @NewStatus
    BEGIN
        INSERT INTO audit_log(book_id, status_changed, change_date)
        VALUES (@BookID, 'Status changed from ' + @OldStatus + ' to ' + @NewStatus, GETDATE());
    END;
END;

UPDATE books 
SET books.status = 'Borrowed'
WHERE id=6;