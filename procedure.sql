CREATE PROCEDURE InsertarVenta
    @Fecha DATE,
    @ClienteID INT,
    @VendedorID INT,
    @Productos VentaProductoType READONLY 
AS
BEGIN
    DECLARE @FacturaID INT;
    DECLARE @Total DECIMAL(10, 2) = 0;
    DECLARE @ProductoID INT, @Cantidad INT, @PrecioUnitario DECIMAL(10, 2);

 
    DECLARE productos_cursor CURSOR FOR
    SELECT ProductoID, Cantidad, PrecioUnitario FROM @Productos;

  
    OPEN productos_cursor;

    FETCH NEXT FROM productos_cursor INTO @ProductoID, @Cantidad, @PrecioUnitario;

   
    WHILE @@FETCH_STATUS = 0
    BEGIN
       
        SET @Total = @Total + (@Cantidad * @PrecioUnitario);

        FETCH NEXT FROM productos_cursor INTO @ProductoID, @Cantidad, @PrecioUnitario;
    END;

   
    CLOSE productos_cursor;
    DEALLOCATE productos_cursor;

    
    INSERT INTO Ventas (Fecha, ClienteID, VendedorID, Total)
    VALUES (@Fecha, @ClienteID, @VendedorID, @Total);

    SET @FacturaID = SCOPE_IDENTITY();

    INSERT INTO Venta_Producto (FacturaID, ProductoID, Cantidad, PrecioUnitario)
    SELECT @FacturaID, ProductoID, Cantidad, PrecioUnitario FROM @Productos;
END;



go

CREATE PROCEDURE ReporteVentasPorVendedor
AS
BEGIN
    SELECT V.VendedorID, V.Nombre, SUM(VD.Importe) AS TotalVentas
    FROM Vendedor V
    JOIN Ventas VE ON V.VendedorID = VE.VendedorID
    JOIN Venta_Producto VD ON VE.FacturaID = VD.FacturaID
    GROUP BY V.VendedorID, V.Nombre;
END;

go

use SharkDB

CREATE PROCEDURE ReporteVentasPorRegion
AS
BEGIN
    SELECT R.RegionID, R.Nombre, SUM(VD.Importe) AS TotalVentas
    FROM Región R
    JOIN Vendedor V ON R.RegionID = V.RegionID
    JOIN Ventas VE ON V.VendedorID = VE.VendedorID
    JOIN Venta_Producto VD ON VE.FacturaID = VD.FacturaID
    GROUP BY R.RegionID, R.Nombre;
END;

go

DROP PROCEDURE IF EXISTS InsertarVenta;

go

CREATE PROCEDURE ReporteVentasPorEstado
AS
BEGIN
    SELECT 
        E.EstadoID, 
        E.Nombre AS EstadoNombre, 
        SUM(VP.Importe) AS TotalVentas
    FROM 
        Estado E
    JOIN Municipio M ON E.EstadoID = M.EstadoID
    JOIN Cliente C ON M.MunicipioID = C.MunicipioID
    JOIN Ventas VE ON C.ClienteID = VE.ClienteID
    JOIN Venta_Producto VP ON VE.FacturaID = VP.FacturaID
    GROUP BY 
        E.EstadoID, E.Nombre
    ORDER BY 
        TotalVentas DESC;
END;

go 

CREATE PROCEDURE ReporteVentasPorMunicipio
AS
BEGIN
    SELECT 
        M.MunicipioID, 
        M.Nombre AS MunicipioNombre, 
        SUM(VP.Importe) AS TotalVentas
    FROM 
        Municipio M
    JOIN 
        Cliente C ON M.MunicipioID = C.MunicipioID
    JOIN 
        Ventas VE ON C.ClienteID = VE.ClienteID
    JOIN 
        Venta_Producto VP ON VE.FacturaID = VP.FacturaID
    GROUP BY 
        M.MunicipioID, M.Nombre
    ORDER BY 
        TotalVentas DESC;
END;
