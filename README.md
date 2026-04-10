# Tienda Consola
-> Sebastian Guarachi Aguilar
>[!info] Ingenieria de Software

## Descripcion
Aplicacion de consola para gestionar una tienda con roles de administrador y cliente.

## Funcionalidades principales
- Login con usuario y clave.
- Menu de administrador para gestionar productos y usuarios.
- Menu de cliente para ver productos y realizar compras con carrito.

## Historia de usuario
**Titulo**: Realizar una compra con carrito

**Como**
Cliente

**Quiero**
Seleccionar productos, agregarlos a un carrito y finalizar la compra

**Para**
Poder comprar productos disponibles de manera ordenada y confirmar el total a pagar

**Descripcion**
El cliente inicia sesion con usuario y clave. El sistema reconoce su rol y le muestra el menu de cliente. Desde alli, el cliente puede ver el inventario, elegir productos y cantidades, agregarlos a un carrito de compras y, finalmente, confirmar la compra para descontar el stock y mostrar el total.

**Criterios de aceptacion**
1. Dado que soy un cliente con usuario y clave validos, cuando inicio sesion, entonces el sistema reconoce mi rol y muestra las opciones de cliente.
2. Dado que estoy en el menu de cliente, cuando selecciono "Ver productos", entonces el sistema lista los productos disponibles con nombre, stock y precio.
3. Dado que hay stock disponible, cuando selecciono un producto y una cantidad valida, entonces el sistema agrega el producto al carrito y confirma la accion.
4. Dado que solicito una cantidad mayor al stock, cuando intento agregar al carrito, entonces el sistema muestra un mensaje de stock insuficiente y no agrega el producto.
5. Dado que tengo productos en el carrito, cuando selecciono "Ver carrito", entonces el sistema muestra los items con cantidades y subtotales, y el total acumulado.
6. Dado que tengo productos en el carrito, cuando confirmo la compra, entonces el sistema descuenta el stock del inventario, vacia el carrito y muestra el total pagado.
7. Dado que tengo productos en el carrito, cuando cancelo la compra, entonces el carrito se vacia y el stock no se modifica.
