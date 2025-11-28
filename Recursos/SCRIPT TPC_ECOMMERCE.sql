USE master
GO

ALTER DATABASE TPC_ECOMMERCE SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE TPC_ECOMMERCE;

CREATE DATABASE TPC_ECOMMERCE
GO

USE TPC_ECOMMERCE
GO

CREATE TABLE [dbo].[CATEGORIAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Activo] [bit] NOT NULL DEFAULT 1,
 CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[MARCAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Activo] [bit] NOT NULL DEFAULT 1,
 CONSTRAINT [PK_MARCAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[PRODUCTOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](150) NULL,
	[IdMarca] [int] NULL,
	[IdCategoria] [int] NULL,
    [Origen] [varchar](50) NULL,
	[Precio] [money] NULL,
	[Stock] [int] NOT NULL DEFAULT 0,
    [Activo] [bit] NOT NULL DEFAULT 1,
 CONSTRAINT [PK_PRODUCTOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PRODUCTOS]  WITH CHECK ADD CONSTRAINT [FK_PRODUCTOS_CATEGORIAS] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[CATEGORIAS] ([Id])
GO

ALTER TABLE [dbo].[PRODUCTOS] CHECK CONSTRAINT [FK_PRODUCTOS_CATEGORIAS]
GO

ALTER TABLE [dbo].[PRODUCTOS]  WITH CHECK ADD CONSTRAINT [FK_PRODUCTOS_MARCAS] FOREIGN KEY([IdMarca])
REFERENCES [dbo].[MARCAS] ([Id])
GO

ALTER TABLE [dbo].[PRODUCTOS] CHECK CONSTRAINT [FK_PRODUCTOS_MARCAS]
GO


CREATE TABLE [dbo].[IMAGENES](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProducto] [int] NOT NULL,
	[ImagenUrl] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_IMAGENES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[IMAGENES]  WITH CHECK ADD CONSTRAINT [FK_IMAGENES_PRODUCTOS] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[PRODUCTOS] ([Id])
GO

ALTER TABLE [dbo].[IMAGENES] CHECK CONSTRAINT [FK_IMAGENES_PRODUCTOS]
GO


CREATE TABLE [dbo].[USUARIOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Documento] [varchar](8) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Telefono] [bigint] NOT NULL,
	[Direccion] [varchar](100) NOT NULL,
	[CodigoPostal] [varchar](20) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Contrasenia] [varchar](20) NOT NULL,
	[TipoUsuario] [tinyint] NOT NULL DEFAULT 0,
	[FechaAlta] [datetime] NOT NULL DEFAULT GETDATE(),
 CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- TipoUsuario
/*
CLIENTE = 0,
EMPLEADO = 1,
ADMIN = 2
*/


CREATE TABLE [dbo].[CLIENTES](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Activo] [bit] NOT NULL DEFAULT 1,
 CONSTRAINT [PK_CLIENTES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLIENTES]  WITH CHECK ADD CONSTRAINT [FK_CLIENTES_USUARIOS] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIOS] ([Id])
GO

ALTER TABLE [dbo].[CLIENTES] CHECK CONSTRAINT [FK_CLIENTES_USUARIOS]
GO


-- Secuencia creada ya que no se puede tener dos identity en la misma tabla. Legajo comienza en 1000.
CREATE SEQUENCE [dbo].[SEQ_Legajo]
    START WITH 1
    INCREMENT BY 1;

CREATE TABLE [dbo].[EMPLEADOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Legajo] [int] NOT NULL DEFAULT NEXT VALUE FOR [dbo].[SEQ_Legajo],
	[FechaIngreso] [date] NOT NULL,
	[FechaDespido] [date] NULL,
	[Activo] [bit] NOT NULL DEFAULT 1,
 CONSTRAINT UQ_EMPLEADOS_Legajo UNIQUE (Legajo),
 CONSTRAINT [PK_EMPLEADOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMPLEADOS]  WITH CHECK ADD CONSTRAINT [FK_EMPLEADOS_USUARIOS] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIOS] ([Id])
GO

ALTER TABLE [dbo].[EMPLEADOS] CHECK CONSTRAINT [FK_EMPLEADOS_USUARIOS]
GO


CREATE TABLE [dbo].[VENTAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[MetodoPago] [tinyint] NOT NULL,
	[MetodoEnvio] [tinyint] NOT NULL,
	[FechaHoraVenta] [datetime] NOT NULL,
	[FechaHoraEntrega] [datetime] NULL,
	[EstadoPedido] [tinyint] NOT NULL DEFAULT 0,
	[MontoTotal] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_VENTAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (
	PAD_INDEX = OFF, 
	STATISTICS_NORECOMPUTE = OFF, 
	IGNORE_DUP_KEY = OFF, 
	ALLOW_ROW_LOCKS = ON, 
	ALLOW_PAGE_LOCKS = ON, 
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VENTAS]  WITH CHECK ADD  CONSTRAINT [FK_VENTAS_USUARIOS] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIOS] ([Id])
GO

ALTER TABLE [dbo].[VENTAS] CHECK CONSTRAINT [FK_VENTAS_USUARIOS]
GO


CREATE TABLE [dbo].[VENTAS_PRODUCTOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdVenta] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_VENTAS_PRODUCTOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (
	PAD_INDEX = OFF, 
	STATISTICS_NORECOMPUTE = OFF, 
	IGNORE_DUP_KEY = OFF, 
	ALLOW_ROW_LOCKS = ON, 
	ALLOW_PAGE_LOCKS = ON, 
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VENTAS_PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_VENTAS_PRODUCTOS_VENTAS] FOREIGN KEY([IdVenta])
REFERENCES [dbo].[VENTAS] ([Id])
GO

ALTER TABLE [dbo].[VENTAS_PRODUCTOS] CHECK CONSTRAINT [FK_VENTAS_PRODUCTOS_VENTAS]
GO

ALTER TABLE [dbo].[VENTAS_PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_VENTAS_PRODUCTOS_PRODUCTOS] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[PRODUCTOS] ([Id])
GO

ALTER TABLE [dbo].[VENTAS_PRODUCTOS] CHECK CONSTRAINT [FK_VENTAS_PRODUCTOS_PRODUCTOS]
GO




INSERT INTO MARCAS (Nombre) VALUES ('Wilson'), ('Logitech'), ('Royal Kludge'), ('Lenovo'), ('Samsung'), ('Sony'), ('LG'), ('Dell'), ('Asus')
INSERT INTO CATEGORIAS (Nombre) VALUES ('Mochilas'),('Periféricos'), ('Accesorios'), ('Televisores'), ('Notebooks')
INSERT INTO PRODUCTOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Origen, Precio, Stock) VALUES
('M01', 'Mochila Porta Notebook', 'Esta mochila combina un diseño elegante y profesional con la robustez necesaria para enfrentar el ajetreo urbano y los viajes de negocios.', 1, 1, 'China', 49999, 15),
('P03', 'Mouse Gamer Hero G502', 'Sumérgete en el mundo de los videojuegos con el mouse gamer Logitech G Series Hero G502 en color negro', 2, 2, 'Corea', 64999, 2),
('P08', 'Teclado Mecánico 75% Rk M75', 'Este teclado cuenta con un diseño compacto con 81 teclas, por lo que es fácil de transportar y usar en cualquier lugar.', 2, 3, 'Japon', 185000, 10),
('65BRAVIA8II', 'Televisor 65" BRAVIA 8 OLED 4K', 'Televisor OLED 4K con tecnología QD-OLED', 6, 4, 'Japon', 6399000, 100),
('65X855', 'Televisor 65" Serie X855 4K', 'Este televisor cuenta con resolucion 4K y Triluminos Display', 6, 4, 'Japon', 2300000, 0)

INSERT INTO imagenes VALUES
(1, 'https://http2.mlstatic.com/D_NQ_NP_703368-MLU76300898146_052024-O.webp'),
(1, 'https://http2.mlstatic.com/D_NQ_NP_842545-MLU76300482840_052024-O.webp'),
(1, 'https://http2.mlstatic.com/D_NQ_NP_747302-MLU76300769244_052024-O.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_701613-MLA45464844877_042021-O.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_987670-MLA45464844880_042021-O.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_793119-MLU72761228270_112023-O.webp'),
(3, 'https://http2.mlstatic.com/D_NQ_NP_767460-MLA74282172500_022024-O.webp'),
(3, 'https://http2.mlstatic.com/D_NQ_NP_848157-MLA74517144673_022024-O.webp'),
(3, 'https://http2.mlstatic.com/D_NQ_NP_616027-MLA74397845971_022024-O.webp'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_1_FrontWithStand_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_2_CW_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_3_CCW_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_5_Bezel_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_6_Stand_M?$productIntroPlatemobile$&fmt=png-alpha'),
(4, 'https://sony.scene7.com/is/image/sonyglobalsolutions/TVFY24_UM_0_insitu_M?$productIntroPlatemobile$&fmt=png-alpha'),
(5, 'https://http2.mlstatic.com/D_NQ_NP_2X_755172-MEC92794030205_092025-T.webp')


INSERT INTO Usuarios (Documento, Nombre, Apellido, FechaNacimiento, Telefono, Direccion, CodigoPostal, Email, Contrasenia, TipoUsuario, FechaAlta) VALUES
('44902784', 'Carlos', 'Perez', '2001-10-11', 1149283928, 'Av. Rivadavia 1138', 'C1398', 'carlitosp@gmail.com', 'carlitos123', 0, '2025-11-11 14:39:22'),
('23919203', 'Jose', 'Gonzalez', '1980-08-07', 1139282950, 'Av. La Plata 132', 'C1402', 'JoseElG@gmail.com', 'josejose', 1, '2023-04-10 12:28:34'),
('28965413', 'Roberto', 'Gomez', '1986-08-07', 1142357298, 'Centenera 2238', 'C1425', 'RobertoG@gmail.com', 'robertoboca', 2, '2025-03-08 15:43:51'),
('34559843', 'Maria', 'Hernandez', '1990-08-07', 1154723582, 'Triunvirato 4253', 'C1408', 'MariaH@gmail.com', 'maria123', 1, '2024-12-12 10:12:40'),
('44902784', 'Juan', 'Gallardo', '2004-10-11', 1154378298, 'Av. Argentina 3468', 'C1440', 'jgallardo@gmail.com', 'juan123', 0, '2023-11-11 20:25:36')

INSERT INTO Clientes (IdUsuario) SELECT Id FROM Usuarios WHERE TipoUsuario = 0;

INSERT INTO Empleados (idUsuario, Legajo, FechaIngreso) VALUES (2, 1002, '2024-10-10')
INSERT INTO Empleados (idUsuario, Legajo, FechaIngreso) VALUES (3, 1003, '2024-05-02')
INSERT INTO Empleados (idUsuario, Legajo, FechaIngreso, FechaDespido) VALUES (4, 1004, '2024-10-10', '2025-12-12')
