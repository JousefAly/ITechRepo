
SET IDENTITY_INSERT dbo.Categories ON
INSERT INTO dbo.Categories(Id, Name, Description,ImageUrl) VALUES (1,'Phones','This category sells phones','img/PhonesCategory.jpg');
INSERT INTO dbo.Categories(Id, Name, Description, ImageUrl) VALUES (2,'Laptops','This category sells Laptops','img/LaptopsCategory.jpg');
SET IDENTITY_INSERT dbo.Categories OFF