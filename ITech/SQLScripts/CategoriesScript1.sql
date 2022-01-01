
SET IDENTITY_INSERT dbo.Categories ON
INSERT INTO dbo.Categories(Id, Name, Description) VALUES (1,'Phones','This category sells phones');
INSERT INTO dbo.Categories(Id, Name, Description) VALUES (2,'Laptops','This category sells Laptops');
SET IDENTITY_INSERT dbo.Categories OFF