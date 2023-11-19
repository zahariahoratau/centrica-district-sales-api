INSERT INTO Management.Salesperson ([FirstName], [LastName], [BirthDate], [HireDate], [Email], [PhoneNumber])
OUTPUT INSERTED.[Id]
VALUES (@FirstName, @LastName, @BirthDate, @HireDate, @Email, @PhoneNumber)
