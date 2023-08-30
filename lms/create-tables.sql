CREATE TABLE borrower(
    "id" INT PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    date_of_birth DATE NOT NULL,
	membership_date DATE NOT NULL
);

CREATE TABLE books(
    id INT PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    author VARCHAR(255) NOT NULL,
    isbn VARCHAR(255) NOT NULL UNIQUE,
    published_date DATE NOT NULL,
    genre VARCHAR(255) NOT NULL,
    shelf_location VARCHAR(255) NOT NULL,
    status VARCHAR(255) NOT NULL
);

CREATE TABLE loans(
    id INT PRIMARY KEY,
    book_id INT NOT NULL,
    borrower_id INT NOT NULL,
    borrowed_date DATE NOT NULL,
    due_date DATE NOT NULL,
    returned_date DATE NULL,
	FOREIGN KEY (book_id) REFERENCES books(id),
	FOREIGN KEY (borrower_id) REFERENCES borrower(id)
);

CREATE TABLE audit_log (
    id INT PRIMARY KEY IDENTITY,
    book_id INT NOT NULL,
    status_changed NVARCHAR(50) NOT NULL,
    change_date DATE NOT NULL,
    FOREIGN KEY (book_id) REFERENCES books(id)
);