  
alter procedure Play_Almacen_Registrar    
@c_Codigo char(1),    
@v_Descripcion varchar(50),  
@v_Direccion varchar(1000),  
@v_Telefono varchar(20),
@b_TieneCaja bit
as    
    
declare @n_IdAlmacen numeric(10,0)    
set @n_IdAlmacen = (select isnull(max(n_IdAlmacen),0) from Almacen) + 1    
    
insert into Almacen values(@n_IdAlmacen,1,@c_Codigo,@v_Descripcion,1,@v_Direccion,@v_Telefono,@b_TieneCaja)    
    
--Crear almacen para todos los productos en la tabla Stock    
    
  declare @n_IdKardex numeric(10,0)    
        declare @n_IdProducto as int    
        declare CURSORITO cursor for    
  select n_IdProducto from producto    
    open CURSORITO    
  fetch next from CURSORITO    
  into @n_IdProducto    
      while @@fetch_status = 0    
    begin    
    
    insert into Stock values(@n_IdAlmacen,@n_IdProducto,0,0)    
 set @n_IdKardex = (select isnull(max(n_IdKardex),0) from Kardex) + 1    
 insert into kardex(n_IdKardex,d_FechaMovimiento,c_TipoMovimiento,i_IdMotivoTraslado,    
 n_IdProducto,n_IdAlmacen,f_Cantidad,f_Saldo)     
 values(@n_IdKardex,getdate(),'I',15,@n_IdProducto,@n_IdAlmacen,0,0)    
    
    fetch next from CURSORITO    
    into @n_IdProducto    
    end    
        close CURSORITO    
  deallocate CURSORITO 
go

alter procedure Play_Almacen_Actualizar    
@n_IdAlmacen numeric(10,0),    
@c_Codigo char(1),    
@v_Descripcion varchar(50),    
@b_Estado bit,  
@v_Direccion varchar(1000),  
@v_Telefono varchar(20),
@b_TieneCaja bit
as    
update Almacen set c_Codigo=@c_Codigo,    
v_Descripcion = @v_Descripcion,    
b_Estado = @b_Estado,  
v_Direccion = @v_Direccion,  
v_Telefono = @v_Telefono,
b_TieneCaja = @b_TieneCaja
where n_IdAlmacen = @n_IdAlmacen
go

alter procedure Play_Almacen_Seleccionar    
@n_IdAlmacen numeric(10,0)    
as    
select c_Codigo,v_Descripcion,b_Estado,v_Direccion,v_Telefono,b_TieneCaja
from Almacen    
where n_IdAlmacen = @n_IdAlmacen  