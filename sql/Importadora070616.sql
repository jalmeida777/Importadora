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

create procedure Play_OrdenCompra_Actualizar
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

create procedure Play_OrdenCompraDetalle_SeleccionarSaldos   
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

SP_HELPTEXT Play_OrdenCompra_Registrar

  
ALTER procedure Play_OrdenCompra_Registrar    
@n_IdProveedor numeric(10,0),    
@n_IdMoneda numeric(10,0),    
@d_FechaEmision datetime,    
@v_Referencia varchar(20),    
@t_Observacion text,    
@f_SubTotal float,    
@f_IGV float,    
@f_Total float,    
@n_IdUsuarioCreacion numeric(10,0),
@v_RutaArchivo VARCHAR(1000)
as    
declare @v_NumeroOrdenCompra as varchar(10)    
declare @i_IdOrdenCompra as int      
declare @Final int      
declare @Actual int      
    
set @Final = (select convert(int,v_CorrelativoFinal) from Correlativo where n_IdTipoDocumento = 10)      
set @Actual = (select convert(int,v_CorrelativoActual) from Correlativo where n_IdTipoDocumento = 10)      
    
if(@Actual < @Final)      
begin      
 set @i_IdOrdenCompra = (select isnull(max(i_IdOrdenCompra),0) from OrdenCompra) + 1      
 set @v_NumeroOrdenCompra = (select c_Serie+'-'+v_CorrelativoActual from Correlativo where n_IdTipoDocumento = 10)      
    
insert into OrdenCompra(i_IdOrdenCompra,n_IdProveedor,n_IdMoneda,d_FechaEmision,  
v_NumeroOrdenCompra,v_Referencia,t_Observacion,f_SubTotal,f_IGV,f_Total,  
n_IdUsuarioCreacion,d_FechaCreacion,i_IdOrdenCompraEstado,v_RutaArchivo)    
values(@i_IdOrdenCompra,@n_IdProveedor,@n_IdMoneda,@d_FechaEmision,  
@v_NumeroOrdenCompra,@v_Referencia,@t_Observacion,@f_SubTotal,@f_IGV,@f_Total,  
@n_IdUsuarioCreacion,getdate(),1,@v_RutaArchivo)    
    
end      
else      
begin      
 set @i_IdOrdenCompra = '0'      
end      
select @i_IdOrdenCompra      
GO


alter procedure Play_OrdenCompra_Seleccionar  
@i_IdOrdenCompra int  
as  
select oc.n_IdProveedor,n_IdMoneda,d_FechaEmision,v_NumeroOrdenCompra,v_Referencia,  
t_Observacion,f_SubTotal,f_IGV,f_Total,n_IdUsuarioCreacion,d_FechaCreacion,oc.i_IdOrdenCompraEstado,  
pro.v_Nombre as 'NombreProveedor',usu.v_Nombre as 'Usuario',oce.v_DescripcionEstado,usu.v_RutaFoto,v_RutaArchivo  
from OrdenCompra oc inner join Proveedor pro on oc.n_IdProveedor = pro.n_IdProveedor  
inner join Usuario usu on oc.n_IdUsuarioCreacion = usu.n_IdUsuario  
inner join OrdenCompraEstado oce on oce.i_IdOrdenCompraEstado = oc.i_IdOrdenCompraEstado  
where oc.i_IdOrdenCompra = @i_IdOrdenCompra  
go

alter procedure Play_CajaHistorica_SelectID --1,2016,4,28  
@n_IdAlmacen numeric(10,0),  
@año int,  
@mes int,  
@dia int  
as  
select isnull(i_IdCaja,0) as 'i_IdCaja' from CajaHistorica   
where n_IdAlmacen = @n_IdAlmacen   
and YEAR(d_Fecha)=@año and MONTH(d_Fecha)=@mes and DAY(d_Fecha)=@dia  
go

alter procedure Play_FlujoCaja_Listar 
@i_IdCaja int
as
select f_CajaInicial,f_IngresoAdicional,f_SalidaAdicional,f_TotalVenta,f_CajaFinal,f_CajaReal 
from CajaHistorica 
where i_IdCaja = @i_IdCaja
go

alter PROCEDURE Play_CajaMovimiento_Listar
@i_IdCaja int
as
select cm.i_IdCaja,cm.v_NroDocumento, 
td.v_Descripcion as 'TipoDocumento',
ltrim(right(convert(varchar(25),d_FechaMovimiento, 100), 7)) as d_FechaMovimiento ,
co.v_Descripcion as 'Concepto',
cm.f_Importe,
case cm.c_TipoMovimiento when 'E' then 'INGRESO' ELSE 'SALIDA' END AS 'c_TipoMovimiento',
usu.v_Usuario
from CajaMovimiento cm inner join TipoDocumento td 
on cm.n_IdTipoDocumento = td.n_IdTipoDocumento 
inner join CajaConcepto co on cm.i_IdConceptoCaja = co.i_IdCajaConcepto 
left join Usuario usu on cm.n_IdUsuario = usu.n_IdUsuario
where cm.i_IdCaja = @i_IdCaja
go

alter procedure Play_CajaHistorica_RegistrarCierre --100,1,2,1    
@f_CajaReal float,    
@n_IdUsuarioCierre numeric(10,0),  
@i_IdCaja int,  
@n_IdAlmacen numeric(10,0)
as                        
update dbo.CajaHistorica 
set f_CajaReal = @f_CajaReal, 
n_IdUsuarioCierre = @n_IdUsuarioCierre,
d_FechaCierre = GETDATE(),
b_Estado = 0
where i_IdCaja = @i_IdCaja and n_IdAlmacen = @n_IdAlmacen
go

sp_helptext Play_CajaHistorica_Registrar

alter procedure Play_Valida_Caja --1,2016,5,14  
@n_IdAlmacen numeric(10,0),  
@i_Año int,  
@i_Mes int,  
@i_Dia int  
as  
select count(1) as 'Existe' from CajaHistorica   
where n_IdAlmacen = @n_IdAlmacen  
and year(d_Fecha)=@i_Año  
and month(d_Fecha)=@i_Mes  
and day(d_Fecha)=@i_Dia  
and b_Estado = 1
go



create procedure Play_CajaHistorica_Registrar  --1,100,100,0,0,0,0,1    
@n_IdAlmacen numeric(10,0),                          
@f_CajaInicial float,    
@f_CajaFinal float,     
@n_IdUsuarioApertura numeric(10,0)    
       
         
as                        
declare @i_IdCaja int                                   
set @i_IdCaja = (select isnull(max(i_IdCaja),0) from CajaHistorica) + 1                        
insert into CajaHistorica             
(i_IdCaja ,n_IdAlmacen ,d_Fecha,f_CajaInicial,f_CajaFinal,f_CajaReal,f_IngresoAdicional,f_SalidaAdicional,f_TotalVenta,b_Estado,n_IdUsuarioApertura,d_FechaApertura)          
values(@i_IdCaja,@n_IdAlmacen,GETDATE(),@f_CajaInicial,@f_CajaFinal,0,0,0,0,1,@n_IdUsuarioApertura,GETDATE())    
    
select @i_IdCaja  
go
   
  
alter procedure Play_Pedido_CajaMovimiento  --1,100,100,0,0,0,0,1    
@n_IdAlmacen numeric(10,0),
@i_IdConceptoCaja int,  
@c_TipoMovimiento char(1),  
@f_Importe float,  
@v_NroDocumento varchar(20),
@n_IdUsuario numeric(10,0)
as                        

declare @i_IdCaja int
set @i_IdCaja = (select i_IdCaja from CajaHistorica where n_IdAlmacen = @n_IdAlmacen and year(d_Fecha)=year(getdate()) and month(d_Fecha) = month(getdate()) and day(d_Fecha)= day(getdate()))

--Obtener saldo actual de Caja Historica  
declare @saldo float  
set @saldo = (select f_CajaFinal from CajaHistorica where i_IdCaja = @i_IdCaja)  
  

--Registrar CajaMovimiento  
if @c_TipoMovimiento = 'E'  
begin  
set @saldo = @saldo + @f_Importe  
insert into CajaMovimiento values(@i_IdCaja,GETDATE(),@i_IdConceptoCaja,@f_Importe,'E',1,@v_NroDocumento,@n_IdUsuario)  
end  
else  
begin  
set @saldo = @saldo - @f_Importe  
insert into CajaMovimiento values(@i_IdCaja,GETDATE(),@i_IdConceptoCaja,@f_Importe,'S',1,@v_NroDocumento,@n_IdUsuario)  
end  
--Actualizar CajaHistorica  
if @c_TipoMovimiento = 'E'  
begin  
update dbo.CajaHistorica set f_CajaFinal = @saldo, f_TotalVenta = f_TotalVenta + @f_Importe where i_IdCaja = @i_IdCaja  
end  
else  
begin  
update dbo.CajaHistorica set f_CajaFinal = @saldo,f_TotalVenta = f_TotalVenta - @f_Importe where i_IdCaja = @i_IdCaja  
end  
go

delete dbo.CajaMovimiento
delete dbo.CajaHistorica