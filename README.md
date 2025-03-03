# EduControl
El Sistema de Gestión Escolar "EduControl" está destinado a un colegio privado de educación secundaria. Su objetivo principal es proporcionar una herramienta que facilite 
la administración y gestión de diferentes aspectos académicos y administrativos, desde el registro de alumnos hasta la obtención de boletines de calificaciones y la 
gestión de cuotas mensuales.

## Características Principales
### Modulo de seguridad
- Iniciar sesión: Los usuarios pueden iniciar sesión en el sistema mediante su usuario y contraseña. El sistema valida la identidad del usuario y les permite acceder a las funcionalidades según el rol al que pertenezcan.
- Cerrar sesión: Los usuarios pueden cerrar sesión en cualquier momento, lo que les obliga a volver a ingresar sus credenciales para acceder al sistema nuevamente.
- Recuperar clave: Los usuarios pueden recuperar su contraseña ingresando su correo electrónico. Un token será enviado a su correo para ingresar al sistema y cambiar su clave.
- Cambiar clave: Los usuarios pueden cambiar su contraseña actual.

### Gestión de Usuarios
  - Gestionar usuarios: Los administradores pueden agregar, modificar y eliminar usuarios.
  - Gestionar roles: Los administradores pueden gestionar los roles de usuarios, asignando permisos específicos según el rol al que pertenezcan.
    
### Ciclos Académicos
- Agregar ciclo académico: Los usuarios autorizados pueden agregar un nuevo ciclo académico, actualizando la información anual del sistema.
- Consultar ciclos académicos: Los usuarios autorizados pueden consultar los ciclos académicos registrados y trabajar en el.
  
### ABM de alumnos
- Registrar alumno: Registrar nuevos alumnos en el sistema.
- Modificar alumno: Modificar los datos personales o académicos de los alumnos existentes.
- Eliminar alumno: Eliminar de forma definitiva un alumno y toda su información asociada.
  
### Grados académicos
Los usuarios pueden consultar los grados académicos disponibles y acceder a la gestión de cursos asociados a cada grado.

### Asistencias y Calificaciones
  - Registrar asistencia: Registrar la asistencia diaria de los alumnos en cada materia.
  - Registrar nota de examen: Registrar las notas de los exámenes de cada materia, incluyendo parciales y exámenes recuperatorios.
  - Registrar observación: Registrar observaciones sobre el desempeño de los alumnos en cada trimestre.
    
### Boletines y Rendimiento
  - Generar boletín escolar: Los usuarios pueden generar boletines escolares trimestrales para cada alumno, con información sobre calificaciones, asistencias, promedios y observaciones.
  - Consultar rendimiento anual: Visualización del rendimiento completo del alumno durante el año, con gráficos de su desempeño en cada trimestre.
    
### Comunicación con Alumnos:
  - Enviar correos electrónicos: Los usuarios pueden enviar correos electrónicos a los alumnos.

### Gestión de Cuotas Mensuales
- Registrar cuota mensual: El sistema permite registrar el valor de las cuotas mensuales para cada mes dentro de un ciclo lectivo.
- Modificar cuota mensual: Los usuarios pueden actualizar el valor de cualquier cuota mensual previamente registrada.
- Eliminar cuota mensual: Los usuarios pueden eliminar cuotas mensuales registradas.
- Registrar pago de cuota mensual: Registro de pagos de cuotas mensuales, con opciones de pago como efectivo, transferencia bancaria o tarjetas.
- Generar comprobante de pago: El sistema permite generar y exportar comprobantes de pago en formato PDF, detallando el monto, la fecha de pago y el método utilizado.

### Auditoría
La auditoría permite el registro y trazabilidad de acciones críticas dentro del sistema, garantizando la transparencia y la capacidad de seguimiento de todas las actividades importantes. 
Este módulo cubre dos áreas: las acciones realizadas sobre los alumnos y las sesiones de usuario.

**Auditoría – Alumnos:** El sistema registra todas las acciones realizadas sobre los alumnos. Cada vez que un usuario
realiza una acción sobre un alumno (registro, modificación o eliminación), se genera un registro de auditoría con la siguiente información:
 - Acción realizada: Especifica si se trató de un Registro, Eliminación o Modificación de un alumno.
 - Datos del alumno: Identificación del alumno afectado por la acción.
 - Usuario responsable: Nombre y rol del usuario que realizó la acción.
 - Fecha y hora: Momento exacto en que se ejecutó la acción.
 - Valores originales y modificados: En caso de una modificación, se registran los datos anteriores y los nuevos valores.

**Auditoría – Login/Logout:** El sistema también audita los inicios y cierres de sesión de los usuarios. Esta funcionalidad permite conocer qué usuarios han accedido al sistema y cuándo 
lo han hecho, así como cuándo cierran sesión. Los datos registrados incluyen:
 - Usuario: Nombre de usuario que realizó el Login o Logout.
 - Rol: Rol que cumple el usuario en el sistema en el momento del Login/Logout.
 - Fecha y hora: Momento exacto en que se realizó la acción de Login o Logout.
 - Acción: Indica si la acción registrada fue un Login o Logout.

El sistema permite la generación de reportes detallados en formato PDF para cada registro de auditoría, tanto para las acciones sobre los alumnos como para los eventos de Login/Logout.

### Reportes
El sistema cuenta con la funcionalidad de generar y exportar diversos reportes en formato PDF, lo que permite a los usuarios obtener información detallada.
Los reportes disponibles son:
- Boletín Escolar.
- Comprobante de Pago de Cuota Mensual.
- Registros de Auditorías.
