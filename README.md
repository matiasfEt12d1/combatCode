<h1 align="center">E.T. Nº12 D.E. 1º "Libertador Gral. José de San Martín"</h1>

<p align="center">
  <img src="https://et12.edu.ar/imgs/et12.svg">
</p>


# Código en Combate

**Asignaturas:** Laboratorio de Programación Orientada a Objetos | Bases de Datos  
**Proyecto:** Software 2.3  
**Plan Troncal / Ubicación Temporal:** 3.º Bimestre (Duración: 1 Bimestre)  
**Estado:** En proceso

---

## Descripción del Proyecto


**Código en Combate** es un simulador de combate orientado a objetos desarrollado como un proyecto académico integrador para las asignaturas **Laboratorio de Programación Orientada a Objetos** y **Bases de Datos**.

<details close>
<summary>Ver mas</summary>
<br>

El sistema simula enfrentamientos tácticos entre distintos tipos de personajes como guerreros, magos, arqueros y asesinos, los cuales ejecutan acciones de ataque, defensa y uso de habilidades especiales con comportamientos heterogéneos. La solución técnica está diseñada para resolver las acciones del combate sin condicionales por tipo de personaje, mediante la aplicación rigurosa de polimorfismo, interfaces, clases abstractas, métodos virtuales y composición.

La arquitectura del sistema se organiza en una estructura multiproyecto e integra persistencia relacional con MySQL mediante el micro-ORM Dapper, encapsulado a través del patrón Repositorio. Toda modificación de datos en la base de datos se ejecuta mediante Stored Procedures con uso obligatorio de transacciones explícitas (`COMMIT` y `ROLLBACK`), asegurando además un esquema de seguridad con dos conexiones diferenciadas a nivel de motor de base de datos, inyección de dependencias y pruebas unitarias.

## Objetivos Académicos

* Desarrollar un simulador de combate en el que diferentes tipos de personajes ejecuten acciones mediante polimorfismo.
* Integrar una arquitectura multiproyecto desacoplada respetando los principios SOLID, con énfasis en la Inversión de Control (IoC) e Inyección de Dependencias (DI).
* Implementar persistencia de datos eficiente utilizando Dapper y el patrón Repositorio.
* Diseñar la capa de persistencia mediante Stored Procedures en MySQL con transacciones explícitas obligatorias (`COMMIT` y `ROLLBACK`).
* Configurar la gestión de usuarios del SGBD mediante dos accesos y conexiones diferenciadas.
* Construir y ejecutar un proyecto de pruebas unitarias para la validación del simulador.


## Contenidos

### Laboratorio de Programación Orientada a Objetos
* **Polimorfismo:** Sobrecarga de funciones, sobrecarga de operadores y funciones virtuales.
* **Tipos de Proyectos:** Confección de proyectos de biblioteca de clases, proyectos de testing con pruebas unitarias, aplicaciones multiproyecto y gestión de dependencias entre proyectos.
* **Buenas Prácticas:** Principios SOLID, Inversión de Control (IoC) e Inyección de Dependencias (DI).

### Bases de Datos
* **Stored Procedures:** Modelización e implementación de operaciones en el SGBD mediante procedimientos almacenados con uso obligatorio de transacciones explícitas (`START TRANSACTION`, `COMMIT` y `ROLLBACK`).
* **Gestión de Usuarios:** Configuración de dos accesos a MySQL:
  * **Usuario Administrador:** Acceso global a todas las bases de datos del sistema.
  * **Usuario Desarrollo:** Restringido únicamente a la base de datos del proyecto.
* **Persistencia Relacional:** Acceso a datos mediante Dapper y patrón Repositorio.


</details>

## Estructura de Carpetas

El proyecto está organizado bajo la solución `Proyecto.sln` con la siguiente estructura de directorios y módulos:

```text
combatCode/
├── scripts/
├── src/
│   ├── Aplicacion/
│   │   ├── Interfaces/...
│   │   ├── Servicios/...
│   │   ├── Aplicacion.csproj
│   ├── Persistencia/
│   │   ├── Entidades/...
│   │   ├── Repositorios/...
│   │   ├── Persistencia.csproj
│   └── Test/
│       ├── Test.csproj
└── Proyecto.sln
```


## Despligue

### Requisitos
* **.NET SDK** (+8.0)
* **MySQL Server** / MariaDB

### Configuración de Base de Datos
1. Ejecutar las sentencias DDL para crear la base de datos `db_combatCode`.
2. Crear los Stored Procedures requeridos.
3. Crear el usuario de desarrollo y asignar los privilegios correspondientes sobre el esquema del proyecto.

### Compilación y Pruebas
Para compilar la solución completa:
```bash
dotnet build
```

Para ejecutar las pruebas unitarias:
```bash
dotnet test
```

Para iniciar la aplicación:
```bash
dotnet run --project src/Aplicacion/Aplicacion.csproj
```
