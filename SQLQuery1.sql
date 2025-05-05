CREATE TABLE ContactData (
    Id INT PRIMARY KEY IDENTITY(1,1),    -- Auto-incremented primary key
    Name NVARCHAR(69) NOT NULL,          -- Name column (required)
    Email NVARCHAR(69) NOT NULL,         -- Email column (required)
    Subject NVARCHAR(30) NOT NULL,       -- Subject column (required)
    Message NVARCHAR(MAX) NOT NULL,       -- Message column (required)
    CreatedAt DATETIME DEFAULT GETDATE()  -- Timestamp for when the record is created
);
