sp_helptext play_stockglobal_listar

  
alter procedure Play_StockGlobal_Listar --'5',1        
as          
select pro.v_CodigoInterno,pro.n_IdProducto,pro.v_Descripcion as 'Producto',stk.f_StockContable as 'Sotano',          
'~/Productos/Redimensionada/' + convert(varchar,pro.n_IdProducto) + '.jpg' as 'v_RutaImagen', pro.f_Precio            
into #temp1                      
from Stock stk                       
inner join Producto pro on stk.n_IdProducto = pro.n_IdProducto                      
where stk.n_IdAlmacen = 1                      
and pro.b_Estado = 1              
order by pro.v_Descripcion asc                      
                      
select pro.v_CodigoInterno,pro.n_IdProducto,pro.v_Descripcion as 'Producto',stk.f_StockContable as 'SemiSotano',          
'~/Productos/Redimensionada/' + convert(varchar,pro.n_IdProducto) + '.jpg' as 'v_RutaImagen', pro.f_Precio                              
into #temp2                      
from Stock stk                       
inner join Producto pro on stk.n_IdProducto = pro.n_IdProducto                      
where stk.n_IdAlmacen = 2                 
and pro.b_Estado = 1            
order by pro.v_Descripcion asc                      
                      
select pro.v_CodigoInterno,pro.n_IdProducto,pro.v_Descripcion as 'Producto',stk.f_StockContable as 'TercerPiso',          
'~/Productos/Redimensionada/' + convert(varchar,pro.n_IdProducto) + '.jpg' as 'v_RutaImagen', pro.f_Precio                              
into #temp3                      
from Stock stk                       
inner join Producto pro on stk.n_IdProducto = pro.n_IdProducto                      
where stk.n_IdAlmacen = 3                 
and pro.b_Estado = 1              
order by pro.v_Descripcion asc                      
                      
select pro.v_CodigoInterno,pro.n_IdProducto,pro.v_Descripcion as 'Producto',stk.f_StockContable as 'FullTienda',          
'~/Productos/Redimensionada/' + convert(varchar,pro.n_IdProducto) + '.jpg' as 'v_RutaImagen', pro.f_Precio                            
into #temp4                      
from Stock stk                       
inner join Producto pro on stk.n_IdProducto = pro.n_IdProducto                      
where stk.n_IdAlmacen = 4                
and pro.b_Estado = 1              
order by pro.v_Descripcion asc                      
                                   
                      
SELECT t1.v_CodigoInterno,t1.n_IdProducto,t1.Producto,Sotano,SemiSotano,TercerPiso,FullTienda,Sotano+SemiSotano+TercerPiso+FullTienda as 'TOTAL' ,t1.v_RutaImagen,t1.f_Precio                    
from #temp1 t1 inner join #temp2 t2 on t1.n_IdProducto = t2.n_IdProducto                      
inner join #temp3 t3 on t1.n_IdProducto = t3.n_IdProducto                       
inner join #temp4 t4 on t1.n_IdProducto = t4.n_IdProducto                      
                      
drop table #temp1                      
drop table #temp2                      
drop table #temp3                      
drop table #temp4                      
go

alter table OrdenCompra
drop column f_SubTotal
go
alter table OrdenCompra
drop column f_IGV
go

alter procedure Play_OrdenCompra_Registrar      
@n_IdProveedor numeric(10,0),      
@n_IdMoneda numeric(10,0),      
@d_FechaEmision datetime,      
@v_Referencia varchar(20),      
@t_Observacion text,      
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
v_NumeroOrdenCompra,v_Referencia,t_Observacion,f_Total,    
n_IdUsuarioCreacion,d_FechaCreacion,i_IdOrdenCompraEstado,v_RutaArchivo)      
values(@i_IdOrdenCompra,@n_IdProveedor,@n_IdMoneda,@d_FechaEmision,    
@v_NumeroOrdenCompra,@v_Referencia,@t_Observacion,@f_Total,    
@n_IdUsuarioCreacion,getdate(),1,@v_RutaArchivo)      
      
end        
else        
begin        
 set @i_IdOrdenCompra = '0'        
end        
select @i_IdOrdenCompra 
go

  
alter procedure Play_OrdenCompra_Actualizar  
@i_IdOrdenCompra int,  
@n_IdProveedor numeric(10,0),  
@d_FechaEmision datetime,  
@v_Referencia varchar(20),  
@t_Observacion text,  
@f_Total float,  
@v_RutaArchivo varchar(1000)  
as  
update OrdenCompra   
set n_IdProveedor = @n_IdProveedor,  
d_FechaEmision = @d_FechaEmision,  
v_Referencia = @v_Referencia,  
t_Observacion = @t_Observacion,  
f_Total = @f_Total,  
v_RutaArchivo = @v_RutaArchivo  
where i_IdOrdenCompra = @i_IdOrdenCompra  
go
  
alter procedure Play_OrdenCompra_Seleccionar    
@i_IdOrdenCompra int    
as    
select oc.n_IdProveedor,n_IdMoneda,d_FechaEmision,v_NumeroOrdenCompra,v_Referencia,    
t_Observacion,f_Total,n_IdUsuarioCreacion,d_FechaCreacion,oc.i_IdOrdenCompraEstado,    
pro.v_Nombre as 'NombreProveedor',usu.v_Nombre as 'Usuario',oce.v_DescripcionEstado,usu.v_RutaFoto,v_RutaArchivo    
from OrdenCompra oc inner join Proveedor pro on oc.n_IdProveedor = pro.n_IdProveedor    
inner join Usuario usu on oc.n_IdUsuarioCreacion = usu.n_IdUsuario    
inner join OrdenCompraEstado oce on oce.i_IdOrdenCompraEstado = oc.i_IdOrdenCompraEstado    
where oc.i_IdOrdenCompra = @i_IdOrdenCompra 
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

alter procedure Play_Usuario_Actualizar_Parcial  
@n_IdUsuario numeric(10,0),    
@i_IdRol int,      
@v_Usuario varchar(30),      
@v_RutaFoto varchar(300),      
@b_Estado bit,    
@v_Nombre varchar(100),
@v_Email varchar(100)
as
update dbo.Usuario set i_IdRol = @i_IdRol,      
v_Usuario = @v_Usuario,    
v_RutaFoto = @v_RutaFoto,    
b_Estado = @b_Estado,    
v_Nombre = @v_Nombre,
v_Email = @v_Email
where n_IdUsuario = @n_IdUsuario
go

alter procedure Play_Usuario_Actualizar_Completo  
@n_IdUsuario numeric(10,0),    
@i_IdRol int,      
@v_Usuario varchar(30),      
@v_Pwd varchar(10),      
@v_RutaFoto varchar(300),      
@b_Estado bit,    
@v_Nombre varchar(100),
@v_Email varchar(100)
as      
update dbo.Usuario set i_IdRol = @i_IdRol,      
v_Usuario = @v_Usuario,    
v_Pwd = @v_Pwd,    
v_RutaFoto = @v_RutaFoto,    
b_Estado = @b_Estado,    
v_Nombre = @v_Nombre,
v_Email = @v_Email
where n_IdUsuario = @n_IdUsuario
go

alter procedure Play_Usuario_Registrar    
@i_IdRol int,    
@v_Usuario varchar(30),    
@v_Pwd varchar(10),    
@v_RutaFoto varchar(300),    
@b_Estado bit,  
@v_Nombre varchar(100),
@v_Email varchar(100)
as    
declare @x numeric(10,0)    
set @x = (select isnull(max(n_IdUsuario),0) from dbo.Usuario) + 1    
insert into dbo.Usuario values(@x,@i_IdRol,@v_Usuario,@v_Pwd,@v_RutaFoto,@b_Estado,@v_Nombre,@v_Email)    
select @x
go

alter procedure Play_Usuario_Seleccionar            
@n_IdUsuario numeric(10,0)            
as            
select n_IdUsuario,i_IdRol,v_Usuario,v_Pwd,v_RutaFoto,b_Estado,v_Nombre,v_Email
from dbo.Usuario            
where n_IdUsuario = @n_IdUsuario
go

alter procedure Play_Usuario_Listar    
@nombre varchar(100),    
@b_Estado bit    
as    
SELECT     dbo.Usuario.n_IdUsuario, dbo.Usuario.i_IdRol, dbo.Rol.v_Nombrerol, 
dbo.Usuario.v_Nombre,dbo.Usuario.v_Usuario, dbo.Usuario.v_Pwd, dbo.Usuario.b_Estado,
dbo.Usuario.v_Email
FROM         dbo.Usuario INNER JOIN    
                      dbo.Rol ON dbo.Usuario.i_IdRol = dbo.Rol.i_IdRol    
where dbo.Usuario.v_Nombre like '%' + @nombre +'%'    
and b_Estado = @b_Estado
go
