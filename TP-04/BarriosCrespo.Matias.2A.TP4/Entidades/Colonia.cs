﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace Entidades
{
    public class Colonia : IComprar
    {

        protected double saldoActual;
        protected double gastos;
        protected string nombre;
        protected List<Profesor> listadoDeProfesores;
        protected List<Grupo> listadoDeGrupos;
        protected ControlStock<Producto> stockProductos;


        public Colonia()
        {

        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Colonia(string nombre) : this()
        {
            this.nombre = nombre;
            this.listadoDeGrupos = new List<Grupo>();
            this.listadoDeProfesores = new List<Profesor>();
            this.stockProductos = new ControlStock<Producto>();


            //Stock de diez productos
            //this.stockProductos = new ControlStock<Producto>(10);

        }

        public List<Grupo> ListaDeGrupos
        {
            get { return this.listadoDeGrupos; }
            set { this.listadoDeGrupos = value; }
        }

        public List<Profesor> ListaDeProfesores
        {
            get { return this.listadoDeProfesores; }
            set { this.listadoDeProfesores = value; }
        }

        public double SaldoActual
        {
            get { return this.saldoActual; }
            set { this.saldoActual = value; }
        }

        public double Gastos
        {
            get { return this.gastos; }
            set { this.gastos = value; }
        }

        public ControlStock<Producto> ProductosEnVenta
        {
            get { return this.stockProductos; }
            set { this.stockProductos = value; }
        }

        #region sobrecargas == + -
        /// <summary>
        /// Sobrecarga  == entre Colonia y alumno(colono). 
        /// </summary>
        /// <param name="ca"></param>
        /// <param name="co"></param>
        /// <returns>Si el colono está en la colonia, devuelve true.</returns>
        public static bool operator ==(Colonia ca, Colono co)
        {
            bool retorno = false;
            foreach (Grupo aux in ca.listadoDeGrupos)
            {
                if (aux == co)
                {
                    retorno = true;
                    break;
                }
            }
            return retorno;
        }
        /// <summary>
        /// Sobrecarga  != entre Colonia y alumno(colono). 
        /// </summary>
        /// <param name="ca"></param>
        /// <param name="co"></param>
        /// <returns>Si el colono no está en la colonia, devuelve true.</returns>
        public static bool operator !=(Colonia ca, Colono co)
        {
            return !(ca == co);
        }
        /// <summary>
        /// Igualdad entre Colonia y profeosor. Inspecciona si el profesor está en la colonia.
        /// </summary>
        /// <param name="ca"></param>
        /// <param name="p1"></param>
        /// <returns>Retorna true si el profesor trabaja en la colonia.</returns>
        public static bool operator ==(Colonia ca, Profesor p1)
        {
            bool retorno = false;
            foreach (Profesor aux in ca.listadoDeProfesores)
            {
                if (aux == p1)
                {
                    retorno = true;
                    break;
                }
            }
            return retorno;
        }
        /// <summary>
        /// Distinto entre Colonia y profesor. 
        /// </summary>
        /// <param name="ca"></param>
        /// <param name="p1"></param>
        /// <returns>Retorna true si el profesor no trabaja en la colonia.</returns>
        public static bool operator !=(Colonia ca, Profesor p1)
        {
            return !(ca == p1);
        }

        /// <summary>
        /// Igualdad entre Colonia y grupo. Inspecciona si el grupo se encuentra en la colonia (segun color)
        /// </summary>
        /// <param name="co"></param>
        /// <param name="g1"></param>
        /// <returns>Retorna true si el grupo ya se encuentra en la colonia.</returns>
        public static bool operator ==(Colonia co, Grupo g1)
        {
            bool retorno = false;
            foreach (Grupo aux in co.listadoDeGrupos)
            {
                if (aux == g1)
                {
                    retorno = true;
                    break;
                }
            }
            return retorno;
        }
        /// <summary>
        /// Distinto entre Colonia y grupo. Inspecciona si el grupo no se encuentra en la colonia (segun color)
        /// </summary>
        /// <param name="co"></param>
        /// <param name="g1"></param>
        /// <returns>Retorna true si el grupo no se encuentra en la colonia.</returns>
        public static bool operator !=(Colonia co, Grupo g1)
        {
            return !(co == g1);
        }
        /// <summary>
        /// Sobrecarga + entre Colonia y grupo. Agrega el grupo a la colonia
        /// </summary>
        /// <param name="co"></param>
        /// <param name="g1"></param>
        /// <returns>Si pudo agregar retorna la colonia con el nuevo grupo.</returns>
        public static Colonia operator +(Colonia co, Grupo g1)
        {
            int contadorGrupos = 0;

            foreach (Grupo aux in co.listadoDeGrupos)
            {
                if (aux == g1)
                {
                    contadorGrupos++;
                }
            }

            if (contadorGrupos == 0)
                co.listadoDeGrupos.Add(g1);

            return co;

        }
        /// <summary>
        /// Sobrecarga - entre Colonia y grupo. Elimina el grupo de la colonia
        /// </summary>
        /// <param name="co"></param>
        /// <param name="g1"></param>
        /// <returns>Si pudo eliminar retorna la colonia sin el grupo.</returns>
        public static Colonia operator -(Colonia co, Grupo g1)
        {
            foreach (Grupo aux in co.listadoDeGrupos)
            {
                if (aux == g1)
                {
                    co.listadoDeGrupos.Remove(g1);
                    break;
                }
            }
            return co;
        }
        /// <summary>
        /// Agrega colonos a la colonia.
        /// </summary>
        /// <param name="colonia"></param>
        /// <param name="co"></param>
        /// <returns></returns>
        //La colonia debe ser la encargada de agregar chicos y asignarle un grupo.
        public static Colonia operator +(Colonia colonia, Colono co)
        {
            Random numeroColor = new Random();
            int indice = 0;
            if (colonia != co)
            {
                //Si no hay grupos, se crea uno y agrega al colono.       
                if (colonia.listadoDeGrupos.Count == 0)
                {
                    Grupo nuevoGrupo = new Grupo(10, Colonia.GeneradorColores(), co.EdadGrupo);
                    nuevoGrupo += co;
                    colonia.listadoDeGrupos.Add(nuevoGrupo);

                }
                else
                {
                    foreach (Grupo aux in colonia.listadoDeGrupos)
                    {
                        //Si el grupo existente es de la edad del colono lo agrega
                        //si la capacidad del grupo no ha sido pasada.
                        if (Colonia.RecorrerGruposEdad(colonia, co.EdadGrupo, out indice))
                        {
                            if (aux.ListadoColonos.Count < aux.Capacidad)
                            {
                                colonia.listadoDeGrupos[indice].ListadoColonos.Add(co);
                                //aux.ListadoColonos.Add(co);
                                break;
                            }

                        }
                        else
                        {
                            //Si no existe la edad del grupo, sea crea y agrega al colono.
                            Random otroRandom = new Random();
                            Grupo otroGrupo = new Grupo(10, Colonia.GeneradorColores(), co.EdadGrupo);
                            otroGrupo += co;
                            colonia.listadoDeGrupos.Add(otroGrupo);
                            break;
                        }

                    }
                }
            }

            return colonia;

        }
        /// <summary>
        /// Elimina un colono de la colonia.
        /// </summary>
        /// <param name="colonia"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Colonia operator -(Colonia colonia, Colono c)
        {


            foreach (Grupo aux in colonia.ListaDeGrupos)
            {
                foreach (Colono colono in aux.ListadoColonos)
                {
                    if (colonia == c)
                    {
                        aux.ListadoColonos.Remove(c);

                        break;

                    }

                }
            }

            return colonia;
        }


        /// <summary>
        /// Sobrecarga "+" entre colonia y profesor.
        /// </summary>
        /// <param name="colonia"></param>
        /// <param name="p1"></param>
        /// <returns>Si pudo agregarlo, retorna una colonia con el nuevo profesor.</returns>
        public static Colonia operator +(Colonia colonia, Profesor p1)
        {
            if (colonia != p1)
                colonia.listadoDeProfesores.Add(p1);

            return colonia;
        }


        #endregion

        #region metodos adicionales

        /// <summary>
        /// Recorre los grupos de la colonia buscando la edad pasada por parámetro.        
        /// </summary>
        /// <param name="ca"></param>
        /// <param name="edadDelGrupo"></param>
        /// <param name="indice"></param>
        /// <returns>Devuelve true si existe algun grupo de la colonia coincide con la edad del parámetro </returns>
        private static bool RecorrerGruposEdad(Colonia ca, EEdad edadDelGrupo, out int indice)
        {
            bool retorno = false;
            indice = default;
            int contador = 0;
            foreach (Grupo aux in ca.listadoDeGrupos)
            {
                if (aux.EdadDelGrupo == edadDelGrupo)
                {
                    retorno = true;
                    break;
                }
                else
                    contador++;
            }

            indice = contador;
            return retorno;
        }
        /// <summary>
        /// Genera colores alteatorios
        /// </summary>
        /// <returns></returns>
        private static ConsoleColor GeneradorColores()
        {
            Random rd = new Random();
            return (ConsoleColor)rd.Next(0, 12);
        }
        #endregion


        /// <summary>
        /// Muestra la colonia con toda su informacion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Colonia: {0} 1.0\n", this.nombre);
            sb.AppendFormat("Saldo actual: ${0}\n", this.saldoActual);
            if (this.listadoDeProfesores.Count > 0)
                sb.AppendFormat("Profesores: \n\n");
            foreach (Profesor aux in this.listadoDeProfesores)
            {
                sb.AppendFormat(aux.MostrarDatos());
            }

            foreach (Grupo aux in this.listadoDeGrupos)
            {
                sb.AppendFormat(aux.ToString());
            }

            return sb.ToString();
        }
        /// <summary>
        /// Realiza venta de un producto.
        /// </summary>
        /// <param name="colonia"></param>
        /// <param name="p1"></param>
        /// <param name="c1"></param>
        public void RealizaVenta(Colonia colonia, Producto p1, Colono c1)
        {
            if (colonia.stockProductos == p1)
            {
                this.saldoActual += p1.Precio;
                c1.Deuda += p1.precio;
                colonia.stockProductos -= p1;
                c1.ListaProductosComprados.Add(p1);

            }
            else
            {
                Console.WriteLine("No se pudo realizar venta");
            }


        }

        /// <summary>
        /// Método para obtener los datos desde un dataRow y retornar un colono.
        /// </summary>
        /// <param name="fila"></param>
        /// <returns></returns>
        public Colono ObtenerDatosDatRow(DataRow fila)
        {
            int id = int.Parse(fila["id"].ToString());
            string nombre = fila["nombre"].ToString();
            string apellido = fila["apellido"].ToString();
            int dni = int.Parse(fila["dni"].ToString());
            DateTime fechaNacimiento = Convert.ToDateTime(fila["fechaNacimiento"].ToString());
            //EPeriodoInscripcion periodo = EnumConverter(typeof(EPeriodoInscripcion),fila["periodo"].ToString());
            EPeriodoInscripcion periodo = EPeriodoInscripcion.Mes;
            double saldo = double.Parse(fila["saldo"].ToString());
            Colono c = new Colono(nombre, apellido, fechaNacimiento, dni, periodo, saldo);

            return c;
        }
        /// <summary>
        /// Método serializador.
        /// </summary>
        /// <returns></returns>
        public bool SerializacionXml()
        {
            bool retorno = false;
            Encoding miCodificacion = Encoding.UTF8;


            try
            {
                using (XmlTextWriter writer = new XmlTextWriter(this.Path, miCodificacion))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Colonia));
                    ser.Serialize(writer, this);
                    retorno = true;
                    writer.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retorno;
        }

        public string Path
        {
            get
            {
                string rutaDeGuardado = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "BarriosCrespo.Serializer.Colonia.Xml";
                return rutaDeGuardado;
            }

        }

        /// <summary>
        /// Método deserealizador.
        /// </summary>
        /// <param name="colonia"></param>
        /// <returns></returns>
        public bool Deserealizar(out Colonia colonia)
        {
            bool retorno = false;
            Encoding miCodificacion = Encoding.UTF8;

            colonia = null;

            try
            {
                using (XmlTextReader lector = new XmlTextReader(this.Path))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Colonia));
                    colonia = ((Colonia)ser.Deserialize(lector));
                    retorno = true;
                    lector.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retorno;
        }




    }
}
