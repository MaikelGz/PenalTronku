# PenalTronku

**PenalTronku** es un videojuego 3D de penaltis desarrollado con Unity. El jugador controla la dirección del disparo, mantiene pulsada una tecla para cargar su potencia y trata de marcar mientras un portero se desplaza lateralmente. El proyecto contiene el código fuente de Unity y una compilación ejecutable para Windows.

## Características

- Lanzamiento de un balón con dirección calculada hacia un objetivo y fuerza configurable mediante una barra de potencia.
- Portero con movimiento lateral y velocidad creciente durante la partida.
- Detección y contador de goles, con sonido y un breve tiempo de espera entre detecciones.
- Cambio temporal de cámara al iniciar el disparo.
- Menús para iniciar, repetir y salir.
- Escenario 3D, modelos, animaciones, materiales, música y efectos de sonido.

## Requisitos

### Para jugar

- Windows de 64 bits.
- Ejecutar la aplicación desde la distribución completa: `PenalTronku.exe` debe permanecer junto a su carpeta `PenalTronku_Data` y los archivos de Unity incluidos en la misma distribución.

En el repositorio hay un ejecutable en la raíz y otra distribución dentro de `PenalTronkuExe/`. También se incluye `PenalTronkuExe.zip`; si se utiliza el archivo ZIP, hay que extraerlo completamente antes de iniciar el ejecutable. No se debe mover el `.exe` fuera de su carpeta de datos.

### Para abrir y modificar el proyecto

- Unity **2020.3.48f1**, según `ProjectSettings/ProjectVersion.txt`.
- Recomendado: abrir la carpeta del repositorio desde Unity Hub y dejar que el Package Manager resuelva las dependencias declaradas en `Packages/manifest.json`.

## Ejecución

### Compilación incluida

1. Descarga o clona el repositorio.
2. Si vas a usar `PenalTronkuExe.zip`, extrae todo su contenido.
3. Ejecuta `PenalTronku.exe` desde la carpeta que contiene `PenalTronku_Data`.

La compilación disponible es para Windows. La configuración y el contenido del repositorio no documentan una build para otras plataformas.

### Desde el editor de Unity

1. Abre Unity Hub y selecciona **Add / Añadir proyecto desde disco**.
2. Selecciona la raíz del repositorio, que contiene `Assets`, `Packages` y `ProjectSettings`.
3. Abre el proyecto con Unity 2020.3.48f1.
4. Para iniciar desde el menú, ejecuta la escena `Assets/Scenes/Portada.unity`. Las escenas están configuradas en `ProjectSettings/EditorBuildSettings.asset`.

## Controles

| Acción | Control |
| --- | --- |
| Cargar potencia del disparo | Mantener `Espacio` |
| Ejecutar disparo | Soltar `Espacio` después de cargar |
| Mover el objetivo de disparo | `W`, `A`, `S`, `D` |
| Navegar por la interfaz | Ratón y botones de la interfaz; el sistema de entrada también define `Enter` y `Escape` |

La dirección del balón se calcula desde su posición hasta el `Target` configurado en la escena. La posición de ese objetivo determina hacia dónde se dispara; el jugador lo mueve en el plano horizontal mediante los ejes `Horizontal` y `Vertical` del Input Manager clásico de Unity.

## Flujo de juego

1. **Portada:** el menú permite iniciar la partida. `MenuInicial.JugarJuego()` carga la siguiente escena de la lista de build settings.
2. **Escena principal:** el jugador ajusta el objetivo, mantiene `Espacio` para cargar fuerza y lo suelta para lanzar el balón. La fuerza aumenta con el tiempo hasta el máximo indicado por `FuerzaMaxima`; la barra refleja la carga normalizada.
3. **Resolución del disparo:** el balón recibe un impulso físico hacia el objetivo. Cuando entra en el collider de gol, `GolManager` incrementa el contador, actualiza el texto y reproduce el efecto si las referencias están configuradas. Después de marcar, espera `tiempoEspera` antes de aceptar otro gol.
4. **Fin de la tanda:** al alcanzar nueve goles, el gestor carga la escena siguiente en el orden de build settings, que actualmente es `Final.unity`.
5. **Final:** el menú puede volver a la escena anterior mediante `Repetir()` o cerrar la aplicación mediante `SalirJuego()`.

El código de `ThrowBall` devuelve la pelota a su posición inicial tres segundos después de soltar la tecla. Para que la mecánica funcione, el objeto de pelota necesita los componentes físicos y etiquetas/colliders esperados por los scripts; las referencias serializadas (objetivo, slider, cámaras y audio) deben estar asignadas en el Inspector.

## Arquitectura y componentes

El código propio se encuentra en `Assets/Resources/Scripts/` y está compuesto por scripts `MonoBehaviour` conectados a objetos y elementos de interfaz de las escenas:

| Script | Responsabilidad |
| --- | --- |
| `ThrowBall.cs` | Acumula fuerza mientras se mantiene `Espacio`, actualiza el slider, aplica un impulso al `Rigidbody` en dirección al objetivo y restablece la pelota tras el disparo. Solicita también el cambio de cámara. |
| `Target.cs` | Mueve el objetivo con los ejes `Horizontal` y `Vertical`, configurados para flechas y `WASD`. |
| `MovimientoPortero.cs` | Desplaza el portero de lado a lado dentro de un rango respecto a su posición inicial e incrementa gradualmente su velocidad. |
| `GolManager.cs` | Detecta entradas de objetos con etiqueta `Pelota` en el trigger de gol, mantiene el contador, actualiza el texto, reproduce el audio, aplica un cooldown y avanza de escena al llegar a nueve goles. |
| `CameraSwitcher.cs` | Cambia la prioridad entre la cámara principal y la cámara de acción mediante Cinemachine, y la restaura tras `cameraSwitchTime`. |
| `MenuInicial.cs` | Implementa las acciones de iniciar, repetir la escena anterior y salir de la aplicación. |

La física del balón se basa en `Rigidbody` y en un material físico (`Assets/Resources/Physics/Ball.physicMaterial`). El portero dispone de un modelo/animaciones y el proyecto utiliza componentes de interfaz de Unity UI y TextMesh Pro. Las referencias públicas de los scripts deben vincularse correctamente en el Inspector; no se resuelven automáticamente por nombre.

## Estructura del repositorio

```text
Assets/
├── Escenariosmarketplace/       Modelos y recursos de escenario
├── PelotaFutbolMarketplace/     Recursos de balones y escena demo del paquete
├── Resources/
│   ├── Animator/                Controlador y clips del portero
│   ├── Images/, Materials/      Recursos visuales
│   ├── Personajes/, Porteria/   Modelos del portero y la portería
│   ├── Physics/                 Material físico del balón
│   ├── Scripts/                 Lógica propia del juego
│   ├── Sounds/                  Música y efecto de gol
│   └── Timeline/                Recurso de Timeline
├── Scenes/                      Portada, escena principal y final
└── TextMesh Pro/                Recursos y ejemplos del paquete TMP
Packages/                        Manifiesto y bloqueo de paquetes Unity
ProjectSettings/                 Configuración del proyecto y escenas de build
PenalTronku.exe                   Build de Windows situada en la raíz
PenalTronku_Data/                 Datos de Unity asociados al ejecutable raíz
PenalTronkuExe/                   Otra distribución empaquetada de Windows
PenalTronkuExe.zip                Archivo comprimido de la distribución
```

La carpeta `Assets/TextMesh Pro/Examples & Extras/` pertenece a los recursos de ejemplo del paquete y no representa lógica propia del juego.

## Escenas

El orden configurado en **Build Settings** es:

| Índice | Archivo | Uso |
| ---: | --- | --- |
| 0 | `Assets/Scenes/Portada.unity` | Menú inicial |
| 1 | `Assets/Scenes/Scena Principal.unity` | Tanda de penaltis |
| 2 | `Assets/Scenes/Final.unity` | Pantalla final |

Los métodos de menú y la lógica de goles navegan usando índices relativos (`buildIndex + 1` o `buildIndex - 1`). Por ello, cambiar el orden de estas escenas altera el flujo de navegación.

## Paquetes y tecnología

- Unity 2020.3.48f1.
- C# sobre el runtime Mono incluido en la distribución.
- Cinemachine 2.6.17 para cámaras virtuales.
- TextMesh Pro 3.0.6 para texto.
- Unity UI (`com.unity.ugui`) para controles de interfaz.
- Post Processing 3.4.0 y Timeline 1.4.8 disponibles en el proyecto.
- Input Manager clásico; no se observa configuración del nuevo Input System.

Las dependencias directas y sus versiones están declaradas en `Packages/manifest.json` y `Packages/packages-lock.json`.

## Recursos de terceros

El proyecto incluye recursos de paquetes y referencias a los siguientes recursos usados durante el desarrollo:

- [Football Goal Net – Sketchfab](https://sketchfab.com/3d-models/football-goal-net-eb0326f99d7344e38514a36e1b2a0b51)
- [Simple Low Poly Nature Pack – Unity Asset Store](https://assetstore.unity.com/packages/3d/environments/landscapes/simple-low-poly-nature-pack-157552)
- [Free Sport Balls – Unity Asset Store](https://assetstore.unity.com/packages/3d/props/free-sport-balls-293937)

Consulta las licencias de cada recurso y paquete antes de redistribuirlos fuera de los términos permitidos por sus autores o tiendas.

## Estado y limitaciones conocidas

- Es un proyecto pequeño de carácter experimental/educativo; la configuración actual menciona posibles errores menores.
- La nota de distribución del proyecto indica que la red de la portería no se muestra correctamente en la build. No se ha documentado aquí una causa técnica confirmada.
- `ThrowBall` llama a `LaunchBallAndSwitchCamera()` mientras `Espacio` permanece pulsado. Como el método inicia una coroutine en cada llamada, la transición de cámara puede programarse repetidamente durante la carga.
- El retorno de la pelota se programa tres segundos después de soltar `Espacio`, sin esperar a que la pelota termine de moverse. En función de la duración del disparo, podría reiniciarse antes de que la jugada se resuelva.
- El gestor de goles avanza de escena al llegar a nueve; el reinicio de `cantidadGoles` que aparece en esa rama no persiste tras la carga de escena.
- La navegación basada en índices depende del orden de escenas indicado arriba.

Estas observaciones describen el comportamiento visible en los scripts y configuración disponibles; no implican una corrección aplicada al código.

## Autoría

Proyecto desarrollado por **MaikelGR**. El nombre de producto configurado en Unity es **PenalTronku**.
