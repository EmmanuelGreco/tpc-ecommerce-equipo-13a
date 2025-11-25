USE master
GO

USE TPC_ECOMMERCE
GO

-- alter table productos 
-- ADD CONSTRAINT df_estado_producto DEFAULT 1 FOR Estado;

-- ALTER TABLE Productos
-- ALTER COLUMN Estado bit NOT NULL;



/*SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.IdMarca, M.Nombre Marca, 
P.IdCategoria, C.Nombre Categoria, P.Origen, Precio
FROM PRODUCTOS P, MARCAS M, CATEGORIAS C
WHERE M.Id = P.IdMarca AND C.Id = P.IdCategoria*/


--SELECT * FROM PRODUCTOS
--SELECT * FROM MARCAS
--SELECT * FROM CATEGORIAS


-- INSERT INTO PRODUCTOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Origen, Stock, Precio ) 
--                                        VALUES (1, 1, 1, 1, 1, 1, 1, 1);
--                                        SELECT SCOPE_IDENTITY() AS 'IdInsertado';
--SELECT * FROM IMAGENES

--DELETE FROM IMAGENES where IdProducto >= 10
--delete FROM PRODUCTOS where id >= 10

SELECT * FROM EMPLEADOS
SELECT * FROM USUARIOS
SELECT * FROM CLIENTES


/*SELECT C.Id,
		U.Documento,
		U.Nombre,
		U.Apellido,
		U.FechaNacimiento,
		U.Telefono,
		U.Direccion,
		U.CodigoPostal,
		U.Email,
		U.Contrasenia,
		U.FechaAlta,
		C.Activo 
FROM Clientes C INNER JOIN Usuarios U ON C.IdUsuario = U.Id*/

/*SELECT E.Id,
		E.Legajo,
		E.FechaIngreso,
		E.FechaDespido,
		U.Documento,
		U.Nombre,
		U.Apellido,
		U.FechaNacimiento,
		U.Telefono,
		U.Direccion,
		U.CodigoPostal,
		U.Email,
		U.Contrasenia,
		U.FechaAlta,
		E.Activo 
FROM Empleados E INNER JOIN Usuarios U ON E.IdUsuario = U.Id*/

--SELECT IdUsuario FROM CLIENTES WHERE Id = 2

/*UPDATE EMPLEADOS SET 
	Activo = CASE 
		WHEN Activo = 1 THEN 0 
		ELSE 1 
	END,
	FechaDespido = CASE 
		WHEN Activo = 1 THEN GETDATE()
		ELSE NULL
	END
WHERE Id = @IdEmpleado*/