CREATE PROCEDURE InsertCompleteAddressAndAddress
    @Id UNIQUEIDENTIFIER,
    @ZipCode NVARCHAR(8),
    @Street NVARCHAR(255),
    @City NVARCHAR(100),
    @State NVARCHAR(2),
    @Neighborhood NVARCHAR(100),
    @Complement NVARCHAR(255),
    @Number NVARCHAR(10)
AS
BEGIN
 
    IF NOT EXISTS (SELECT 1 FROM dbo.Address WHERE ZipCode = @ZipCode)
    BEGIN
        INSERT INTO dbo.Address (ZipCode, Street, City, State, Neighborhood)
        VALUES (@ZipCode, @Street, @City, @State, @Neighborhood);
    END

    INSERT INTO dbo.CompleteAddress (Id, ZipCode, Complement, Number)
    VALUES (@Id, @ZipCode, @Complement, @Number);

	SELECT ca.ZipCode, a.Street, a.Neighborhood, a.City, a.State, ca.Number, ca.Complement
	FROM CompleteAddress as ca 
		INNER JOIN Address as a
			ON ca.ZipCode = a.ZipCode
END