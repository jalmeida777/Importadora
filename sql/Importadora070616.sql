CREATE TABLE [dbo].[OrdenCompraDocumento](
	[i_IdOrdenCompra] [int] NULL,
	[n_IdAlmacen] [numeric](10, 0) NULL,
	[n_IdTipoDocumento] [numeric](10, 0) NULL,
	[v_NumeroDocumento] [varchar](10) COLLATE Modern_Spanish_CI_AS NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [Importadora]
GO
ALTER TABLE [dbo].[OrdenCompraDocumento]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompraDocumento_Almacen] FOREIGN KEY([n_IdAlmacen])
REFERENCES [dbo].[Almacen] ([n_IdAlmacen])
GO
ALTER TABLE [dbo].[OrdenCompraDocumento]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompraDocumento_OrdenCompra] FOREIGN KEY([i_IdOrdenCompra])
REFERENCES [dbo].[OrdenCompra] ([i_IdOrdenCompra])
GO
ALTER TABLE [dbo].[OrdenCompraDocumento]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompraDocumento_TipoDocumento] FOREIGN KEY([n_IdTipoDocumento])
REFERENCES [dbo].[TipoDocumento] ([n_IdTipoDocumento])
go

alter procedure Play_OrdenCompraDetalle_Seleccionar  
@i_IdOrdenCompra int  
as  
select i_IdOrdenCompra,i_Cantidad as 'Cantidad',ocd.n_IdProducto,  
f_CostoUnidad as 'CostoUnitario',f_CostoTotal as 'CostoTotal',  
pro.v_Descripcion as 'Producto',i_Saldo as 'Saldo'  
from OrdenCompraDetalle ocd inner join Producto pro on ocd.n_IdProducto = pro.n_IdProducto  
where i_IdOrdenCompra = @i_IdOrdenCompra
go

alter procedure Play_OrdenCompra_Actualizar
@i_IdOrdenCompra int,
@n_IdProveedor numeric(10,0),
@d_FechaEmision datetime,
@v_Referencia varchar(20),
@t_Observacion text,
@f_SubTotal float,
@f_IGV float,
@f_Total float,
@v_RutaArchivo varchar(1000)
as
update OrdenCompra 
set n_IdProveedor = @n_IdProveedor,
d_FechaEmision = @d_FechaEmision,
v_Referencia = @v_Referencia,
t_Observacion = @t_Observacion,
f_SubTotal = @f_SubTotal,
f_IGV = @f_IGV,
f_Total = @f_Total,
v_RutaArchivo = @v_RutaArchivo
where i_IdOrdenCompra = @i_IdOrdenCompra
go

create procedure Play_OrdenCompraDetalle_Eliminar
@i_IdOrdenCompra int
as
delete OrdenCompraDetalle where i_IdOrdenCompra = @i_IdOrdenCompra
go

create procedure Play_OrdenCompra_Actualizar_Archivo
@i_IdOrdenCompra int,
@v_RutaArchivo varchar(1000)
as
update OrdenCompra set v_RutaArchivo = @v_RutaArchivo 
where i_IdOrdenCompra = @i_IdOrdenCompra
go

alter procedure Play_OrdenCompraDetalle_SeleccionarSaldos   
@i_IdOrdenCompra int    
as    
select 
ocd.n_IdProducto,    
pro.v_Descripcion as 'Producto',
i_Saldo as 'Cantidad'    
from OrdenCompraDetalle ocd inner join Producto pro on ocd.n_IdProducto = pro.n_IdProducto    
where i_IdOrdenCompra = @i_IdOrdenCompra  
and i_Saldo > 0
go

create procedure Play_OrdenCompraDetalle_ActualizarSaldo
@i_IdOrdenCompra int,
@n_IdProducto int,
@i_Cantidad int
as
update OrdenCompraDetalle set i_Saldo = i_Saldo - @i_Cantidad
where n_IdProducto = @n_IdProducto
and i_IdOrdenCompra = @i_IdOrdenCompra
go

create procedure Play_OrdenCompraDocumento_Insertar
@i_IdOrdenCompra int,
@n_IdAlmacen numeric(10,0),
@n_IdTipoDocumento numeric(10,0),
@v_NumeroDocumento varchar(10)
as
insert into OrdenCompraDocumento
values (@i_IdOrdenCompra,@n_IdAlmacen,
@n_IdTipoDocumento,@v_NumeroDocumento)
go

create procedure Play_OrdenCompra_CambiarEstado
@i_IdOrdenCompra int
as
declare @Cantidad int
declare @Saldo int

set @Cantidad = (select isnull(sum(i_Cantidad),0) as 'Cantidad' from ordencompradetalle where i_idOrdenCompra = @i_IdOrdenCompra)
set @Saldo = (select isnull(sum(i_Saldo),0) as 'Saldo' from ordencompradetalle where i_idOrdenCompra = @i_IdOrdenCompra)

if @Saldo = 0
begin
update OrdenCompra set i_IdOrdenCompraEstado = 3 where i_idOrdenCompra = @i_IdOrdenCompra --Recibido Total
end
else if  @Saldo < @Cantidad
begin
update OrdenCompra set i_IdOrdenCompraEstado = 2 where i_idOrdenCompra = @i_IdOrdenCompra --Recibido Parcial
end
go

select * from OrdenCompraDocumento