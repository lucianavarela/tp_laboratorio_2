using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClasesInstanciadas;
using Excepciones;
using EntidadesAbstractas;

namespace Tests_Unitarios
{
    [TestClass]
    public class Tests
    {
        /// <summary>
        /// Corrobora que el DNI sea correcto para un Argentino
        /// </summary>
        [TestMethod]
        public void DNIArgentinoCorrecto()
        {
            try
            {
                Alumno alumno = new Alumno(2, "Luciana", "Varela", "37806217", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(DniInvalidoException));
            }
        }

        /// <summary>
        /// Corrobora que Espacio se encuentre en los valores
        /// Mínimo: 1
        /// Máximo: 50
        /// </summary>
        [TestMethod]
        public void AlumnoRepetido()
        {
            Universidad uni = new Universidad();
            Alumno alumno1 = new Alumno(2, "Luciana", "Varela", "12345678", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);
            try
            {
                Alumno alumno2 = new Alumno(2, "Luciana", "Varela", "12345678", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(AlumnoRepetidoException));
            }
        }

        /// <summary>
        /// Verifico que valide el espacio total del aula al agregar alumnos
        /// En caso de estar completa, el Aula lanzará la excepción AulaLlenaException
        /// </summary>
        [TestMethod]
        public void CantidadProfesores()
        {
            Universidad uni = new Universidad();
            Profesor profesor1 = new Profesor(2, "Pablo", "Garcia", "12345621", Persona.ENacionalidad.Argentino);
            uni += profesor1;
            Assert.AreEqual(1, uni.Instructores.Count);

            Profesor profesor2 = new Profesor(1, "Sebastian", "Lopez", "45678123", Persona.ENacionalidad.Argentino);
            uni += profesor2;
            Assert.AreEqual(2, uni.Instructores.Count);
        }

        /// <summary>
        /// Verifico que al agregar un alumno,
        /// disminuya la cantidad de espacio disponible
        /// </summary>
        [TestMethod]
        public void ListasDeUniversidadCreadas()
        {
            Universidad uni = new Universidad();
            Assert.IsNotNull(uni.Alumnos);
            Assert.IsNotNull(uni.Instructores);
            Assert.IsNotNull(uni.Jornadas);
            Assert.AreEqual(uni.Instructores.Count, 0);
        }
    }
}
