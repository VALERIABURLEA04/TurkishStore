DELETE AdminData;

SELECT * FROM AdminData;

DBCC CHECKIDENT ('AdminData', RESEED, 0); 

INSERT INTO AdminData (Username, PasswordHash)
VALUES ('username', CONVERT(VARCHAR(255), HASHBYTES('SHA2_256', 'password'), 2)); 