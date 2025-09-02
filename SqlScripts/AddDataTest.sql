USE MillionRealEstateDB;
GO

INSERT INTO Users (Username, PasswordHash, Role) VALUES
('juanperez', 'hashed_password1', 'Agent'),
('mariagomez', 'hashed_password2', 'Agent');

INSERT INTO Owners (Name, Address, Birthday, Photo) VALUES
('Carlos Rodríguez', 'Cra 7 #45-23, Bogotá', '1980-05-12', 'https://example.com/photos/carlos.jpg'),
('María Fernanda López', 'Av. El Poblado #12-34, Medellín', '1975-09-23', 'https://example.com/photos/maria.jpg'),
('Jorge Martínez', 'Calle 15 #20-11, Cali', '1990-02-14', NULL),
('Ana Torres', 'Carrera 50 #18-90, Barranquilla', '1988-11-30', 'https://example.com/photos/ana.jpg');

INSERT INTO Properties (Address, City, Price, YearBuilt, OwnerId) VALUES
('Calle 100 #15-30', 'Bogotá', 650000000, 2010, 1),
('Carrera 70 #45-10', 'Medellín', 420000000, 2015, 2),
('Av. Roosevelt #25-50', 'Cali', 310000000, 2008, 3),
('Calle 84 #50-20', 'Barranquilla', 550000000, 2012, 4);

INSERT INTO PropertyImages (PropertyId, Url, IsPrimary) VALUES
(1, 'https://example.com/properties/bogota1_front.jpg', 1),
(1, 'https://example.com/properties/bogota1_interior.jpg', 0),
(3, 'https://example.com/properties/cali1_front.jpg', 1);


INSERT INTO PropertyTraces (PropertyId, DateSale, Name, Value, Tax) VALUES
(1, '2020-03-15', 'Carlos Rodríguez', 600000000, 48000000),
(3, '2019-11-10', 'Jorge Martínez', 290000000, 23200000);
