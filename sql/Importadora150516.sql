create procedure Play_OrdenCompraEstado_Combo
as
select i_IdOrdenCompraEstado,v_DescripcionEstado
from OrdenCompraEstado
where b_Estado = 1
go

alter procedure Play_OrdenCompra_Listar --'20150701','20150830',1,'minka'    
@FechaInicio char(8),        
@FechaFin char(8),      
@v_DescripcionEstado varchar(50)
as      
select 
oc.i_IdOrdenCompra,
oc.v_NumeroOrdenCompra,
oc.d_FechaEmision,
prv.v_Nombre,      
md.v_DescripcionMoneda,
oc.f_SubTotal,
oc.f_IGV,
oc.f_Total,  
usu.v_Nombre as 'Usuario',
oce.v_DescripcionEstado
from OrdenCompra oc 
left join Proveedor prv on prv.n_IdProveedor = oc.n_IdProveedor      
inner join Moneda md on md.n_IdMoneda = oc.n_IdMoneda    
inner join Usuario usu on oc.n_IdUsuarioCreacion = usu.n_IdUsuario
inner join OrdenCompraEstado oce on oc.i_IdOrdenCompraEstado = oce.i_IdOrdenCompraEstado
where convert(char(8),d_FechaEmision,112) between @FechaInicio and @FechaFin        
and  oce.v_DescripcionEstado like '%' + @v_DescripcionEstado + '%'
go

sp_helptext Play_OrdenCompra_Registrar

alter procedure Play_OrdenCompra_Registrar  
@n_IdProveedor numeric(10,0),  
@n_IdMoneda numeric(10,0),  
@d_FechaEmision datetime,  
@v_Referencia varchar(20),  
@t_Observacion text,  
@f_SubTotal float,  
@f_IGV float,  
@f_Total float,  
@n_IdUsuarioCreacion numeric(10,0),
@v_RutaArchivo varchar(1000)
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
go


alter procedure Play_OrdenCompraDetalle_Insertar  
@i_IdOrdenCompra int,  
@n_IdProducto int,  
@i_Cantidad int,  
@f_CostoUnidad float, 
@f_CostoTotal float
as  
insert into OrdenCompraDetalle values(@i_IdOrdenCompra,@n_IdProducto,@i_Cantidad,@i_Cantidad,
@f_CostoUnidad,@f_CostoTotal)
go

create procedure Play_Correlativo_Aumentar_SinAlmacen
@n_IdTipoDocumento numeric(10,0)
as  
declare @v_CorrelativoActual varchar(12)  
declare @i_Correlativo int  
set @i_Correlativo = (select convert(int,v_CorrelativoActual) from Correlativo where n_IdTipoDocumento = @n_IdTipoDocumento) + 1  
set @v_CorrelativoActual = replicate('0',6-len(@i_Correlativo)) + convert(varchar,@i_Correlativo)  
update Correlativo set v_CorrelativoActual = @v_CorrelativoActual where n_IdTipoDocumento = @n_IdTipoDocumento
go  

--en la tabla correlativo poner el n_IdAlmacen acepta nulos

alter procedure Play_NotaIngreso_Listar      
@FechaInicio char(8),      
@FechaFin char(8),      
@v_Descripcion varchar(50),  
@b_Estado bit      
as      
select ni.n_IdNotaIngreso,ni.v_NumeroNotaIngreso,ni.d_FechaEmision,alm.v_Descripcion,  
t_Observacion,mt.v_Descripcion as 'MotivoTraslado', usu.v_Nombre  
from NotaIngreso ni     
inner join MotivoTraslado mt on ni.n_IdMotivoTraslado = mt.n_IdMotivoTraslado    
inner join Almacen alm on ni.n_IdAlmacen = alm.n_IdAlmacen      
inner join Usuario usu on usu.n_IdUsuario = ni.n_IdUsuarioCreacion  
where convert(char(8),d_FechaEmision,112) between @FechaInicio and @FechaFin      
and alm.v_Descripcion like '%' + @v_Descripcion + '%'      
and ni.b_Estado = @b_Estado  
go

  
alter procedure Play_NotaSalida_Listar  --'20150711','20150830','','',1      
@FechaInicio char(8),        
@FechaFin char(8),        
@v_Descripcion varchar(50),  
@b_Estado bit        
as        
select ni.n_IdNotaSalida,v_NumeroNotaSalida,d_FechaEmision,alm.v_Descripcion,  
t_Observacion,mt.v_Descripcion as 'Motivo', usu.v_Nombre  
from NotaSalida ni   
inner join Almacen alm on ni.n_IdAlmacen = alm.n_IdAlmacen       
inner join MotivoTraslado mt on ni. n_IdMotivoTraslado = mt.n_IdMotivoTraslado  
inner join Usuario usu on usu.n_IdUsuario = ni.n_IdUsuarioCreacion  
where convert(char(8),d_FechaEmision,112) between @FechaInicio and @FechaFin        
and alm.v_Descripcion like '%' + @v_Descripcion + '%'        
and ni.b_Estado = @b_Estado  
go

alter table Pedido
drop column n_IdBanco
go

  
alter procedure Play_Pedido_Seleccionar            
@n_IdPedido numeric(10,0)            
as            
select             
ped.n_IdPedido,            
ped.n_IdAlmacen,            
ped.n_IdTipoDocumento,            
ped.n_IdCliente,            
ped.n_IdFormaPago,            
ped.n_IdMoneda,            
ped.d_FechaEmision,            
ped.v_NumeroPedido,            
ped.v_NumeroComprobante,            
ped.f_TC,            
ped.f_SubTotal,            
ped.f_Impuesto,            
ped.f_Total,            
ped.f_Pago,            
ped.f_Vuelto,            
ped.t_Obs,            
ped.n_IdUsuarioRegistra,            
ped.d_FechaRegistra,            
ped.n_IdUsuarioModifica,            
ped.d_FechaModifica,            
ped.n_IdUsuarioAnula,            
ped.d_FechaAnula,            
ped.b_EstadoPedido,            
ped.b_EstadoComprobante,            
cli.v_Nombre,          
ped.v_NroAutorizacion,        
td.v_Descripcion as 'TipoDocumento',      
ped.n_IdUsuarioVendedor,    
usu.v_Usuario as 'UsuarioRegistra',    
usu.v_RutaFoto as 'UsuarioFotoRegistra',    
cli.i_Puntos,  
ped.f_Descuento  
from Pedido ped         
inner join dbo.TipoDocumento td on ped.n_IdTipoDocumento = td.n_IdTipoDocumento        
left join cliente cli on ped.n_IdCliente = cli.n_IdCliente    
left join usuario usu on ped.n_IdUsuarioRegistra = usu.n_IdUsuario    
where n_IdPedido = @n_IdPedido    
go

  
  
alter procedure Play_Pedido_Actualizar      
@n_IdPedido numeric(10,0),      
@n_IdCliente numeric(10,0),      
@n_IdFormaPago numeric(10,0),      
@f_SubTotal float,      
@f_Impuesto float,      
@f_Total float,      
@f_Pago float,      
@f_Vuelto float,      
@t_Obs text,      
@n_IdUsuarioModifica numeric(10,0),    
@d_FechaEmision datetime,    
@n_IdUsuarioVendedor numeric(10,0),  
@f_Descuento float  
as      
update Pedido      
set n_IdCliente = @n_IdCliente,      
n_IdFormaPago=@n_IdFormaPago,      
f_SubTotal=@f_SubTotal,      
f_Impuesto=@f_Impuesto,      
f_Total=@f_Total,      
f_Pago=@f_Pago,      
f_Vuelto=@f_Vuelto,      
t_Obs=@t_Obs,      
n_IdUsuarioModifica=@n_IdUsuarioModifica,      
d_FechaModifica=getdate(),    
d_FechaEmision = @d_FechaEmision,    
n_IdUsuarioVendedor = @n_IdUsuarioVendedor,  
f_Descuento = @f_Descuento    
where n_IdPedido = @n_IdPedido    
go

  
alter procedure Play_Pedido_Registrar        
@n_IdAlmacen numeric(10,0),        
@n_IdCliente numeric(10,0),        
@n_IdFormaPago numeric(10,0),        
@n_IdMoneda numeric(10,0),        
@d_FechaEmision datetime,      
@f_SubTotal float,        
@f_Impuesto float,        
@f_Total float,        
@f_Pago float,        
@f_Vuelto float,        
@t_Obs text,        
@n_IdUsuarioRegistra numeric(10,0),      
@n_IdUsuarioVendedor numeric(10,0),  
@f_Descuento float  
as        
        
declare @n_IdPedido numeric(10,0)        
declare @v_NumeroPedido varchar(10)        
declare @Final int            
declare @Actual int            
declare @f_TC float        
          
set @Final = (select convert(int,v_CorrelativoFinal) from Correlativo where n_IdTipoDocumento = 1 and n_IdAlmacen = @n_IdAlmacen)            
set @Actual = (select convert(int,v_CorrelativoActual) from Correlativo where n_IdTipoDocumento = 1 and n_IdAlmacen = @n_IdAlmacen)           
        
if(@Actual < @Final)            
begin            
set @n_IdPedido = (select isnull(max(n_IdPedido),0) from Pedido) + 1        
set @v_NumeroPedido = (select c_Serie+'-'+v_CorrelativoActual from Correlativo where n_IdTipoDocumento = 1 and n_IdAlmacen = @n_IdAlmacen)            
--set @f_TC = (select f_TC from TC where i_Anio = year(getdate()) and i_Mes = month(getdate()) and i_Dia = day(getdate()))        
    set @f_TC = 0    
    
insert into Pedido        
(n_IdPedido,n_IdAlmacen,n_IdTipoDocumento,n_IdCliente,n_IdFormaPago,        
n_IdMoneda,d_FechaEmision,v_NumeroPedido,f_TC,f_SubTotal,f_Impuesto,f_Total,        
f_Pago,f_Vuelto,t_Obs,n_IdUsuarioRegistra,d_FechaRegistra,b_EstadoPedido,n_IdUsuarioVendedor,f_Descuento)        
values(@n_IdPedido,@n_IdAlmacen,1,@n_IdCliente,@n_IdFormaPago,        
@n_IdMoneda,@d_FechaEmision,@v_NumeroPedido,@f_TC,@f_SubTotal,@f_Impuesto,@f_Total,        
@f_Pago,@f_Vuelto,@t_Obs,@n_IdUsuarioRegistra,getdate(),1,@n_IdUsuarioVendedor,@f_Descuento)        
        
end            
else            
begin            
 set @n_IdPedido = '0'            
end            
select @n_IdPedido   
go


CREATE procedure Play_Proveedor_RegistrarRapido      
@v_Nombre varchar(50)      
as      
declare @x numeric(10,0)      
set @x = (select isnull(max(n_IdProveedor),0) from Proveedor) + 1      
insert into Proveedor(n_IdProveedor,v_Nombre,b_Estado) values(@x,@v_Nombre,1)    
select @x  
go

alter procedure Play_OrdenCompra_Seleccionar
@i_IdOrdenCompra int
as
select oc.n_IdProveedor,n_IdMoneda,d_FechaEmision,v_NumeroOrdenCompra,v_Referencia,
t_Observacion,f_SubTotal,f_IGV,f_Total,n_IdUsuarioCreacion,d_FechaCreacion,oc.i_IdOrdenCompraEstado,
pro.v_Nombre as 'NombreProveedor',usu.v_Nombre as 'Usuario',oce.v_DescripcionEstado,usu.v_RutaFoto,
oc.v_RutaArchivo
from OrdenCompra oc inner join Proveedor pro on oc.n_IdProveedor = pro.n_IdProveedor
inner join Usuario usu on oc.n_IdUsuarioCreacion = usu.n_IdUsuario
inner join OrdenCompraEstado oce on oce.i_IdOrdenCompraEstado = oc.i_IdOrdenCompraEstado
where oc.i_IdOrdenCompra = @i_IdOrdenCompra
go

create procedure Play_OrdenCompraDetalle_Seleccionar
@i_IdOrdenCompra int
as
select i_IdOrdenCompra,i_Cantidad as 'Cantidad',ocd.n_IdProducto,
f_CostoUnidad as 'CostoUnitario',f_CostoTotal as 'CostoTotal',
pro.v_Descripcion as 'Producto'
from OrdenCompraDetalle ocd inner join Producto pro on ocd.n_IdProducto = pro.n_IdProducto
where i_IdOrdenCompra = @i_IdOrdenCompra
go


sp_helptext play_pedido_listar
  
alter procedure Play_Pedido_Listar --'20150720','20150720',1                  
@FechaInicio char(8),                      
@FechaFin char(8),                    
@b_Estado bit,            
@n_IdAlmacen numeric(10,0)  
as                    
select pd.n_IdPedido,  
pd.v_NumeroPedido,  
pd.d_FechaEmision,  
cl.v_Nombre,                    
pd.f_Total,   
usu.v_Nombre as 'Vendedor',  
usu2.v_Nombre as 'Usuario',  
fp.v_FormaPago,
f_SubTotal,
f_Descuento
from  dbo.Pedido pd         
left join dbo.Cliente cl on cl.n_IdCliente = pd.n_IdCliente              
left join Usuario usu on usu.n_IdUsuario = pd.n_IdUsuarioVendedor      
inner join Usuario usu2 on usu2.n_IdUsuario = pd.n_IdUsuarioRegistra  
inner join FormaPago fp on fp.n_IdFormaPago =pd.n_IdFormaPago  
where convert(char(8),d_FechaEmision,112) between @FechaInicio and @FechaFin                      
and pd.b_EstadoPedido = @b_Estado            
and pd.n_IdAlmacen = @n_IdAlmacen 
go

sp_helptext play_pedido_seleccionar

alter procedure Play_Pedido_Seleccionar              
@n_IdPedido numeric(10,0)              
as              
select               
ped.n_IdPedido,              
ped.n_IdAlmacen,              
ped.n_IdTipoDocumento,              
ped.n_IdCliente,              
ped.n_IdFormaPago,              
ped.n_IdMoneda,              
ped.d_FechaEmision,              
ped.v_NumeroPedido,              
ped.v_NumeroComprobante,              
ped.f_TC,              
ped.f_SubTotal,              
ped.f_Impuesto,              
ped.f_Total,              
ped.f_Pago,              
ped.f_Vuelto,              
ped.t_Obs,              
ped.n_IdUsuarioRegistra,              
ped.d_FechaRegistra,              
ped.n_IdUsuarioModifica,              
ped.d_FechaModifica,              
ped.n_IdUsuarioAnula,              
ped.d_FechaAnula,              
ped.b_EstadoPedido,              
ped.b_EstadoComprobante,              
cli.v_Nombre,            
ped.v_NroAutorizacion,                
ped.n_IdUsuarioVendedor,      
usu.v_Usuario as 'UsuarioRegistra',      
usu.v_RutaFoto as 'UsuarioFotoRegistra',      
cli.i_Puntos,    
alm.v_Descripcion as 'Tienda',  
usu2.v_Nombre as 'Vendedor',  
cli.v_DocumentoIdentidad,
ped.f_Descuento  
from Pedido ped           
left join cliente cli on ped.n_IdCliente = cli.n_IdCliente      
left join usuario usu on ped.n_IdUsuarioRegistra = usu.n_IdUsuario       
inner join Almacen alm on ped.n_IdAlmacen = alm.n_IdAlmacen  
left join usuario usu2 on ped.n_IdUsuarioVendedor = usu2.n_IdUsuario  
where n_IdPedido = @n_IdPedido 
go

alter table ordencompra
add v_RutaArchivo varchar(1000)
go

