CREATE TABLE books (
    id INT PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    author VARCHAR(255) NOT NULL,
    isbn VARCHAR(255) NOT NULL UNIQUE,
    published_date DATE NOT NULL,
    genre VARCHAR(255) NOT NULL,
    shelf_location INT NOT NULL,
    status VARCHAR(255) NOT NULL
);
CREATE INDEX books_published_date_index ON books(published_date);

CREATE TABLE borrower (
    id INT PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    date_of_birth DATE NOT NULL,
    membership_date DATE NOT NULL
);
CREATE INDEX borrower_date_of_birth_index ON borrower(date_of_birth);
CREATE INDEX borrower_membership_date_index ON borrower(membership_date);

CREATE TABLE loans (
    id INT PRIMARY KEY,
    book_id INT NOT NULL,
    borrow_id INT NOT NULL,
    borrowed_date DATE NOT NULL,
    due_date DATE NOT NULL,
    returned_date DATE NULL,
    FOREIGN KEY (book_id) REFERENCES books(id),
    FOREIGN KEY (borrow_id) REFERENCES borrower(id)
);
CREATE INDEX loans_book_id_borrow_id_index ON loans(book_id, borrow_id);

CREATE TABLE audit_log (
    id INT PRIMARY KEY IDENTITY,
    book_id INT NOT NULL,
    status_changed VARCHAR(255) NOT NULL,
    changed_date DATE NOT NULL,
	FOREIGN KEY (book_id) REFERENCES books(id)
);
CREATE INDEX audit_log_book_id_index ON audit_log(book_id);
CREATE INDEX audit_log_changed_date_index ON audit_log(changed_date);
