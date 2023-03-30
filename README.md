# Bank Leader

Implementación de MongoDB con Clean Architecture en la que cada entidad tiene los siguientes EndPoints:


![Captura de pantalla 2023-03-30 102320](https://user-images.githubusercontent.com/93845990/228885976-f75a36fe-2ccf-46a2-aaa3-56308c13fc0a.png)


![Captura de pantalla 2023-03-30 102346](https://user-images.githubusercontent.com/93845990/228886002-b37daec2-f183-4cc6-8c3b-d5b4e3d23eed.png)


## Colecciones :

### Cliente :

```
{
  "cliente_Id": "string"
  "nombre": "string",
  "apellido": "string",
  "fecha_Nacimiento": "2023-03-30T15:23:55.304Z",
  "telefono": "string",
  "correo": "string",
  "genero": "string"
}
```

### Cuenta :

```
{
  "cuenta_Id": "string",
  "cliente_Id": "string",
  "tipo_Cuenta": "string",
  "saldo": 0,
  "fecha_Apertura": "2023-03-30T15:27:29.853Z",
  "fecha_Cierre": "2023-03-30T15:27:29.853Z",
  "tasa_Interes": 0,
  "estado": "string"
}
```

### Producto :

```
{
  "producto_Id": "string",
  "cliente_Id": "string",
  "tipo_Producto": "string",
  "descripcion": "string",
  "plazo": 0,
  "monto": 0,
  "tasa_Interes": 0,
  "estado": "string"
}
```

### Trajeta :

```
{
    "tarjeta_Id": "string",
    "cliente_Id": "string",
    "tipo_Tarjeta": "string",
    "fecha_Emision": "2023-03-30T15:29:58.906Z",
    "fecha_Vencimiento": "2023-03-30T15:29:58.906Z",
    "limite_Credito": 0,
    "estado": "string"
 }
```

### Transaccion :

```
{
    "transaccion_Id": "string",
    "cuenta_Id": "string",
    "tarjeta_Id": "string",
    "producto_Id": "string",
    "fecha": "2023-03-30",
    "tipo_Transaccion": "string",
    "descripcion": "string",
    "monto": 0
  }
```
