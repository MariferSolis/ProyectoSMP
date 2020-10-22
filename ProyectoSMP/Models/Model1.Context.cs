﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoSMP.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SMPEntities4 : DbContext
    {
        public SMPEntities4()
            : base("name=SMPEntities4")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AreaDeMaquina> AreaDeMaquina { get; set; }
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Calendario> Calendario { get; set; }
        public virtual DbSet<Canton> Canton { get; set; }
        public virtual DbSet<CumplimientoMantenimiento> CumplimientoMantenimiento { get; set; }
        public virtual DbSet<Distrito> Distrito { get; set; }
        public virtual DbSet<InventarioDeRepuestos> InventarioDeRepuestos { get; set; }
        public virtual DbSet<MantenimientoDeMaquina> MantenimientoDeMaquina { get; set; }
        public virtual DbSet<Maquina> Maquina { get; set; }
        public virtual DbSet<ParoDeMaquina> ParoDeMaquina { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<TipoDeIdentificacion> TipoDeIdentificacion { get; set; }
        public virtual DbSet<TipoDeSistemaDeMaquina> TipoDeSistemaDeMaquina { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    
        public virtual int ActualizarCalendario(Nullable<int> idEvento, string asunto, string descripcion, Nullable<System.DateTime> comienza, Nullable<System.DateTime> fin, string color, Nullable<bool> todoDia)
        {
            var idEventoParameter = idEvento.HasValue ?
                new ObjectParameter("IdEvento", idEvento) :
                new ObjectParameter("IdEvento", typeof(int));
    
            var asuntoParameter = asunto != null ?
                new ObjectParameter("Asunto", asunto) :
                new ObjectParameter("Asunto", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var comienzaParameter = comienza.HasValue ?
                new ObjectParameter("Comienza", comienza) :
                new ObjectParameter("Comienza", typeof(System.DateTime));
    
            var finParameter = fin.HasValue ?
                new ObjectParameter("Fin", fin) :
                new ObjectParameter("Fin", typeof(System.DateTime));
    
            var colorParameter = color != null ?
                new ObjectParameter("Color", color) :
                new ObjectParameter("Color", typeof(string));
    
            var todoDiaParameter = todoDia.HasValue ?
                new ObjectParameter("TodoDia", todoDia) :
                new ObjectParameter("TodoDia", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ActualizarCalendario", idEventoParameter, asuntoParameter, descripcionParameter, comienzaParameter, finParameter, colorParameter, todoDiaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarAreaDeMaquina(string nombre, string descripcion, Nullable<bool> estado)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarAreaDeMaquina", nombreParameter, descripcionParameter, estadoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarBitacora(string controlador, string metodo, string mensaje, Nullable<int> idUsuario, Nullable<System.DateTime> fecha, string tipo)
        {
            var controladorParameter = controlador != null ?
                new ObjectParameter("Controlador", controlador) :
                new ObjectParameter("Controlador", typeof(string));
    
            var metodoParameter = metodo != null ?
                new ObjectParameter("Metodo", metodo) :
                new ObjectParameter("Metodo", typeof(string));
    
            var mensajeParameter = mensaje != null ?
                new ObjectParameter("Mensaje", mensaje) :
                new ObjectParameter("Mensaje", typeof(string));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(System.DateTime));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarBitacora", controladorParameter, metodoParameter, mensajeParameter, idUsuarioParameter, fechaParameter, tipoParameter);
        }
    
        public virtual int AgregarCalendario(string asunto, string descripcion, Nullable<System.DateTime> comienza, Nullable<System.DateTime> fin, string color, Nullable<bool> todoDia)
        {
            var asuntoParameter = asunto != null ?
                new ObjectParameter("Asunto", asunto) :
                new ObjectParameter("Asunto", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var comienzaParameter = comienza.HasValue ?
                new ObjectParameter("Comienza", comienza) :
                new ObjectParameter("Comienza", typeof(System.DateTime));
    
            var finParameter = fin.HasValue ?
                new ObjectParameter("Fin", fin) :
                new ObjectParameter("Fin", typeof(System.DateTime));
    
            var colorParameter = color != null ?
                new ObjectParameter("Color", color) :
                new ObjectParameter("Color", typeof(string));
    
            var todoDiaParameter = todoDia.HasValue ?
                new ObjectParameter("TodoDia", todoDia) :
                new ObjectParameter("TodoDia", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarCalendario", asuntoParameter, descripcionParameter, comienzaParameter, finParameter, colorParameter, todoDiaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarCumplimientoMantenimiento(Nullable<int> idMantenimiento, Nullable<bool> estado, Nullable<System.DateTime> fecha)
        {
            var idMantenimientoParameter = idMantenimiento.HasValue ?
                new ObjectParameter("IdMantenimiento", idMantenimiento) :
                new ObjectParameter("IdMantenimiento", typeof(int));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(bool));
    
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarCumplimientoMantenimiento", idMantenimientoParameter, estadoParameter, fechaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarInventarioDeRepuestos(string nombre, Nullable<int> cantidad, Nullable<int> requisición, Nullable<int> maximos, Nullable<int> minimos, string tipo, string almacen)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("Cantidad", cantidad) :
                new ObjectParameter("Cantidad", typeof(int));
    
            var requisiciónParameter = requisición.HasValue ?
                new ObjectParameter("Requisición", requisición) :
                new ObjectParameter("Requisición", typeof(int));
    
            var maximosParameter = maximos.HasValue ?
                new ObjectParameter("Maximos", maximos) :
                new ObjectParameter("Maximos", typeof(int));
    
            var minimosParameter = minimos.HasValue ?
                new ObjectParameter("Minimos", minimos) :
                new ObjectParameter("Minimos", typeof(int));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(string));
    
            var almacenParameter = almacen != null ?
                new ObjectParameter("Almacen", almacen) :
                new ObjectParameter("Almacen", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarInventarioDeRepuestos", nombreParameter, cantidadParameter, requisiciónParameter, maximosParameter, minimosParameter, tipoParameter, almacenParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarMantenimientoDeMaquina(string numeroDeOrden, string nombreDeMantenimiento, string tipo, string fecuentia, Nullable<int> rol, Nullable<int> idUsuario, Nullable<int> idMaquina, Nullable<int> idRepuesto, string uRLArchivo)
        {
            var numeroDeOrdenParameter = numeroDeOrden != null ?
                new ObjectParameter("NumeroDeOrden", numeroDeOrden) :
                new ObjectParameter("NumeroDeOrden", typeof(string));
    
            var nombreDeMantenimientoParameter = nombreDeMantenimiento != null ?
                new ObjectParameter("NombreDeMantenimiento", nombreDeMantenimiento) :
                new ObjectParameter("NombreDeMantenimiento", typeof(string));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(string));
    
            var fecuentiaParameter = fecuentia != null ?
                new ObjectParameter("Fecuentia", fecuentia) :
                new ObjectParameter("Fecuentia", typeof(string));
    
            var rolParameter = rol.HasValue ?
                new ObjectParameter("Rol", rol) :
                new ObjectParameter("Rol", typeof(int));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var idMaquinaParameter = idMaquina.HasValue ?
                new ObjectParameter("IdMaquina", idMaquina) :
                new ObjectParameter("IdMaquina", typeof(int));
    
            var idRepuestoParameter = idRepuesto.HasValue ?
                new ObjectParameter("IdRepuesto", idRepuesto) :
                new ObjectParameter("IdRepuesto", typeof(int));
    
            var uRLArchivoParameter = uRLArchivo != null ?
                new ObjectParameter("URLArchivo", uRLArchivo) :
                new ObjectParameter("URLArchivo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarMantenimientoDeMaquina", numeroDeOrdenParameter, nombreDeMantenimientoParameter, tipoParameter, fecuentiaParameter, rolParameter, idUsuarioParameter, idMaquinaParameter, idRepuestoParameter, uRLArchivoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarMaquina(string nombreMaquina, Nullable<int> idTipoSistema, Nullable<int> idArea, string codigo, string modelo, string proceso, Nullable<int> cadencia, string descripcion)
        {
            var nombreMaquinaParameter = nombreMaquina != null ?
                new ObjectParameter("NombreMaquina", nombreMaquina) :
                new ObjectParameter("NombreMaquina", typeof(string));
    
            var idTipoSistemaParameter = idTipoSistema.HasValue ?
                new ObjectParameter("IdTipoSistema", idTipoSistema) :
                new ObjectParameter("IdTipoSistema", typeof(int));
    
            var idAreaParameter = idArea.HasValue ?
                new ObjectParameter("IdArea", idArea) :
                new ObjectParameter("IdArea", typeof(int));
    
            var codigoParameter = codigo != null ?
                new ObjectParameter("Codigo", codigo) :
                new ObjectParameter("Codigo", typeof(string));
    
            var modeloParameter = modelo != null ?
                new ObjectParameter("Modelo", modelo) :
                new ObjectParameter("Modelo", typeof(string));
    
            var procesoParameter = proceso != null ?
                new ObjectParameter("Proceso", proceso) :
                new ObjectParameter("Proceso", typeof(string));
    
            var cadenciaParameter = cadencia.HasValue ?
                new ObjectParameter("Cadencia", cadencia) :
                new ObjectParameter("Cadencia", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarMaquina", nombreMaquinaParameter, idTipoSistemaParameter, idAreaParameter, codigoParameter, modeloParameter, procesoParameter, cadenciaParameter, descripcionParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarParoDeMaquina(string nombreParo, string tipo, string descripcion, Nullable<System.DateTime> fechaComienza, Nullable<System.DateTime> fechaFin, Nullable<int> idMaquina, Nullable<int> idMantenimiento)
        {
            var nombreParoParameter = nombreParo != null ?
                new ObjectParameter("NombreParo", nombreParo) :
                new ObjectParameter("NombreParo", typeof(string));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var fechaComienzaParameter = fechaComienza.HasValue ?
                new ObjectParameter("FechaComienza", fechaComienza) :
                new ObjectParameter("FechaComienza", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(System.DateTime));
    
            var idMaquinaParameter = idMaquina.HasValue ?
                new ObjectParameter("IdMaquina", idMaquina) :
                new ObjectParameter("IdMaquina", typeof(int));
    
            var idMantenimientoParameter = idMantenimiento.HasValue ?
                new ObjectParameter("IdMantenimiento", idMantenimiento) :
                new ObjectParameter("IdMantenimiento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarParoDeMaquina", nombreParoParameter, tipoParameter, descripcionParameter, fechaComienzaParameter, fechaFinParameter, idMaquinaParameter, idMantenimientoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarTipoDeIdentificacion(string descripcion, Nullable<bool> estado)
        {
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarTipoDeIdentificacion", descripcionParameter, estadoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarTipoDeSistemaDeMaquina(string nombre, string descripcion, Nullable<bool> estado)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarTipoDeSistemaDeMaquina", nombreParameter, descripcionParameter, estadoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> AgregarUsuario(string identificacion, Nullable<int> idTipoDeIdentificacion, string nombre, string apellidos, string correo, string password, string tipoCarga, string provincia, string canton, string distrito, Nullable<int> rol, Nullable<bool> estado)
        {
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("Identificacion", identificacion) :
                new ObjectParameter("Identificacion", typeof(string));
    
            var idTipoDeIdentificacionParameter = idTipoDeIdentificacion.HasValue ?
                new ObjectParameter("IdTipoDeIdentificacion", idTipoDeIdentificacion) :
                new ObjectParameter("IdTipoDeIdentificacion", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var tipoCargaParameter = tipoCarga != null ?
                new ObjectParameter("TipoCarga", tipoCarga) :
                new ObjectParameter("TipoCarga", typeof(string));
    
            var provinciaParameter = provincia != null ?
                new ObjectParameter("Provincia", provincia) :
                new ObjectParameter("Provincia", typeof(string));
    
            var cantonParameter = canton != null ?
                new ObjectParameter("Canton", canton) :
                new ObjectParameter("Canton", typeof(string));
    
            var distritoParameter = distrito != null ?
                new ObjectParameter("Distrito", distrito) :
                new ObjectParameter("Distrito", typeof(string));
    
            var rolParameter = rol.HasValue ?
                new ObjectParameter("Rol", rol) :
                new ObjectParameter("Rol", typeof(int));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("AgregarUsuario", identificacionParameter, idTipoDeIdentificacionParameter, nombreParameter, apellidosParameter, correoParameter, passwordParameter, tipoCargaParameter, provinciaParameter, cantonParameter, distritoParameter, rolParameter, estadoParameter);
        }
    
        public virtual ObjectResult<Cantones_Result> Cantones(string provincia)
        {
            var provinciaParameter = provincia != null ?
                new ObjectParameter("Provincia", provincia) :
                new ObjectParameter("Provincia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Cantones_Result>("Cantones", provinciaParameter);
        }
    
        public virtual ObjectResult<ConsultarCalendario_Result> ConsultarCalendario()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConsultarCalendario_Result>("ConsultarCalendario");
        }
    
        public virtual ObjectResult<ConsultarUnUsuarios_Result> ConsultarUnUsuarios(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConsultarUnUsuarios_Result>("ConsultarUnUsuarios", idParameter);
        }
    
        public virtual ObjectResult<ConsultarUsuarios_Result> ConsultarUsuarios()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConsultarUsuarios_Result>("ConsultarUsuarios");
        }
    
        public virtual ObjectResult<Distritos_Result> Distritos(string provincia, string canton)
        {
            var provinciaParameter = provincia != null ?
                new ObjectParameter("Provincia", provincia) :
                new ObjectParameter("Provincia", typeof(string));
    
            var cantonParameter = canton != null ?
                new ObjectParameter("Canton", canton) :
                new ObjectParameter("Canton", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Distritos_Result>("Distritos", provinciaParameter, cantonParameter);
        }
    
        public virtual int EliminarCalendario(Nullable<int> idEvento)
        {
            var idEventoParameter = idEvento.HasValue ?
                new ObjectParameter("IdEvento", idEvento) :
                new ObjectParameter("IdEvento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EliminarCalendario", idEventoParameter);
        }
    
        public virtual ObjectResult<ExisteCorreo_Result> ExisteCorreo(string correo)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ExisteCorreo_Result>("ExisteCorreo", correoParameter);
        }
    
        public virtual ObjectResult<ExisteUsuario_Result> ExisteUsuario(string correo, string password)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ExisteUsuario_Result>("ExisteUsuario", correoParameter, passwordParameter);
        }
    
        public virtual ObjectResult<Provincias_Result> Provincias()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Provincias_Result>("Provincias");
        }
    }
}
