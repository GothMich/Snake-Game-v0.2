# Snake Game v0.2

Proyecto base para crear el juego clasico **Snake** en **Unity 2D** usando **C#**, pensado para estudiantes que estan comenzando.

## Objetivo del proyecto

Este proyecto sirve como punto de partida para explicar:

- organizacion basica de un proyecto en Unity
- logica separada en scripts simples
- movimiento por grid
- deteccion de colisiones
- crecimiento de la serpiente
- reinicio de partida

## Version recomendada de Unity

Este proyecto fue creado con:

- `Unity 6000.4.2f1`

Si es posible, conviene que tus alumnos usen esa misma version o una muy cercana para evitar diferencias en paquetes o configuracion.

## Como abrir el proyecto

1. Abrir **Unity Hub**.
2. Elegir **Add project** o **Open**.
3. Seleccionar la carpeta del proyecto.
4. Abrir la escena principal:
   - `Assets/Scenes/MainScene.unity`
5. Presionar **Play**.

## Controles actuales

- `W`, `A`, `S`, `D` o flechas para mover la serpiente
- `R` para reiniciar cuando termina la partida

## Estructura principal

Los scripts del proyecto estan separados por responsabilidad:

- `GameManager`: inicia la partida, controla el estado general, el puntaje y el reinicio
- `SnakeController`: mueve la serpiente por la cuadricula, cambia direccion, detecta colisiones y gestiona el crecimiento
- `FoodSpawner`: genera comida en una celda libre del tablero

Archivos importantes:

- `Assets/Scripts/GameManager.cs`
- `Assets/Scripts/SnakeController.cs`
- `Assets/Scripts/FoodSpawner.cs`
- `Assets/Scenes/MainScene.unity`
- `Assets/SnakeSetupInstructions.md`

## Funcionalidades actuales del proyecto base

Actualmente el proyecto ya incluye:

- una sola escena principal
- movimiento por grid
- movimiento a velocidad fija
- controles por teclado
- comida generada en posiciones libres
- crecimiento de la serpiente al comer
- colision con paredes
- colision con el propio cuerpo
- reinicio de partida con tecla
- puntaje basico en pantalla
- renderizado simple por codigo, sin sprites externos obligatorios

## Configuracion de la escena

La escena esta pensada para ser simple y facil de entender.

Objetos principales:

- `GameManager`
- `Snake`
- `FoodSpawner`

En el inspector de `GameManager` se deben asignar:

- `Snake Controller`
- `Food Spawner`

Si quieres revisar la configuracion paso a paso, consulta:

- `Assets/SnakeSetupInstructions.md`

## Que compartir con los alumnos

Para compartir el proyecto, normalmente basta con entregar estas carpetas:

- `Assets`
- `Packages`
- `ProjectSettings`

No hace falta compartir:

- `Library`
- `Logs`
- `Temp`
- `UserSettings`

Lo mas practico es comprimir la carpeta del proyecto o subirla a un repositorio.

## Posibles mejoras para futuras fases

Estas son buenas ideas para que los alumnos sigan construyendo el juego:

- agregar una pantalla de inicio
- mostrar un panel de game over mas claro
- mejorar la UI del puntaje con Canvas y TextMeshPro
- agregar sonido al comer y al perder
- permitir pausar la partida
- aumentar la velocidad con el tiempo
- agregar limites visuales al tablero
- usar sprites propios para cabeza, cuerpo y comida
- agregar sistema de record o high score
- soportar reinicio automatico
- agregar wrap around para salir por un lado y entrar por el otro
- crear niveles o distintos tamanos de tablero
- añadir menu de dificultad
- portar el control a movil con botones en pantalla

## Ideas para clase

Una secuencia sencilla de trabajo con alumnos puede ser:

1. Ejecutar el proyecto y entender como esta dividido.
2. Leer `GameManager` para ver como se organiza la partida.
3. Leer `SnakeController` para entender el movimiento por grid.
4. Revisar `FoodSpawner` para ver como se eligen posiciones libres.
5. Proponer una mejora pequena y desarrollarla en clase.

## Nota

Este proyecto prioriza scripts cortos, legibles y faciles de explicar antes que una arquitectura avanzada.
